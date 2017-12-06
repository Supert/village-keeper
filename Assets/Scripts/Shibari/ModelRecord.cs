using TypeReferences;
using System;

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