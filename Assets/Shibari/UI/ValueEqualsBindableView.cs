using UnityEngine;
using System;

namespace Shibari.UI
{
    public class ValueEqualsBindableView : BindableView
    {
        private BindableValueRestraint[] bindableValueRestraints = new BindableValueRestraint[1]
        {
            new BindableValueRestraint(typeof(System.Object))
        };

        public override BindableValueRestraint[] BindableValueRestraints { get { return bindableValueRestraints; } }

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