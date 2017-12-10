using System;

namespace Shibari
{
    public class BindableField<TValue>
    {
        protected TValue Value { get; private set; }
        
        public TValue Get()
        {
            return Value;
        }

        public virtual void Set(TValue value)
        {
            Value = value;
            OnValueChanged?.Invoke();
        }

        public event Action OnValueChanged;
    }
}