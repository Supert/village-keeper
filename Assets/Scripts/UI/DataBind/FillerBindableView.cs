using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    [RequireComponent(typeof(Image))]
    public class FillerBindableView : BindableView
    {
        private Image image;

        protected override void Start()
        {
            image = GetComponent<Image>();
            base.Start();

        }
        protected override void OnValueChanged()
        {
            image.fillAmount = Mathf.Clamp01((float) Fields[0].GetValue());
        }
    }
}