using UnityEngine;
using VillageKeeper.Model;

namespace VillageKeeper.Game
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private BuildingTypes type;
        public BuildingTypes Type { get { return type; } }

        public BuildingTile Tile;

        public float MaxHealth { get { return Core.Data.Balance.GetBuildingMaxHealth(Type); } }

        public int GoldCost { get { return Core.Data.Balance.GetBuildingGoldCost(Type); } }

        public float Health { get; protected set; }

        public virtual void Initialize()
        {
            Health = MaxHealth;
        }

        public void Damage()
        {
            Core.Instance.AudioManager.PlayBuildingHit();

            Health--;
            if (Health <= 0)
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