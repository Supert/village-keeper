using UnityEngine;
using System.Reflection;
using System;

namespace VillageKeeper.UI
{
    public abstract class DataBindView : MonoBehaviour
    {
        [SerializeField]
        protected string dataPrefix;
        [SerializeField]
        protected string dataFieldName;

        private object dataField;
        private MethodInfo getMethod;
        private EventInfo eventInfo;
        private Delegate eventHandler;

        protected abstract void OnValueChanged();

        protected object GetValue()
        {
            return getMethod.Invoke(dataField, new object[0]);
        }

        protected virtual void Start()
        {
            if (!CoreScript.Instance.Data.ContainsKey(dataPrefix))
                throw new ArgumentException(string.Format("No data with prefix {0} is found.", dataPrefix), "dataPrefix");

            var dataFieldProperty = CoreScript.Instance.Data[dataPrefix].GetType().GetProperty(dataFieldName);

            if (dataFieldProperty == null)
                throw new ArgumentException(string.Format("No property {0} is found in data with prefix {1}", dataFieldName, dataPrefix), "dataFieldName");

            if (!Data.BindedData.CheckIfPropertyIsDataField(dataFieldProperty))
                throw new ArgumentException(string.Format("Property {0} in data with prefix {1} is not DataField.", dataFieldName, dataPrefix), "DataFieldName");

            dataField = dataFieldProperty.GetValue(CoreScript.Instance.Data[dataPrefix]);
            getMethod = dataField.GetType().GetMethod("Get", new Type[0]);
            eventInfo = dataFieldProperty.PropertyType.GetEvent("OnValueChanged");
            eventHandler = Delegate.CreateDelegate(typeof(Action), this, "OnValueChanged");
            eventInfo.AddEventHandler(dataField, eventHandler);
            
            OnValueChanged();
        }

        protected void OnDestroy()
        {
            eventInfo.RemoveEventHandler(dataField, eventHandler);
        }
    }
}