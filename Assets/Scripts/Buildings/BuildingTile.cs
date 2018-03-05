using UnityEngine;
using UnityEngine.UI;

namespace VillageKeeper.Game
{
    [RequireComponent(typeof(Image))]
    public class BuildingTile : MonoBehaviour
    {
        public enum States
        {
            Active,
            Highlighted,
            Disabled,
        }
        
        private Image image;

        public int GridX { get; set; }
        public int GridY { get; set; }
        public Building Building { get; set; }

        private States state;
        public States State
        {
            get
            {
                return state;
            }
            set
            {
                switch (value)
                {
                    case States.Active:
                        gameObject.SetActive(true);
                        image.sprite = Core.Data.Resources.BuildingTileDefaultSprite;
                        break;
                    case States.Disabled:
                        gameObject.SetActive(false);
                        break;
                    case States.Highlighted:
                        gameObject.SetActive(true);
                        image.sprite = Core.Data.Resources.BuildingTileHighlightedSprite;
                        break;
                }
                state = value;
            }
        }

        void Awake()
        {
            image = GetComponent<Image>();
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Build)
            {
                image.color = Vector4.MoveTowards(image.color, new Vector4(1, 1, 1, 1), Time.deltaTime / 0.25f);
            }
            else if (Core.Instance.FSM.Current == FSM.States.Battle)
            {
                image.color = Vector4.MoveTowards(image.color, new Vector4(1, 1, 1, 0), Time.deltaTime / 0.25f);
            }
        }
    }
}