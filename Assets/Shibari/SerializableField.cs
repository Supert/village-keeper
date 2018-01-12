using System;
using System.Globalization;
using UnityEngine;

namespace Shibari
{
    //I'm not proud of this class.
    public class SerializableField<TValue> : BindableField<TValue>
    {
        public virtual string Serialize()
        {
            var oldCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfoByIetfLanguageTag("en-US");

            Type type = typeof(TValue);

            string ret = "";

            if (type == typeof(String))
            {
                ret = Get() as string;
            }
            else
                if (type == typeof(Boolean)
                || type == typeof(Byte)
                || type == typeof(SByte)
                || type == typeof(Int16)
                || type == typeof(UInt16)
                || type == typeof(Int32)
                || type == typeof(UInt32)
                || type == typeof(Int64)
                || type == typeof(UInt64)
                || type == typeof(Char)
                || type == typeof(Double)
                || type == typeof(Single)
                || type == typeof(DateTime))
            {
                ret = Get().ToString();
            }
            else
            {
                try
                {
                    ret = JsonUtility.ToJson(Get());
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    ret = "";
                }
            }

            CultureInfo.CurrentCulture = oldCulture;

            return ret;
        }

        public virtual void Deserialize(string serialized)
        {
            CultureInfo culture = CultureInfo.GetCultureInfoByIetfLanguageTag("en-US");

            if (string.IsNullOrEmpty(serialized))
            {
                Debug.LogWarning("Serialized value is empty");
                Set(default(TValue));
                return;
            }

            serialized = serialized.Replace("\r", string.Empty);

            Type type = typeof(TValue);

            if (type == typeof(Boolean))
                Set((TValue)(object)Convert.ToBoolean(serialized, culture));
            else if (type == typeof(Byte))
                Set((TValue)(object)Convert.ToByte(serialized, culture));
            else if (type == typeof(SByte))
                Set((TValue)(object)Convert.ToSByte(serialized, culture));
            else if (type == typeof(Int16))
                Set((TValue)(object)Convert.ToInt16(serialized, culture));
            else if (type == typeof(UInt16))
                Set((TValue)(object)Convert.ToUInt16(serialized, culture));
            else if (type == typeof(Int32))
                Set((TValue)(object)Convert.ToInt32(serialized, culture));
            else if (type == typeof(UInt32))
                Set((TValue)(object)Convert.ToUInt32(serialized, culture));
            else if (type == typeof(Int64))
                Set((TValue)(object)Convert.ToInt64(serialized, culture));
            else if (type == typeof(UInt64))
                Set((TValue)(object)Convert.ToUInt64(serialized, culture));
            else if (type == typeof(Char))
                Set((TValue)(object)Convert.ToChar(serialized, culture));
            else if (type == typeof(Double))
                Set((TValue)(object)Convert.ToDouble(serialized, culture));
            else if (type == typeof(Single))
                Set((TValue)(object)Convert.ToSingle(serialized, culture));
            else if (type == typeof(DateTime))
                Set((TValue)(object)Convert.ToDateTime(serialized, culture));
            else if (type == typeof(System.String))
                Set((TValue)(object)(serialized ?? ""));
            else
            {
                //dirty hack to ensure that JsonUtility uses right IFormatProvider.
                var oldCulture = CultureInfo.CurrentCulture;
                CultureInfo.CurrentCulture = culture;
                Set(JsonUtility.FromJson<TValue>(serialized));
                CultureInfo.CurrentCulture = oldCulture;
            }
        }
    }
}