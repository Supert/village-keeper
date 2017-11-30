using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private SpriteRenderer _sprite;

    private bool _isGoingToFade = false;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>() as SpriteRenderer;
        _sprite.color = new Vector4(1, 1, 1, 0);
        _sprite.sortingLayerName = "Characters";
        _sprite.sortingOrder = 1;
    }

    void Update()
    {
        if (_isGoingToFade)
        {
            var lp = transform.localPosition;
            lp.y += 3f * Time.deltaTime;
            transform.localPosition = lp;
            if (_sprite.color.a != 0)
                _sprite.color = Vector4.MoveTowards(_sprite.color, new Vector4(1, 1, 1, 0), Time.deltaTime / 3f);
            else
                gameObject.SetActive(false);
        }
        else if (_sprite.color.a != 1)
        {
            _sprite.color = Vector4.MoveTowards(_sprite.color, new Vector4(1, 1, 1, 1), Time.deltaTime / 0.25f);
        }
        else
        {
            _isGoingToFade = true;
        }
    }
}
