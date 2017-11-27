using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowLoadBarScript : BarScript
{
    public Sprite fullyLoadedBarFillerSprite;
    public Sprite partiallyLoadedBarFillerSprite;
    private RectTransform rectTransform;
    private Image fillerImage;

    protected override void Start()
    {
        base.Start();
        fillerImage = barFillerImage.GetComponent<Image>() as Image;

        rectTransform = GetComponent<RectTransform>() as RectTransform;
        MaxValue = rectTransform.rect.width / rectTransform.localScale.x;
        minValue = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (CoreScript.Instance.GameState == CoreScript.GameStates.InBattle)
        {
            if (!Input.GetMouseButton(0) && RelativeCurrentValue < 1)
                RelativeCurrentValue -= Time.deltaTime * 5;
            else
                CurrentValue = -CoreScript.Instance.Controls.TouchDeltaPosition.x * 3f;
            if (RelativeCurrentValue == 1f)
            {
                if (fillerImage.sprite != fullyLoadedBarFillerSprite)
                    fillerImage.sprite = fullyLoadedBarFillerSprite;
            }
            else
            {
                if (fillerImage.sprite != partiallyLoadedBarFillerSprite)
                    fillerImage.sprite = partiallyLoadedBarFillerSprite;
            }
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
}

