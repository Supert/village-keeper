using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageBindableView : ResourceBindableView<Sprite>
    {
        private Image image;

        protected void Awake()
        {
            image = GetComponent<Image>();
        }

        protected override void OnValueChanged()
        {
            image.sprite = GetResource();
        }
    }
}