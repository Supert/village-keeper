using UnityEngine;
using System.Linq;

namespace Shibari.UI
{
    public class FieldsEqualsBindableView : BindableView
    {
        [SerializeField]
        protected bool isInversed;
        protected override void OnValueChanged()
        {
            string value = Fields[0].GetValue().ToString();
            gameObject.SetActive(Fields.Select(f => f.GetValue()).Any(v => v.ToString() != value) == isInversed);
        }
    }
}