﻿using System;
using UnityEngine;

namespace VillageKeeper.Data
{
    [Serializable]
    public class StringArray : SerializableArray<string>
    {
        [SerializeField]
        string[] values;

        public override string[] Values
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