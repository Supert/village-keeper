namespace Shibari
{
    public class PrimaryValue<TValue> : BindableValue<TValue>, ISettable<TValue>
    {
        protected TValue Value { get; private set; }

        public override TValue Get()
        {
            return Value;
        }

        public virtual void Set(TValue value)
        {
            Value = value;
        }
    }
}