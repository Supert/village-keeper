using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class BackgroundScript : MonoBehaviour
    {
        public Image mountains;
        public Image village;
        public Sprite winterMountains;
        public Sprite winterVillage;

        void Start()
        {
            if (CoreScript.Instance.TodaySpecial == CoreScript.Specials.Winter)
            {
                mountains.sprite = winterMountains;
                village.sprite = winterVillage;
            }
        }
    }
}