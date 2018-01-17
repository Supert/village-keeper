using UnityEngine;
using System;
using VillageKeeper.Model;

namespace VillageKeeper.Game
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        protected BuildingTypes type;
        public BuildingTypes Type { get { return type; } }

        public BuildingTileScript Tile;

        public float MaxHealth { get { return Data.Balance.GetBuildingMaxHealth(type); } }

        public int GoldCost { get { return Data.Balance.GetBuildingGoldCost(type); } }

        public float Health { get; protected set; }

        protected virtual void Init()
        {
            Health = MaxHealth;
        }

        public void Damage()
        {
            Core.Instance.AudioManager.PlayBuildingHit();

            Health--;
            if (Health == 0)
            {
                DestroySelf();
            }
        }

        protected virtual void DestroySelf()
        {
            Core.Instance.BuildingsArea.RemoveBuilding(this);
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
                    var buildingsArea = Core.Instance.BuildingsArea;
                    var closestCell = buildingsArea.GetClosestGridPosition(transform.localPosition);
                    var closestCellPosition = buildingsArea.GetWorldPositionByGridPosition(closestCell);
                    var distance = (Vector2)transform.localPosition - closestCellPosition;
                    if (Mathf.Abs(distance.x) <= buildingsArea.CellWorldSize.x / 2 && Mathf.Abs(distance.y) <= buildingsArea.CellWorldSize.y / 2 && Data.Saved.Gold >= GoldCost)
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