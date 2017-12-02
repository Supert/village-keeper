using UnityEngine;
using System;

namespace VillageKeeper.Data
{
    public class BoolDataField : DataField<Boolean>
    {
        protected override bool GetDefaultValue()
        {
            if (PlayerPrefs.HasKey(Id))
                return PlayerPrefs.GetInt(Id) != 0;
            else
                return default(Boolean);
        }

        public override void Set(bool value)
        {
            PlayerPrefs.SetInt(Id, value ? 1 : 0);
            base.Set(value);
        }
    }
}