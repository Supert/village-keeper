using UnityEngine;

public class Ghost : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool isFading = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
        spriteRenderer.color = new Vector4(1, 1, 1, 0);
        spriteRenderer.sortingLayerName = "Characters";
        spriteRenderer.sortingOrder = 1;
    }

    void Update()
    {
        if (isFading)
        {
            var lp = transform.localPosition;
            lp.y += 3f * Time.deltaTime;
            transform.localPosition = lp;
            if (spriteRenderer.color.a != 0)
                spriteRenderer.color = Vector4.MoveTowards(spriteRenderer.color, new Vector4(1, 1, 1, 0), Time.deltaTime / 3f);
            else
                gameObject.SetActive(false);
        }
        else if (spriteRenderer.color.a != 1)
        {
            spriteRenderer.color = Vector4.MoveTowards(spriteRenderer.color, new Vector4(1, 1, 1, 1), Time.deltaTime / 0.25f);
        }
        else
        {
            isFading = true;
        }
    }
}
