using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace VillageKeeper.Game
{
    public class PauseMenuScript : OffScreenMenuScript
    {
        public Text title;
        public Button homeButton;
        public Button continueButton;
        public GameObject roundFinishedText;
        public Text WeCollectedFoodText;
        public Text WithMonsterBonusText;
        public Text YouGainGoldText;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(InitCoroutine());
        }

        private IEnumerator InitCoroutine()
        {
            yield return null;
            CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged(e);
            continueButton.onClick.AddListener(() =>
            {
                if (CoreScript.Instance.GameState == CoreScript.GameStates.Paused)
                    CoreScript.Instance.GameState = CoreScript.GameStates.InBattle;
                else
                    CoreScript.Instance.GameState = CoreScript.GameStates.InBuildMode;
                CoreScript.Instance.Audio.PlayClick();
            });
            homeButton.onClick.AddListener(() => CoreScript.Instance.GameState = CoreScript.GameStates.InMenu);
            CoreScript.Instance.Audio.PlayClick();
        }

        void OnGameStateChanged(CoreScript.GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case (CoreScript.GameStates.Paused):
                    Show();
                    title.text = "Pause";
                    roundFinishedText.SetActive(false);
                    break;
                case (CoreScript.GameStates.RoundFinished):
                    Show();
                    title.text = "Victory!";
                    roundFinishedText.SetActive(true);
                    WeCollectedFoodText.text = "We collected " + (CoreScript.Instance.Data.GetFarmsFood() + CoreScript.Instance.Data.GetWindmillBonusFood()).ToString();
                    WithMonsterBonusText.text = "With " + CoreScript.Instance.Data.GetMonsterBonusGold().ToString();
                    YouGainGoldText.text = "you gain " + CoreScript.Instance.Data.GetRoundFinishedBonusGold().ToString();
                    break;
                default:
                    Hide();
                    break;
            }
        }
    }
}