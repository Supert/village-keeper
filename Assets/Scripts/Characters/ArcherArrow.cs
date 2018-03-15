using UnityEngine;

namespace VillageKeeper.Game
{
    public class ArcherArrow : Arrow
    {
        protected override float Gravity { get { return 120f; } }

        public override void Initialize(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            base.Initialize(initialPosition, targetPosition, initialRotationInRad);

            Shadow.position = new Vector3(transform.position.x, targetPosition.y, 0);
        }

        protected override void Update()
        {
            base.Update();
            
            Shadow.transform.position = new Vector3(transform.position.x, TargetPosition.y, 1f);
            Shadow.rotation = Quaternion.identity;
        }
    }
}