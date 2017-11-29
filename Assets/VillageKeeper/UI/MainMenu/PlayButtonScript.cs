using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class PlayButtonScript : MonoBehaviour
    {
        void Start()
        {
            var button = GetComponent<Button>() as Button;
            button.onClick.AddListener(() =>
            {
                CoreScript.Instance.FSM.Event(new FSM.Args(FSM.Args.Types.GoToBuild));
                CoreScript.Instance.Audio.PlayClick();
            });
        }
    }
}