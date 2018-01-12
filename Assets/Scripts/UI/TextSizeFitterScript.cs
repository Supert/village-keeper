using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class TextSizeFitterScript : MonoBehaviour
{
    [SerializeField]
    protected List<Text> textsToBeFitted;

    private void Awake()
    {
        var text = GetComponent<Text>() as Text;
        text.color = new Color(1, 1, 1, 0);
        foreach (var t in textsToBeFitted)
        {
            t.fontSize = text.cachedTextGenerator.fontSizeUsedForBestFit;
        }
        gameObject.SetActive(false);
    }
}