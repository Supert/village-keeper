using Shibari;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VillageKeeper.Model
{
    public class ResourceValue<TValue> : CalculatedValue<TValue> where TValue : UnityEngine.Object
    {
        public ResourceValue(Func<string> formatProvider, IEnumerable<IBindable> formatValues)
            : base(() => ResourceMock.Get<TValue>(string.Format(formatProvider(), formatValues.ToArray())), formatValues.AsEnumerable())
        {

        }

        public ResourceValue(Func<string> formatProvider, params IBindable[] formatValues)
            : this(formatProvider, formatValues.AsEnumerable())
        {

        }

        public ResourceValue(BindableValue<string> bindable, params IBindable[] formatValues)
            : this(bindable.Get, formatValues.Concat(new List<IBindable> { bindable }))
        {

        }
    }
}