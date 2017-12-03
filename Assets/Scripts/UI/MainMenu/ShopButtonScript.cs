using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.UI
{
    public class ShopButtonScript : MonoBehaviour
    {
        void Start()
        {
            var button = GetComponent<Button>() as Button;
            button.onClick.AddListener(() =>
            {
                Core.Instance.FSM.Event(FSM.StateMachineEvents.GoToShop);
                Core.Instance.AudioManager.PlayClick();
            });
        }
    }
}