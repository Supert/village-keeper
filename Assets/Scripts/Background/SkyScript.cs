using UnityEngine;
using System.Collections.Generic;

namespace VillageKeeper.UI
{
    public class SkyScript : MonoBehaviour
    {
        public GameObject Clouds;
        private List<RectTransform> cloudsList = new List<RectTransform>();

        void Start()
        {
            cloudsList.Add(Clouds.GetComponent<RectTransform>());
            cloudsList.Add(Instantiate(Clouds).GetComponent<RectTransform>());
            for (int i = 0; i < cloudsList.Count; i++)
            {
                cloudsList[i].SetParent(transform.parent, false);
                var ap = cloudsList[i].anchoredPosition;
                ap.x = (i - 1) * cloudsList[i].rect.width;
                cloudsList[i].anchoredPosition = ap;
            }
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Build || Core.Instance.FSM.Current == FSM.States.Battle)
            {
                foreach (var c in cloudsList)
                {
                    var ap = c.anchoredPosition;
                    ap.x += Core.Instance.Data.Common.Wind.Get() * Time.deltaTime * 10;
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