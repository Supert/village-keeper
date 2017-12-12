using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Shibari.UI
{

    [RequireComponent(typeof(Text))]
    public class TextBindableView : BindableView
    {
        [SerializeField]
        protected bool useFormatProvider;

        [SerializeField]
        protected BindableIds formatProvider;

        protected BindableFieldInfo formatProviderField;

        [SerializeField]
        protected string format;

        protected Text text;

        protected void Awake()
        {
            text = GetComponent<Text>();
        }

        protected override void Start()
        {
            base.Start();
            if (useFormatProvider)
                formatProviderField = GetField(formatProvider);
        }

        protected override void OnValueChanged()
        {
            text.text = string.Format(format, Fields.Select(f => f.GetValue()).ToArray());
        }

        protected string GetFormat()
        {
            if (useFormatProvider)
                return formatProviderField.ToString();
            else
                return format;
        }
    }
}