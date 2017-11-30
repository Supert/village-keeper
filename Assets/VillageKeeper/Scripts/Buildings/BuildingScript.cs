using UnityEngine;
using System;
using VillageKeeper.Data;

namespace VillageKeeper.Game
{
    public abstract partial class BuildingScript : MonoBehaviour
    {
        public abstract BuildingTypes Type { get; }

        public static BuildingScript GetNewBuildingOfType(BuildingTypes type)
        {
            var path = "Buildings/" + Enum.GetName(typeof(BuildingTypes), type);
            var bs = Instantiate(Resources.Load<BuildingScript>(path)) as BuildingScript;
            return bs;
        }

        public BuildingTileScript Tile;

        public float MaxHealth
        {
            get
            {
                switch (Type)
                {
                    case BuildingTypes.Farm:
                        return 2;
                    case BuildingTypes.WallStone:
                        return 6;
                    case BuildingTypes.WallWooden:
                        return 3;
                    case BuildingTypes.WatchtowerStone:
                        return 4;
                    case BuildingTypes.WatchtowerWooden:
                        return 3;
                    case BuildingTypes.Windmill:
                        return 6;
                    default:
                        return 0;
                }
            }
        }

        public int GoldCost
        {
            get
            {
                switch (Type)
                {
                    case BuildingTypes.Farm:
                        return 2;
                    case BuildingTypes.WallStone:
                        return 25;
                    case BuildingTypes.WallWooden:
                        return 2;
                    case BuildingTypes.WatchtowerStone:
                        return 50;
                    case BuildingTypes.WatchtowerWooden:
                        return 10;
                    case BuildingTypes.Windmill:
                        return 100;
                    default:
                        return 0;
                }
            }
        }

        public string HumanFriendlyName
        {
            get
            {
                switch (Type)
                {
                    case BuildingTypes.Farm:
                        return "Farm";
                    case BuildingTypes.WallStone:
                        return "Stone Wall";
                    case BuildingTypes.WallWooden:
                        return "Wooden Wall";
                    case BuildingTypes.WatchtowerStone:
                        return "Stone Tower";
                    case BuildingTypes.WatchtowerWooden:
                        return "Watchtower";
                    case BuildingTypes.Windmill:
                        return "Windmill";
                    default:
                        return "";
                }
            }
        }

        public string Description
        {
            get
            {
                switch (Type)
                {
                    case BuildingTypes.Farm:
                        return "Provides one point of food. Food converts to gold each round.";
                    case BuildingTypes.WallStone:
                        return "Steady stone wall.";
                    case BuildingTypes.WallWooden:
                        return "Cheap stockade can take few hits";
                    case BuildingTypes.WatchtowerStone:
                        return "Harder better faster stronger watchtower.";
                    case BuildingTypes.WatchtowerWooden:
                        return "Shoots at monster if it came close.";
                    case BuildingTypes.Windmill:
                        return "Provides extra food for each adjacent Farm at end of the round";
                    default:
                        return "";
                }
            }
        }

        private float? _health;
        public float Health
        {
            get
            {
                if (_health == null)
                {
                    _health = (float?)MaxHealth;
                }
                return (float)_health;
            }
            private set
            {
                _health = (float?)value;
                if (_health == 0)
                {
                    DestroySelf();
                }
            }
        }

        public void Damage()
        {
            Health--;
            CoreScript.Instance.AudioManager.PlayBuildingHit();
        }

        protected virtual void DestroySelf()
        {
            CoreScript.Instance.BuildingsArea.RemoveBuilding(this);
            Destroy(gameObject);
        }

        protected virtual void Update()
        {
            if (Tile == null)
            {
                if (Input.GetMouseButton(0))
                {
                    var lp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    lp.z = -9f;
                    transform.localPosition = lp;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    var buildingsArea = CoreScript.Instance.BuildingsArea;
                    var closestCell = buildingsArea.GetClosestGridPosition(transform.localPosition);
                    var closestCellPosition = buildingsArea.GetWorldPositionByGridPosition(closestCell);
                    var distance = (Vector2)transform.localPosition - closestCellPosition;
                    if (Mathf.Abs(distance.x) <= buildingsArea.CellWorldSize.x / 2 && Mathf.Abs(distance.y) <= buildingsArea.CellWorldSize.y / 2 && CoreScript.Instance.Data.Gold.Get() >= GoldCost)
                    {
                        buildingsArea.BuyBuilding(this, closestCell);
                    }
                    if (Tile == null)
                        gameObject.SetActive(false);

                }
            }
        }
    }
}