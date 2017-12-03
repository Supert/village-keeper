using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Text))]
    public class TextBindableView : BindableView
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
            text.text = string.Format(format, Fields.Select(f => f.GetValue()));
        }
    }
}