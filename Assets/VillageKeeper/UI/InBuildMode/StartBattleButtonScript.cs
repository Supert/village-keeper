using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class StartBattleButtonScript : MonoBehaviour
    {
        private OffScreenMenuScript offScreenMenu;

        void Awake()
        {
            offScreenMenu = GetComponent<OffScreenMenuScript>() as OffScreenMenuScript;
        }

        void Start()
        {
            var button = GetComponent<Button>() as Button;
            button.onClick.AddListener(() =>
            {
                CoreScript.Instance.FSM.Event(new FSM.Args(FSM.Args.Types.GoToBattle));
                CoreScript.Instance.Audio.PlayClick();
            });
        }

        public void Show()
        {
            offScreenMenu.Show();
        }
    }
}