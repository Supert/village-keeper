using System;
using UnityEngine;

namespace VillageKeeper.Data
{
    [Serializable]
    public class IntArray : SerializableArray<int>
    {
        [SerializeField]
        private int[] values;
        protected override int[] Values { get { return values; } }
    }
}