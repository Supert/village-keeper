using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.Game
{
    public class Ghost : MonoBehaviour
    {
        private Image image;

        private bool isFading = false;

        void Awake()
        {
            image = GetComponent<Image>() as Image;
            image.color = new Vector4(1f, 1f, 1f, 0f);
        }

        void Update()
        {
            if (isFading)
            {
                var lp = transform.localPosition;
                lp.y += 3f * Time.deltaTime;
                transform.localPosition = lp;
                if (image.color.a > 0f)
                    image.color = Vector4.MoveTowards(image.color, new Vector4(1, 1, 1, 0), Time.deltaTime / 3f);
                else
                    gameObject.SetActive(false);
            }
            else if (image.color.a < 1f)
            {
                image.color = Vector4.MoveTowards(image.color, new Vector4(1, 1, 1, 1), Time.deltaTime / 0.25f);
            }
            else
            {
                isFading = true;
            }
        }
    }
}