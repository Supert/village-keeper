using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageDataBindView : DataBindView
    {
        [SerializeField]
        protected string resourcePath;

        protected virtual string FullResourcePath
        {
            get
            {
                return resourcePath + "/" + GetValue().ToString();
            }
        }

        private Image image;

        protected void Awake()
        {
            image = GetComponent<Image>();
        }


        protected override void OnValueChanged()
        {
            image.sprite = Resources.Load<Sprite>(FullResourcePath);
        }
    }
}