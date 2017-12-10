using System;
using UnityEngine;

namespace Shibari
{
    //I'm not proud of this class.
    public class SerializableField<TValue> : BindableField<TValue>
    {
        public virtual string Serialize()
        {
            Type type = typeof(TValue);


            if (type == typeof(String)
                || type == typeof(Boolean)
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
                return Get().ToString();
            }
            else
            {
                try
                {
                    return JsonUtility.ToJson(Get());
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    return "";
                }
            }
        }

        public virtual void Deserialize(string serialized)
        {
            Type type = typeof(TValue);

            if (type == typeof(Boolean))
                Set((TValue)(object)Convert.ToBoolean(serialized));
            else if (type == typeof(Byte))
                Set((TValue)(object)Convert.ToByte(serialized));
            else if (type == typeof(SByte))
                Set((TValue)(object)Convert.ToSByte(serialized));
            else if (type == typeof(Int16))
                Set((TValue)(object)Convert.ToInt16(serialized));
            else if (type == typeof(UInt16))
                Set((TValue)(object)Convert.ToUInt16(serialized));
            else if (type == typeof(Int32))
                Set((TValue)(object)Convert.ToInt32(serialized));
            else if (type == typeof(UInt32))
                Set((TValue)(object)Convert.ToUInt32(serialized));
            else if (type == typeof(Int64))
                Set((TValue)(object)Convert.ToInt64(serialized));
            else if (type == typeof(UInt64))
                Set((TValue)(object)Convert.ToUInt64(serialized));
            else if (type == typeof(Char))
                Set((TValue)(object)Convert.ToChar(serialized));
            else if (type == typeof(Double))
                Set((TValue)(object)Convert.ToDouble(serialized));
            else if (type == typeof(Single))
                Set((TValue)(object)Convert.ToSingle(serialized));
            else if (type == typeof(DateTime))
                Set((TValue)(object)Convert.ToDateTime(serialized));
            else if (type == typeof(System.String))
                Set((TValue)(object)serialized);
            else
            {
                try
                {
                    Set(JsonUtility.FromJson<TValue>(serialized));
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}