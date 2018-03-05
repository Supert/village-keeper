using UnityEngine;

namespace VillageKeeper.Game
{
    public class ProjectileSpawner : MonoBehaviour
    {
        public void Shoot(Vector2 target)
        {
            var arrow = new GameObject("arrow", typeof(Arrow)).GetComponent<Arrow>();
            var initialPosition = (Vector2)transform.position;
            var vectorToCalcAngle = target - initialPosition;
            var angle = Mathf.Atan2(vectorToCalcAngle.y, vectorToCalcAngle.x);
            arrow.Initialize(initialPosition, target, angle);
        }
    }
}