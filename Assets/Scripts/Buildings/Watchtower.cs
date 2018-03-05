using UnityEngine;

namespace VillageKeeper.Game
{
    public class Watchtower : Building
    {
        private ProjectileSpawner projectileSpawner;

        private float lastTimeShooted = 0f;

        public float ReloadDuration { get { return Core.Data.Balance.GetReloadDuration(Type); } }
        
        public void Shoot()
        {
            projectileSpawner.Shoot(((Vector2) Core.Instance.Monster.transform.localPosition) + new Vector2(0, Core.Instance.BuildingsArea.CellWorldSize.y));
            lastTimeShooted = Time.time;
        }

        protected virtual void Awake()
        {
            projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
        }

        protected virtual void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Battle
                && Time.time >= lastTimeShooted + ReloadDuration
                && Vector2.Distance(transform.position, Core.Instance.Monster.transform.localPosition) < Core.Instance.BuildingsArea.CellWorldSize.x * 4)
            {
                Shoot();
            }
        }
    }
}