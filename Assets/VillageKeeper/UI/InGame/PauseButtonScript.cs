using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class PauseButtonScript : MonoBehaviour
    {
        public Sprite inBattleSprite;
        public Sprite inBattlePressedSprite;
        public Sprite inBuildModeSprite;
        public Sprite inBuildModePressedSprite;
        private Button button;

        void Start()
        {
            button = GetComponent<Button>() as Button;

            //button.onClick.AddListener(() =>
            //{
            //    switch (CoreScript.Instance.GameState)
            //    {
            //        case CoreScript.GameStates.InBattle:
            //            CoreScript.Instance.GameState = CoreScript.GameStates.Paused;
            //            break;
            //        case CoreScript.GameStates.InBuildMode:
            //            CoreScript.Instance.GameState = CoreScript.GameStates.InMenu;
            //            break;
            //    }
            //    CoreScript.Instance.Audio.PlayClick();
            //});

            //CoreScript.Instance.GameStateChanged += (sender, e) =>
            //{
            //    var s = button.spriteState;
            //    switch (e.NewState)
            //    {
            //        case CoreScript.GameStates.InBattle:
            //            button.image.sprite = inBattleSprite;
            //            s.pressedSprite = inBattlePressedSprite;
            //            button.spriteState = s;
            //            break;
            //        case CoreScript.GameStates.InBuildMode:
            //            button.image.sprite = inBuildModeSprite;
            //            s = button.spriteState;
            //            s.pressedSprite = inBuildModePressedSprite;
            //            button.spriteState = s;
            //            break;
            //    }
            //};
        }
    }
}