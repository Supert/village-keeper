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
            if (Core.Instance.SavedData.IsSoundEffectsEnabled.Get())
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
            if (Core.Instance.SavedData.IsMusicEnabled.Get())
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
            Core.Instance.SavedData.IsSoundEffectsEnabled.OnValueChanged += SetButtons;
            Core.Instance.SavedData.IsMusicEnabled.OnValueChanged += SetButtons;

            MusicButton.onClick.AddListener(() =>
            {
                Core.Instance.SavedData.IsMusicEnabled.Set(!Core.Instance.SavedData.IsMusicEnabled.Get());
                Core.Instance.AudioManager.PlayClick();
            });

            SoundsButton.onClick.AddListener(() =>
            {
                Core.Instance.SavedData.IsSoundEffectsEnabled.Set(!Core.Instance.SavedData.IsSoundEffectsEnabled.Get());
                Core.Instance.AudioManager.PlayClick();
            });
        }
    }
}