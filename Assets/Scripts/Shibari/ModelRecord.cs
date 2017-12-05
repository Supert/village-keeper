using TypeReferences;

namespace Shibari
{
    public class ModelRecord
    {
        public string key;
        [ClassImplements(typeof(IBindableData))]
        public ClassTypeReference type;
        public string defaultValuesPath;
    }
}