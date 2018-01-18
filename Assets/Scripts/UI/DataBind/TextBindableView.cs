using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Shibari.UI
{

    [RequireComponent(typeof(Text))]
    public class TextBindableView : BindableView
    {        
        protected Text text;

        protected void Awake()
        {
            text = GetComponent<Text>();
        }

        protected override void OnValueChanged()
        {
            text.text = Fields[0].ToString();
        }
    }
}