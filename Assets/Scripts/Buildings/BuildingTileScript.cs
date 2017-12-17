using UnityEngine;

namespace VillageKeeper.Game
{
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
        private SpriteRenderer spriteRenderer;

        public int gridX;
        public int gridY;
        public Building Building = null;

        private BuildingTileStates state;
        public BuildingTileStates State
        {
            get
            {
                return state;
            }
            set
            {
                switch (value)
                {
                    case BuildingTileStates.Active:
                        gameObject.SetActive(true);
                        spriteRenderer.sprite = tileSpriteDefault;
                        break;
                    case BuildingTileStates.Disabled:
                        gameObject.SetActive(false);
                        break;
                    case BuildingTileStates.Highlightened:
                        gameObject.SetActive(true);
                        spriteRenderer.sprite = tileSpriteHighlighted;
                        break;
                }
                state = value;
            }
        }

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        void Start()
        {
            transform.localScale *= Core.Instance.BuildingsArea.CellWorldSize.x / spriteRenderer.bounds.size.x * 0.9f;
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Build)
            {
                spriteRenderer.color = Vector4.MoveTowards(spriteRenderer.color, new Vector4(1, 1, 1, 1), Time.deltaTime / 0.25f);
            }
            else if (Core.Instance.FSM.Current == FSM.States.Battle)
            {
                spriteRenderer.color = Vector4.MoveTowards(spriteRenderer.color, new Vector4(1, 1, 1, 0), Time.deltaTime / 0.25f);
            }
        }
    }
}