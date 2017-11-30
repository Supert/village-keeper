using UnityEngine;
using System;

namespace VillageKeeper.Data
{
    public class BoolDataField : DataField<Boolean>
    {
        private string id;
        protected override string Id { get { return id; } }

        public BoolDataField(string id, bool defaultValue = default(bool))
        {
            this.id = id;
            DefaultValue = defaultValue;
        }

        public override bool Get()
        {
            if (PlayerPrefs.HasKey(id))
                return PlayerPrefs.GetInt(id) != 0;
            else
                return DefaultValue;
        }

        public override void Set(bool value)
        {
            PlayerPrefs.SetInt(id, value ? 1 : 0);
            base.Set(value);
        }
    }
}