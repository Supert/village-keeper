using UnityEngine;
using System;
using UnityEngine.UI;

namespace Shibari.UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderView : BindableView
    {
        private Slider slider;

        protected override void Awake()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener((f) =>
            {
                if (f != (float)Fields[0].GetValue())
                    (Fields[0] as PrimaryValueInfo).SetValue(f);
            });
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        private BindableValueRestraint[] bindableValueRestraints = new BindableValueRestraint[1]
        {
            new BindableValueRestraint(typeof(float), true)
        };

        public override BindableValueRestraint[] BindableValueRestraints
        {
            get
            {
                return bindableValueRestraints;
            }
        }

        protected override void OnValueChanged()
        {
            float value = (float)Fields[0].GetValue();
            if (value != slider.value)
                slider.value = (float)Fields[0].GetValue();
        }
    }
}