using UnityEngine;

namespace VillageKeeper.UI
{
    public class WindmillSailsScript : MonoBehaviour
    {
        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Battle || Core.Instance.FSM.Current == FSM.States.Build)
                transform.Rotate(0, 0, -Core.Instance.Data.Common.Wind.Get() * Time.deltaTime * 10f);
        }
    }
}