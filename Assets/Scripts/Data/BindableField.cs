using System;

namespace VillageKeeper.Data
{
    public class BindableField<TValue>
    {
        protected TValue Value { get; private set; }

        public virtual void Init(string dataId, string fieldId)
        {

        }

        public TValue Get()
        {
            return Value;
        }

        public virtual void Set(TValue value)
        {
            Value = value;
            OnValueChanged?.Invoke();
        }

        protected virtual TValue GetDefaultValue()
        {
            return (default(TValue));
        }

        public event Action OnValueChanged;
    }
}