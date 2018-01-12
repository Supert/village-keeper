using UnityEngine;
using System.Collections.Generic;

namespace VillageKeeper.UI
{
    public class Sky : MonoBehaviour
    {
        private Wind wind;

        private List<RectTransform> cloudsList = new List<RectTransform>();

        private void Awake()
        {
            wind = new Wind(6f, 3f, 3f, 6f);
        }

        private void Start()
        {
            RectTransform clouds = transform.Find("Clouds").GetComponent<RectTransform>();
            cloudsList.Add(clouds);
            cloudsList.Add(Instantiate(clouds));
            for (int i = 0; i < cloudsList.Count; i++)
            {
                cloudsList[i].SetParent(transform.parent, false);
                var ap = cloudsList[i].anchoredPosition;
                ap.x = (i - 1) * cloudsList[i].rect.width;
                cloudsList[i].anchoredPosition = ap;
            }
        }

        private void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Build || Core.Instance.FSM.Current == FSM.States.Battle)
            {
                wind.Update();

                foreach (var c in cloudsList)
                {
                    var ap = c.anchoredPosition;
                    ap.x += wind.Strength * Time.deltaTime * 10;
                    if (ap.x < -c.rect.width)
                        ap.x += c.rect.width * 2;
                    if (ap.x > c.rect.width)
                    {
                        ap.x -= c.rect.width * 2;
                    }
                    c.anchoredPosition = ap;
                }
            }
        }
    }
}