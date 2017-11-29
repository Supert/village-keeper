using UnityEngine;

namespace VillageKeeper.UI
{
    public class WindmillSailsScript : MonoBehaviour
    {
        void Update()
        {
            if (CoreScript.Instance.FSM.Current == typeof(FSM.BattleState) || CoreScript.Instance.FSM.Current == typeof(FSM.BuildState))
                transform.Rotate(0, 0, -CoreScript.Instance.Wind.Strength * Time.deltaTime * 10);
        }
    }
}