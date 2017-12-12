using System;
using UnityEngine;

namespace VillageKeeper.Data
{
    [Serializable]
    public class StringArray : SerializableArray<string>
    {
        [SerializeField]
        string[] values;

        protected override string[] Values { get { return values; } }
    }
}