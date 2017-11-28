using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using VillageKeeper.Game;

namespace VillageKeeper.UI
{
    public class CastleScript : MonoBehaviour
    {
        private Image image;
        public List<Sprite> castleSprites;
        public List<Sprite> castleWinterSpecialSprites;
        public void SetSprite()
        {
            switch (CoreScript.Instance.TodaySpecial)
            {
                case CoreScript.Specials.None:
                    if (image.sprite != castleSprites[CoreScript.Instance.Data.VillageLevel])
                        image.sprite = castleSprites[CoreScript.Instance.Data.VillageLevel];
                    break;
                case CoreScript.Specials.Winter:
                    if (image.sprite != castleWinterSpecialSprites[CoreScript.Instance.Data.VillageLevel])
                        image.sprite = castleWinterSpecialSprites[CoreScript.Instance.Data.VillageLevel];
                    break;
            }
        }

        void Awake()
        {
            image = GetComponent<Image>() as Image;
        }

        void OnDataFieldChanged(DataScript.DataFieldChangedEventArgs e)
        {
            switch (e.FieldChanged)
            {
                case DataScript.DataFields.VillageLevel:
                    SetSprite();
                    break;
            }
        }

        void Start()
        {
            CoreScript.Instance.Data.DataFieldChanged += (sender, e) => OnDataFieldChanged(e);
        }
    }
}