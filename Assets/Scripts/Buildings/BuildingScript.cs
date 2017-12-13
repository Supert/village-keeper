﻿using UnityEngine;
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

        public float MaxHealth { get { throw new NotImplementedException(); } }
        //public float MaxHealth { get { return BalanceData.GetBuildingMaxHealth(Type); } }

        public int GoldCost { get { throw new NotImplementedException(); } }
        //public int GoldCost { get { return BalanceData.GetBuildingGoldCost(Type); } }

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
                    if (Mathf.Abs(distance.x) <= buildingsArea.CellWorldSize.x / 2 && Mathf.Abs(distance.y) <= buildingsArea.CellWorldSize.y / 2 && Core.Instance.Data.Saved.Gold.Get() >= GoldCost)
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