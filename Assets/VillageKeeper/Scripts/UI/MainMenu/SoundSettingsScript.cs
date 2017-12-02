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
            if (CoreScript.Instance.CommonData.IsSoundEffectsEnabled.Get())
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
            if (CoreScript.Instance.CommonData.IsMusicEnabled.Get())
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
            CoreScript.Instance.CommonData.IsSoundEffectsEnabled.OnValueChanged += SetButtons;
            CoreScript.Instance.CommonData.IsMusicEnabled.OnValueChanged += SetButtons;

            MusicButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.CommonData.IsMusicEnabled.Set(!CoreScript.Instance.CommonData.IsMusicEnabled.Get());
                CoreScript.Instance.AudioManager.PlayClick();
            });

            SoundsButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.CommonData.IsSoundEffectsEnabled.Set(!CoreScript.Instance.CommonData.IsSoundEffectsEnabled.Get());
                CoreScript.Instance.AudioManager.PlayClick();
            });
        }
    }
}