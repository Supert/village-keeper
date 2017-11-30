using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class CastleScript : MonoBehaviour
    {
        public List<Sprite> castleSprites;
        public List<Sprite> castleWinterSpecialSprites;

        private Image image;

        public void SetSprite()
        {
            switch (CoreScript.Instance.TodaySpecial)
            {
                case CoreScript.Specials.None:
                    if (image.sprite != castleSprites[CoreScript.Instance.Data.VillageLevel.Get()])
                        image.sprite = castleSprites[CoreScript.Instance.Data.VillageLevel.Get()];
                    break;
                case CoreScript.Specials.Winter:
                    if (image.sprite != castleWinterSpecialSprites[CoreScript.Instance.Data.VillageLevel.Get()])
                        image.sprite = castleWinterSpecialSprites[CoreScript.Instance.Data.VillageLevel.Get()];
                    break;
            }
        }

        void Init()
        {
            image = GetComponent<Image>() as Image;
            CoreScript.Instance.Data.VillageLevel.OnValueChanged += (level) => SetSprite();
        }
    }
}