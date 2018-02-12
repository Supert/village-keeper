using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.Model;

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
            if (Core.Data.Saved.IsSoundEffectsEnabled.Get())
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
            if (Core.Data.Saved.IsMusicEnabled.Get())
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
            Core.Data.Saved.IsSoundEffectsEnabled.OnValueChanged += SetButtons;
            Core.Data.Saved.IsMusicEnabled.OnValueChanged += SetButtons;

            MusicButton.onClick.AddListener(() =>
            {
                Core.Data.Saved.IsMusicEnabled.Set(!Core.Data.Saved.IsMusicEnabled.Get());
                Core.Instance.AudioManager.PlayClick();
            });

            SoundsButton.onClick.AddListener(() =>
            {
                Core.Data.Saved.IsSoundEffectsEnabled.Set(!Core.Data.Saved.IsSoundEffectsEnabled.Get());
                Core.Instance.AudioManager.PlayClick();
            });
        }
    }
}