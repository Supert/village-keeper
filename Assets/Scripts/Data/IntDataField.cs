using UnityEngine;
using System;

namespace Shibari
{
    public class IntDataField : DataField<Int32>
    {
        protected override Int32 GetDefaultValue()
        {
            if (PlayerPrefs.HasKey(Id))
                return PlayerPrefs.GetInt(Id);
            else
                return default(Int32);
        }

        public override void Set(Int32 value)
        {
            PlayerPrefs.SetInt(Id, value);
            base.Set(value);
        }
    }
}