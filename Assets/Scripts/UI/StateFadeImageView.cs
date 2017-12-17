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

        protected override void AnimationUpdate()
        {
            if (IsShown)
            {
                if (image.color != Color.white)
                {
                    image.color = Vector4.MoveTowards(image.color, Color.white, animationTime == 0 ? 1 : Time.deltaTime / animationTime);
                }
            }
            else
            {
                if (image.color == new Color(1f, 1f, 1f, 0f))
                    gameObject.SetActive(false);
                else
                    image.color = Vector4.MoveTowards(image.color, new Color(1f, 1f, 1f, 0f), animationTime == 0 ? 1 : Time.deltaTime / animationTime);
            }
        }
    }
}