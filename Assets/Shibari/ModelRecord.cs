using TypeReferences;
using System;
using UnityEngine;

namespace Shibari
{
    [Serializable]
    public class ModelRecord
    {
        public string key;

        [ClassImplements(typeof(IBindableData))]
        public ClassTypeReference type;
        
        public string defaultValuesPath;
    }
}