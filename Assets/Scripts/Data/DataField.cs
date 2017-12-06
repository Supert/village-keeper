namespace Shibari
{
    public abstract class DataField<TValue> : BindableField<TValue>
    {
        protected string Id { get; private set; }

        public override void Init(string dataId, string fieldId)
        {
            base.Init(dataId, fieldId);
            Id = dataId + "." + fieldId;
        }
    }
}