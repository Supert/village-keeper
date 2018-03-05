using UnityEngine;

namespace VillageKeeper.Game
{
    public class Arrow : Projectile
    {
        private RectTransform shadow;
        
        protected virtual void Awake()
        {
            shadow = transform.Find("shadow") as RectTransform;

        }

        public override void Initialize(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            base.Initialize(initialPosition, targetPosition, initialRotationInRad);

            shadow.position = new Vector3(transform.position.x, targetPosition.y, 0);
        }

        protected override void Update()
        {
            base.Update();
            
            shadow.transform.position = new Vector3(transform.position.x, TargetPosition.y, 1f);
        }
    }
}