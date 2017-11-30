using UnityEngine;
using System;

namespace VillageKeeper.Data
{
    public class IntDataField : DataField<Int32>
    {
        private string id;
        protected override string Id { get { return id; } }

        public IntDataField(string id, int defaultValue = default(int))
        {
            this.id = id;
            DefaultValue = defaultValue;
        }

        public override Int32 Get()
        {
            if (PlayerPrefs.HasKey(id))
                return PlayerPrefs.GetInt(id);
            else
                return DefaultValue;
        }

        public override void Set(Int32 value)
        {
            PlayerPrefs.SetInt(id, value);
            base.Set(value);
        }
    }
}