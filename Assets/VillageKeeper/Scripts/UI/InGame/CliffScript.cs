using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class CliffScript : MonoBehaviour
    {
        public Sprite winterSprite;

        void Start()
        {
            if (CoreScript.Instance.TodaySpecial == CoreScript.Specials.Winter)
            {
                var image = GetComponent<Image>() as Image;
                image.sprite = winterSprite;
            }
            var offscreen = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
            //CoreScript.Instance.GameStateChanged += (sender, e) =>
            //{
            //    switch (e.NewState)
            //    {
            //        case CoreScript.GameStates.InBattle:
            //        case CoreScript.GameStates.Paused:
            //            offscreen.Show();
            //            break;
            //        case CoreScript.GameStates.InHelp:
            //            break;
            //        default:
            //            offscreen.Hide();
            //            break;
            //    }
            //};
        }
    }
}