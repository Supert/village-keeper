using System;
using UnityEngine;

namespace VillageKeeper.Data
{
    [Serializable]
    public class FloatArray : SerializableArray<float>
    {
        [SerializeField]
        private float[] values;
        public override float[] Values
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
            }
        }
    }
}