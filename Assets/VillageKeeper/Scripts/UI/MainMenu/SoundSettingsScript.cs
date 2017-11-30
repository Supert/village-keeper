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
            if (CoreScript.Instance.Data.IsSoundEffectsEnabled.Get())
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
            if (CoreScript.Instance.Data.IsMusicEnabled.Get())
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

        void Init()
        {
            CoreScript.Instance.Data.IsSoundEffectsEnabled.OnValueChanged += (b) => SetButtons();
            CoreScript.Instance.Data.IsMusicEnabled.OnValueChanged += (b) => SetButtons();

            MusicButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.Data.IsMusicEnabled.Set(!CoreScript.Instance.Data.IsMusicEnabled.Get());
                CoreScript.Instance.AudioManager.PlayClick();
            });

            SoundsButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.Data.IsSoundEffectsEnabled.Set(!CoreScript.Instance.Data.IsSoundEffectsEnabled.Get());
                CoreScript.Instance.AudioManager.PlayClick();
            });
        }
    }
}