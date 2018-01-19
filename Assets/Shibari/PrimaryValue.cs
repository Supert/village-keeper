namespace Shibari
{
    public class PrimaryValue<TValue> : BindableValue<TValue>
    {
        protected TValue Value { get; private set; }

        public override TValue Get()
        {
            return Value;
        }

        public virtual void Set(TValue value, bool silent = false)
        {
            Value = value;
            if (!silent)
                InvokeOnValueChanged();
        }
    }
}