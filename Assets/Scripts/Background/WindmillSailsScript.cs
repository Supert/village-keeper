using UnityEngine;

namespace VillageKeeper.UI
{
    public class WindmillSailsScript : MonoBehaviour
    {
        void Update()
        {
            if (CoreScript.Instance.FSM.Current == FSM.States.Battle || CoreScript.Instance.FSM.Current == FSM.States.Build)
                transform.Rotate(0, 0, -CoreScript.Instance.UiManager.Wind.Strength * Time.deltaTime * 10);
        }
    }
}