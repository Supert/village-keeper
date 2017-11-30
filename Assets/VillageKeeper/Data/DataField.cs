using System;

namespace VillageKeeper.Data
{
    public abstract class DataField<T>
    {
        protected T DefaultValue { get; set; }

        protected abstract string Id { get; }

        public abstract T Get();

        public virtual void Set(T value)
        {
            OnValueChanged?.Invoke(value);
        }

        public event Action<T> OnValueChanged;
    }
}