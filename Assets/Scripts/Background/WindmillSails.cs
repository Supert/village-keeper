using UnityEngine;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class WindmillSails : MonoBehaviour
    {
        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Battle || Core.Instance.FSM.Current == FSM.States.Build)
                transform.Rotate(0, 0, -Data.Common.Wind * Time.deltaTime * 10f);
        }
    }
}