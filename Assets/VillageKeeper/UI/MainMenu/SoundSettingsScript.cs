using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class SoundSettingsScript : MonoBehaviour
    {
        public Sprite MusicOffSprite;
        public Sprite MusicOffPressedSprite;
        public Sprite MusicOnSprite;
        public Sprite MusicOnPressedSprite;
        public Sprite SoundsOffSprite;
        public Sprite SoundsOffPressedSprite;
        public Sprite SoundsOnSprite;
        public Sprite SoundsOnPressedSprite;
        public Button MusicButton;
        public Button SoundsButton;

        void SetButtons()
        {
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled)
            {
                SoundsButton.image.sprite = SoundsOnSprite;
                var s = SoundsButton.spriteState;
                s.pressedSprite = SoundsOnPressedSprite;
                SoundsButton.spriteState = s;
            }
            else
            {
                SoundsButton.image.sprite = SoundsOffSprite;
                var s = SoundsButton.spriteState;
                s.pressedSprite = SoundsOffPressedSprite;
                SoundsButton.spriteState = s;
            }
            if (CoreScript.Instance.Data.IsMusicEnabled)
            {
                MusicButton.image.sprite = MusicOnSprite;
                var s = MusicButton.spriteState;
                s.pressedSprite = MusicOnPressedSprite;
                MusicButton.spriteState = s;
            }
            else
            {
                MusicButton.image.sprite = MusicOffSprite;
                var s = MusicButton.spriteState;
                s.pressedSprite = MusicOffPressedSprite;
                MusicButton.spriteState = s;
            }
        }

        void Start()
        {
            CoreScript.Instance.Data.DataFieldChanged += (sender, e) =>
            {
                switch (e.FieldChanged)
                {
                    case DataScript.DataFields.IsSoundEffectsEnabled:
                    case DataScript.DataFields.IsMusicEnabled:
                        SetButtons();
                        break;
                }
            };
            MusicButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.Data.IsMusicEnabled = !CoreScript.Instance.Data.IsMusicEnabled;
                CoreScript.Instance.AudioManager.PlayClick();
            });
            SoundsButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.Data.IsSoundEffectsEnabled = !CoreScript.Instance.Data.IsSoundEffectsEnabled;
                CoreScript.Instance.AudioManager.PlayClick();
            });
        }
    }
}