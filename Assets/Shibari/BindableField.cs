using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shibari
{
    public class PrimaryValue<TValue> : BindableValue<TValue>
    {
        protected TValue Value { get; private set; }

        public override TValue Get()
        {
            return Value;
        }

        public virtual void Set(TValue value)
        {
            Value = value;
            InvokeOnValueChanged();
        }
    }

    public class SecondaryValue<TValue> : BindableValue<TValue>
    {
        protected TValue Value { get; private set; }

        private Func<TValue> calculateValueFunction;

        public SecondaryValue(Func<TValue> calculateValueFunction, IEnumerable<IBindable> subscribeTo)
        {
            foreach (var bindable in subscribeTo)
            {
                bindable.OnValueChanged += ValueChanged;
            }

            try
            {
                calculateValueFunction.Invoke();
            }
            catch
            {

            }
        }

        public SecondaryValue(Func<TValue> calculateValueFunction, params IBindable[] subscribeTo) : this(calculateValueFunction, subscribeTo.AsEnumerable())
        {

        }

        protected virtual void ValueChanged()
        {
            if (calculateValueFunction == null)
            {
                Debug.LogError("calculateValueFunction is not assigned.");
                return;
            }
            calculateValueFunction.Invoke();
            InvokeOnValueChanged();
        }

        public override TValue Get()
        {
            return Value;
        }
    }

    public abstract class BindableValue<TValue> : IBindable
    {
        public event Action OnValueChanged;

        protected void InvokeOnValueChanged()
        {
            OnValueChanged?.Invoke();
        }

        public abstract TValue Get();

        public static implicit operator TValue(BindableValue<TValue> bindableValue)
        {
            return bindableValue.Get();
        }
    }

    public interface IBindable
    {
        event Action OnValueChanged;
    }
}