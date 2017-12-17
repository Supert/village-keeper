using System;
using UnityEngine;

namespace VillageKeeper.Data
{
    [Serializable]
    public class IntArray : SerializableArray<int>
    {
        [SerializeField]
        private int[] values;
        public override int[] Values
        {
            get
            {
                return values;
            }
            set
            {
                this.values = value;
            }
        }
    }
}