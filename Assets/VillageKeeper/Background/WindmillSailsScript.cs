using UnityEngine;
using System.Collections;

public class WindmillSailsScript : MonoBehaviour
{
    void Update()
    {
        switch (CoreScript.Instance.GameState)
        {
            case CoreScript.GameStates.InBattle:
            case CoreScript.GameStates.InBuildMode:
                transform.Rotate(0, 0, -CoreScript.Instance.Wind.Strength * Time.deltaTime * 10);
                break;
        }
    }
}