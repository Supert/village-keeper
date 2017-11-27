using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class BuildingTileScript : MonoBehaviour
{
    public enum BuildingTileStates
    {
        Active,
        Highlightened,
        Disabled,
    }

    public Sprite tileSpriteDefault;
    public Sprite tileSpriteHighlighted;
    private SpriteRenderer _spriteRenderer;

    public int gridX;
    public int gridY;
    public BuildingScript Building = null;

    private BuildingTileStates _state;
    public BuildingTileStates State
    {
        get
        {
            return _state;
        }
        set
        {
            switch (value)
            {
                case BuildingTileStates.Active:
                    gameObject.SetActive(true);
                    _spriteRenderer.sprite = tileSpriteDefault;
                    break;
                case BuildingTileStates.Disabled:
                    gameObject.SetActive(false);
                    break;
                case BuildingTileStates.Highlightened:
                    gameObject.SetActive(true);
                    _spriteRenderer.sprite = tileSpriteHighlighted;
                    break;
            }
            _state = value;
        }
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
    }

    void Start()
    {
        transform.localScale *= CoreScript.Instance.BuildingsArea.CellWorldSize.x / _spriteRenderer.bounds.size.x * 0.9f;
    }

    void Update()
    {
        switch (CoreScript.Instance.GameState)
        {
            case CoreScript.GameStates.InBuildMode:
                _spriteRenderer.color = Vector4.MoveTowards(_spriteRenderer.color, new Vector4(1, 1, 1, 1), Time.deltaTime / 0.25f);
                break;
            case CoreScript.GameStates.InBattle:
                _spriteRenderer.color = Vector4.MoveTowards(_spriteRenderer.color, new Vector4(1, 1, 1, 0), Time.deltaTime / 0.25f);
                break;
        }
    }
}
