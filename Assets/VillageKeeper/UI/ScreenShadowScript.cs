using UnityEngine;
using UnityEngine.UI;

public class ScreenShadowScript : MonoBehaviour
{
    public float animationTime;

    private bool isShown;
    private Button button;
    private Image image;


    public bool IsShown
    {
        get
        {
            return isShown;
        }
        private set
        {
            if (value)
                gameObject.SetActive(true);
            isShown = value;
        }
    }

    public void Show()
    {
        IsShown = true;
    }

    public void Hide()
    {
        IsShown = false;
    }

    public Button ShadowButton
    {
        get
        {
            if (button == null)
                button = GetComponent<Button>() as Button;
            return button;
        }
    }

    protected virtual void Start()
    {
        image = GetComponent<Image>() as Image;
    }

    protected virtual void Update()
    {
        if (IsShown)
        {
            if (image.color != Color.white)
            {
                image.color = Vector4.MoveTowards(image.color, Color.white, animationTime == 0 ? 1 : Time.deltaTime / animationTime);
            }
        }
        else
        {
            if (image.color == new Color(1f, 1f, 1f, 0f))
                gameObject.SetActive(false);
            else
                image.color = Vector4.MoveTowards(image.color, new Color(1f, 1f, 1f, 0f), animationTime == 0 ? 1 : Time.deltaTime / animationTime);
        }
    }
}
