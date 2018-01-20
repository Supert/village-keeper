using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class StateFadeImageView : StateVisibleView
    {
        private Image image;

        protected override void Start()
        {
            base.Start();
            image = GetComponent<Image>() as Image;
        }

        protected override void ShowUpdate()
        {
            image.color = Vector4.MoveTowards(image.color, Color.white, animationDuration == 0 ? 1f : (Time.time - animationStartTime) / animationDuration);
        }

        protected override void HideUpdate()
        {
            if (image.color == new Color(1f, 1f, 1f, 0f))
                gameObject.SetActive(false);
            else
                image.color = Vector4.MoveTowards(image.color, new Color(1f, 1f, 1f, 0f), animationDuration == 0 ? 1f : (Time.time - animationStartTime) / animationDuration);
        }
    }
}