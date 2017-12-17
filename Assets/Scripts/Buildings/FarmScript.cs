using UnityEngine;
using VillageKeeper.Data;

namespace VillageKeeper.Game
{
    public class FarmScript : Building
    {
        public SpriteRenderer cropsSR;
        public Sprite winterSpecialCropsSprite;

        protected void Start()
        {
            Debug.Log("FIX");
            //if (CoreScript.Instance.TodaySpecial == CoreScript.Specials.Winter)
            //    cropsSR.sprite = winterSpecialCropsSprite;
        }
    }
}