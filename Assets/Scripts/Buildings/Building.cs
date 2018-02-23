using UnityEngine;
using VillageKeeper.Model;

namespace VillageKeeper.Game
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        protected BuildingTypes type;
        public BuildingTypes Type { get { return type; } }

        public BuildingTileScript Tile;

        public float MaxHealth { get { return Core.Data.Balance.GetBuildingMaxHealth(type); } }

        public int GoldCost { get { return Core.Data.Balance.GetBuildingGoldCost(type); } }

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
    }
}