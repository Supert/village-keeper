using UnityEngine;
using System.Collections;

namespace VillageKeeper.Game
{
    public class UiScript : MonoBehaviour
    {
        public HelpMenuScript helpMenu;

        void Start()
        {
            CoreScript.Instance.GameStateChanged += (sender, e) => StartCoroutine(OnGameStateChangedCoroutine(e));
        }

        private IEnumerator OnGameStateChangedCoroutine(CoreScript.GameStateChangedEventArgs e)
        {
            yield return null;
            helpMenu.OnGameStateChanged(e);
        }
    }
}