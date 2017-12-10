using TypeReferences;
using System;
using UnityEngine;

namespace Shibari
{
    [Serializable]
    public class ModelRecord
    {
        public string key;

        [ClassExtends(typeof(BindableData))]
        public ClassTypeReference type;
    }
}