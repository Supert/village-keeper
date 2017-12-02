using System;

namespace VillageKeeper.Data
{
    public abstract class DataField<T>
    {
        protected T Value { get; private set; }

        protected string Id { get; private set; }

        public void Init(string dataFieldId)
        {
            Id = dataFieldId;
            Value = GetDefaultValue();
        }

        protected abstract T GetDefaultValue();

        public T Get()
        {
            return Value;
        }

        public virtual void Set(T value)
        {
            Value = value;
            OnValueChanged?.Invoke();
        }

        public event Action OnValueChanged;
    }
}