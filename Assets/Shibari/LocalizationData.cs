namespace Shibari
{
    public abstract class LocalizationData : BindableData
    {
        public BindableField<string> Language { get; protected set; }
        public BindableField<string> DefaultLanguage { get; protected set; }
    }
}