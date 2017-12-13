using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace VillageKeeper.UI
{
    public class SkyScript : MonoBehaviour
    {
        public GameObject Clouds;
        private List<RectTransform> _cloudsList = new List<RectTransform>();

        void Start()
        {
            _cloudsList.Add(Clouds.GetComponent<RectTransform>());
            _cloudsList.Add(Instantiate<GameObject>(Clouds).GetComponent<RectTransform>());
            for (int i = 0; i < _cloudsList.Count; i++)
            {
                _cloudsList[i].SetParent(transform.parent, false);
                var ap = _cloudsList[i].anchoredPosition;
                ap.x = (i - 1) * _cloudsList[i].rect.width;
                _cloudsList[i].anchoredPosition = ap;

            }
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Build || Core.Instance.FSM.Current == FSM.States.Battle)
            {
                foreach (var c in _cloudsList)
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