using UnityEngine;
using System.Reflection;
using System;
using Shibari;

namespace Shibari.UI
{
    public abstract class BindableView : MonoBehaviour
    {
        protected struct FieldInfo
        {
            public object dataField;
            public MethodInfo getMethod;
            public EventInfo eventInfo;
            public Delegate eventHandler;

            public object GetValue()
            {
                return getMethod.Invoke(dataField, new object[0]);
            }
        }

        [SerializeField]
        private BindableIds[] dataEntries = new BindableIds[0];

        public ModelRecord[] fuckyouunity = new ModelRecord[0];

        protected FieldInfo[] Fields { get; private set; }

        protected abstract void OnValueChanged();


        protected virtual void Start()
        {
            Fields = new FieldInfo[dataEntries.Length];

            for (int i = 0; i < dataEntries.Length; i++)
            {
                Fields[i] = GetField(dataEntries[i]);
                Fields[i].eventInfo.AddEventHandler(Fields[i].dataField, Fields[i].eventHandler);
            }

            OnValueChanged();
        }

        protected FieldInfo GetField(BindableIds ids)
        {
            var data = Model.Get<IBindableData>(ids.dataId);

            var dataFieldProperty = data.GetType().GetProperty(ids.fieldId);

            if (dataFieldProperty == null)
                throw new ArgumentException(string.Format("No property {0} is found in data with prefix {1}", ids.fieldId, ids.dataId), "ids.fieldId");

            if (!Model.IsBindableField(dataFieldProperty))
                throw new ArgumentException(string.Format("Property {0} in data with prefix {1} is not BindableField.", ids.fieldId, ids.dataId), "ids.fieldId");

            FieldInfo result = new FieldInfo()
            {
                dataField = dataFieldProperty.GetValue(data),
                eventInfo = dataFieldProperty.PropertyType.GetEvent("OnValueChanged"),
                eventHandler = Delegate.CreateDelegate(typeof(Action), this, "OnValueChanged"),
            };
            result.getMethod = result.dataField.GetType().GetMethod("Get", new Type[0]);
            return result;
        }

        protected void OnDestroy()
        {
            foreach (var field in Fields)
                field.eventInfo.RemoveEventHandler(field.dataField, field.eventHandler);
        }
    }
}