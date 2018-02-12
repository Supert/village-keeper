using System;
using TypeReferences;

namespace Shibari
{
    [Serializable]
    public class BindableIds
    {
        public Type allowedValueType;
        public bool isSetterRequired;
        public string pathInModel;
    }
}