using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public Camera mainCamera;

    void Start()
    {
        mainCamera.aspect = Screen.width / Screen.height;
    }
}
