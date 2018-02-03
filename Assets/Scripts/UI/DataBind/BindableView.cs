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

        protected BindableValueInfo[] Fields { get; private set; }

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
            Fields = new BindableValueInfo[bindableValuesIds.Length];

            for (int i = 0; i < bindableValuesIds.Length; i++)
            {
                Fields[i] = GetField(bindableValuesIds[i]);
                Fields[i].EventInfo.AddEventHandler(Fields[i].BindableValue, onValueChangedDelegate);
            }

            OnValueChanged();
        }

        protected BindableValueInfo GetField(BindableIds ids)
        {
            if (Model.Get<BindableData>(ids.dataId).Values.ContainsKey(ids.fieldId))
                return Model.Get<BindableData>(ids.dataId).Values[ids.fieldId];
            Debug.Log($"Field with id {ids.fieldId} is not found in data {ids.dataId}.");
            return null;
        }

        protected void OnDestroy()
        {
            if (Fields != null)
            {
                foreach (var field in Fields)
                {
                    field.EventInfo.RemoveEventHandler(field.BindableValue, onValueChangedDelegate);
                }
            }
        }
    }
}