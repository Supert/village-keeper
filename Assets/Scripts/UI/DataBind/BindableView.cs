using UnityEngine;
using System;

namespace Shibari.UI
{
    public abstract partial class BindableView : MonoBehaviour
    {
        [SerializeField]
        private BindableIds[] dataEntries = new BindableIds[0];

        protected BindableFieldInfo[] Fields { get; private set; }

        protected abstract void OnValueChanged();

        protected Delegate onValueChangedDelegate;

        protected virtual void Start()
        {
            onValueChangedDelegate = Delegate.CreateDelegate(typeof(Action), this, "OnValueChanged");
            Fields = new BindableFieldInfo[dataEntries.Length];

            for (int i = 0; i < dataEntries.Length; i++)
            {
                Fields[i] = GetField(dataEntries[i]);
                Fields[i].EventInfo.AddEventHandler(Fields[i].DataField, onValueChangedDelegate);
            }

            OnValueChanged();
        }

        protected BindableFieldInfo GetField(BindableIds ids)
        {
            try
            {
                return Model.Get<BindableData>(ids.dataId).ReflectedProperties[ids.fieldId];
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        protected void OnDestroy()
        {
            if (Fields != null)
            {
                foreach (var field in Fields)
                {
                    field.EventInfo.RemoveEventHandler(field.DataField, onValueChangedDelegate);
                }
            }
        }
    }
}