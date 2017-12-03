using UnityEngine;
using System.Linq;

namespace VillageKeeper.UI
{
    public abstract class ResourceBindableView<T> : BindableView where T : Object
    {
        [SerializeField, Header("Resource Path Entry")]
        protected BindableIds resourceDataIds;

        protected FieldInfo ResourcePathField { get; private set; }

        protected virtual string FullResourcePath
        {
            get
            {
                return string.Format(ResourcePathField.GetValue().ToString(), Fields.Select(f => f.GetValue()).ToArray());
            }
        }

        public T GetResource()
        {
            return Resources.Load<T>(FullResourcePath);
        }

        protected override void Start()
        {
            ResourcePathField = GetField(resourceDataIds);
            base.Start();
        }
    }
}