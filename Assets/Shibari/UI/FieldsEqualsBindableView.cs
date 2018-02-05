using UnityEngine;
using System.Linq;
using System;

namespace Shibari.UI
{
    public class FieldsEqualsBindableView : BindableView
    {
        private BindableValueRestraint[] bindableValueTypes = new BindableValueRestraint[2]
        {
            new BindableValueRestraint( typeof(System.Object)),
            new BindableValueRestraint(typeof(System.Object))
        };
        public override BindableValueRestraint[] BindableValueRestraints { get { return bindableValueTypes; } }

        [SerializeField]
        protected bool isInversed;

        protected override void OnValueChanged()
        {
            string value = Fields[0].GetValue().ToString();
            gameObject.SetActive(Fields.Select(f => f.GetValue()).Any(v => v.ToString() != value) == isInversed);
        }
    }
}