using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Canvas))]
public class ScalerScript : MonoBehaviour
{
    public RectTransform cliffAreaRT;
    public RectTransform archerRT;

    void Start()
    {
        archerRT.anchoredPosition = new Vector2(cliffAreaRT.rect.width * 0.7f, 0);
    }
}