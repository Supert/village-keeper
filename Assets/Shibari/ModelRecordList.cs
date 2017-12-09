using System;
using UnityEngine;

namespace Shibari
{
    [Serializable]
    public class ModelRecordSerializable
    {
        public ModelRecord[] values;
    }

    public class ModelRecordScriptableObject : ScriptableObject
    {
        public ModelRecord value;
    }
}