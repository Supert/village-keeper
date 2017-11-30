using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Text))]
public class TextSizeFitterScript : MonoBehaviour
{
    public List<Text> textsToBeFitted;

    void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return null;
        var text = GetComponent<Text>() as Text;
        text.color = new Color(1, 1, 1, 0);
        foreach (var t in textsToBeFitted)
        {
            t.fontSize = text.cachedTextGenerator.fontSizeUsedForBestFit;
        }
        Canvas.ForceUpdateCanvases();
        gameObject.SetActive(false);
    }
}