using UnityEngine;
using VillageKeeper.Data;

namespace VillageKeeper.Game
{
    public class FarmScript : BuildingScript
    {
        public override BuildingTypes Type { get { return BuildingTypes.Farm; } }

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