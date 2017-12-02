using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Image barFillerImage;
    private float maxValue = 1f;
    public float MaxValue
    {
        get
        {
            return maxValue;
        }
        set
        {
            maxValue = value;
            CurrentValue = CurrentValue;
        }
    }

    public float minValue;
    private float currentValue;
    public float RelativeCurrentValue
    {
        get
        {
            return (currentValue - minValue) / (MaxValue - minValue);
        }
        set
        {
            CurrentValue = value * (MaxValue - minValue) + minValue;
        }
    }

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = Mathf.Clamp(value, minValue, MaxValue);
            if (MaxValue - minValue != 0)
            {
                currentValue = value;
                barFillerImage.fillAmount = RelativeCurrentValue;
            }
        }
    }

    //I can remember why I did this. My friend's noname device was drawing blue healthbar instead of red one by some reason.
    protected virtual void Awake()
    {
        barFillerImage.color = new Color(0, 0, 0, 0);
    }

    protected virtual void Start()
    {
        barFillerImage.color = new Color(1, 1, 1, 1);
    }
}