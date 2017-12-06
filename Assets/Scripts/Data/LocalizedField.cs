using UnityEngine;
using System.Collections.Generic;

namespace Shibari
{
    public class LocalizedField : BindableField<string>
    {
        [SerializeField]
        protected Dictionary<string, string> values = new Dictionary<string, string>();
        public override void Set(string value)
        {
            string language = Model.Localization.Language.Get();
            if (string.IsNullOrEmpty(language) || !values.ContainsKey(language))
            {
                if (values.ContainsKey(Model.Localization.DefaultLanguage.Get()))
                    base.Set(values[Model.Localization.DefaultLanguage.Get()]);
                else
                    base.Set("!" + value + "!");
            }
            else
                base.Set(value);
        }
    }
}