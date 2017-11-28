using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace VillageKeeper.Game
{
    [RequireComponent(typeof(Text))]
    public class GoldLabelScript : MonoBehaviour
    {
        private Text text;
        private void SetText()
        {
            text.text = CoreScript.Instance.Data.Gold.ToString();
        }

        void Awake()
        {
            text = GetComponent<Text>() as Text;
        }

        void Start()
        {
            CoreScript.Instance.GameStateChanged += (sender, e) => OnGameStateChanged(e);
            CoreScript.Instance.Data.DataFieldChanged += (sender, e) => OnDataFieldChanged(e);
        }

        void OnGameStateChanged(CoreScript.GameStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case CoreScript.GameStates.InBuildMode:
                    SetText();
                    break;
            }
        }

        void OnDataFieldChanged(DataScript.DataFieldChangedEventArgs e)
        {
            if (e.FieldChanged == DataScript.DataFields.Gold)
                SetText();
        }
    }
}