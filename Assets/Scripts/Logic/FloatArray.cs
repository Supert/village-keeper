using System;
using UnityEngine;

namespace VillageKeeper.Data
{
    [Serializable]
    public class FloatArray : SerializableArray<float>
    {
        [SerializeField]
        private float[] values;
        protected override float[] Values { get { return values; } }
    }
}