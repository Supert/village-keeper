using Shibari;
using System;

namespace VillageKeeper.Model
{
    public class ResourceValue<TValue> : SecondaryValue<TValue> where TValue : UnityEngine.Object
    {
        public ResourceValue(Func<string> formatProvider, params IBindable[] formatValues) : base(() => ResourceMock.Get<TValue>(string.Format(formatProvider(), formatValues)), formatValues)
        {

        }
    }
}