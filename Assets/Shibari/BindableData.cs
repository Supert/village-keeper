namespace Shibari
{
    public abstract class BindableData
    {
        public virtual void Init(string key) { }

        public void DeserializeValues(string json) { }

        public string GenerateSerializationTemplate()
        {
            throw new System.NotImplementedException();
        }
    }
}