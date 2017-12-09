namespace Shibari
{
    public class LocalizationData : IBindableData
    {
        public DataField<string> Language { get; private set; }
        public DataField<string> DefaultLanguage { get; private set; }

        public void Init(string key)
        {

        }
    }
}