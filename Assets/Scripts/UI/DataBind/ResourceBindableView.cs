using UnityEngine;
using System.Linq;

namespace Shibari.UI
{
    public abstract class ResourceBindableView<T> : BindableView
    {
        [SerializeField, Header("Resource Path Entry")]
        protected BindableIds resourceDataIds;

        protected PrimaryValueInfo ResourcePathField { get; private set; }

        protected virtual string FullResourcePath
        {
            get
            {
                return string.Format(ResourcePathField.GetValue().ToString(), Fields.Select(f => f.GetValue()).ToArray());
            }
        }

        public abstract T GetResource();

        protected override void Start()
        {
            ResourcePathField = GetField(resourceDataIds);
            base.Start();
        }
    }
}