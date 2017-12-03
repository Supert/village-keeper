using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Text))]
    public class LocalizedBindableView : BindableView
    {
        protected Text text;

        [SerializeField, Header("Localized Format Entry")]
        protected BindableIds localizedFormatEntry;

        protected FieldInfo LocalizedFormatField { get; private set; }

        protected override void OnValueChanged()
        {
            text.text = string.Format(LocalizedFormatField.GetValue().ToString(), Fields.Select(f => f.GetValue()).ToArray());
        }

        protected virtual void Awake()
        {
            text = GetComponent<Text>();
        }

        protected override void Start()
        {
            LocalizedFormatField = GetField(localizedFormatEntry);
            base.Start();
        }
    }
}