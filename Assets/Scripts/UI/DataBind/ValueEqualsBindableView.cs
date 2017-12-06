using UnityEngine;
using System.Linq;

namespace Shibari.UI
{
    public class ValueEqualsBindableView : BindableView
    {
        [SerializeField]
        protected bool isInversed;
        [SerializeField]
        protected string equalsTo;
        protected override void OnValueChanged()
        {
            gameObject.SetActive(equalsTo.Equals(Fields[0].GetValue().ToString()) != isInversed);
        }
    }
}