using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Text))]
    public class TextDataBindView : DataBindView
    {
        [SerializeField]
        protected string format;

        protected Text text;

        protected void Awake()
        {
            text = GetComponent<Text>();
        }

        protected override void OnValueChanged()
        {
            text.text = string.Format(format, GetValue());
        }
    }
}