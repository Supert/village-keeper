﻿using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.FSM;

namespace VillageKeeper.UI
{
    public class HelpMenuScript : MonoBehaviour
    {
        public Text tipText;
        public Text tipCounterText;
        public Button closeButton;
        public Button previousButton;
        public Button nextButton;

        private string[] currentTips;

        private void Start()
        {
            nextButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.GameData.CurrentHelpTip.Set(CoreScript.Instance.GameData.CurrentHelpTip.Get() + 1);
                CoreScript.Instance.AudioManager.PlayClick();
            });

            previousButton.onClick.AddListener(() =>
            {
                CoreScript.Instance.GameData.CurrentHelpTip.Set(CoreScript.Instance.GameData.CurrentHelpTip.Get() - 1);
                CoreScript.Instance.AudioManager.PlayClick();
            });

            CoreScript.Instance.GameData.CurrentHelpTip.OnValueChanged += () =>
            {
                tipText.text = currentTips[CoreScript.Instance.GameData.CurrentHelpTip.Get()];
                tipCounterText.text = "Tip " + (CoreScript.Instance.GameData.CurrentHelpTip.Get() + 1).ToString() + "/" + currentTips.Length;
            };

            CoreScript.Instance.FSM.SubscribeToEnter(States.BattleHelp, ShowBattle);
            CoreScript.Instance.FSM.SubscribeToEnter(States.BuildHelp, ShowBuild);
        }

        private void ShowBattle()
        {
            currentTips = CoreScript.Instance.Localization.GetBattleHelpTips();
            Show();
        }

        private void ShowBuild()
        {
            currentTips = CoreScript.Instance.Localization.GetBuildHelpTips();
            Show();
        }

        private void Show()
        {
            CoreScript.Instance.GameData.CurrentHelpTip.Set(0);
        }
    }
}