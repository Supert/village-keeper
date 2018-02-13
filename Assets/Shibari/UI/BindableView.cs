using UnityEngine;
using System;
using System.Linq;

namespace Shibari.UI
{

    public abstract class BindableView : MonoBehaviour
    {
        public abstract BindableValueRestraint[] BindableValueRestraints { get; }

        [SerializeField]
        private BindableIds[] bindableValuesIds = new BindableIds[0];

        public BindableIds[] BindableValuesIds { get { return bindableValuesIds; } set { bindableValuesIds = value; } }

        protected BindableValueInfo[] BindedValues { get; private set; }

        public void Initialize()
        {
            if (BindableValuesIds == null || bindableValuesIds.Length != BindableValueRestraints.Length)
                bindableValuesIds = BindableValueRestraints.Select(bvt => new BindableIds() { allowedValueType = bvt.Type }).ToArray();
            for (int i = 0; i < bindableValuesIds.Length; i++)
            {
                if (bindableValuesIds[i] == null)
                    bindableValuesIds[i] = new BindableIds();
                if (bindableValuesIds[i].isSetterRequired != BindableValueRestraints[i].IsSetterRequired)
                    bindableValuesIds[i].isSetterRequired = BindableValueRestraints[i].IsSetterRequired;
                if (bindableValuesIds[i].allowedValueType == null || bindableValuesIds[i].allowedValueType != BindableValueRestraints[i].Type)
                    bindableValuesIds[i].allowedValueType = BindableValueRestraints[i].Type;
            }
        }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected abstract void OnValueChanged();

        protected Delegate onValueChangedDelegate;

        protected virtual void Start()
        {
            onValueChangedDelegate = Delegate.CreateDelegate(typeof(Action), this, "OnValueChanged");
            BindedValues = new BindableValueInfo[bindableValuesIds.Length];

            for (int i = 0; i < bindableValuesIds.Length; i++)
            {
                BindedValues[i] = GetField(bindableValuesIds[i]);
                BindedValues[i].EventInfo.AddEventHandler(BindedValues[i].BindableValue, onValueChangedDelegate);
            }

            OnValueChanged();
        }

        protected BindableValueInfo GetField(BindableIds ids)
        {
            return Model.RootNode.GetBindableValueByPath(ids.pathInModel.Split('/'));
        }

        protected void OnDestroy()
        {
            if (BindedValues != null)
            {
                foreach (var field in BindedValues)
                {
                    field.EventInfo.RemoveEventHandler(field.BindableValue, onValueChangedDelegate);
                }
            }
        }
    }
}