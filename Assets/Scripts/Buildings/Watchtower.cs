using UnityEngine;

namespace VillageKeeper.Game
{
    public class Watchtower : Building
    {
        private ProjectileSpawner projectileSpawner;

        private float lastTimeShot = 0f;

        public float ReloadDuration { get { return Core.Data.Balance.GetReloadDuration(Type); } }
        
        public void Shoot()
        {
            Vector2 target = ((Vector2)Core.Instance.Monster.transform.position) + new Vector2(0, Core.Instance.Monster.WorldSize.y * 0.25f);
            Vector2 vectorToCalculateAngle = ((target - (Vector2) transform.position) / 2f + new Vector2(0f, Core.Instance.BuildingsArea.CellWorldSize.y)).normalized;
            float angle = Mathf.Atan2(vectorToCalculateAngle.y, vectorToCalculateAngle.x);
            projectileSpawner.Shoot(target, angle);
            lastTimeShot = Time.time;
        }

        protected virtual void Awake()
        {
            projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
        }

        protected virtual void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Battle
                && Time.time >= lastTimeShot + ReloadDuration
                && Vector2.Distance(transform.position, Core.Instance.Monster.Position) < Core.Instance.BuildingsArea.CellWorldSize.x * 4)
            {
                Shoot();
            }
        }
    }
}