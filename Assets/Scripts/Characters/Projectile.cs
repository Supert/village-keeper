using UnityEngine;

namespace VillageKeeper.Game
{
    public abstract class Projectile : MonoBehaviour
    {
        protected Vector2 TargetPosition { get; set; }
        protected Vector2 Velocity { get; set; }
        
        private void SetVelocity(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            var deltaPosition = targetPosition - initialPosition;
            var gravity = 9.81f;
            var angleInRads = initialRotationInRad;
            //http://physics.stackexchange.com/questions/60595/solve-for-initial-velocity-of-a-projectile-given-angle-gravity-and-initial-and
            var initialVelocity = (deltaPosition.x / Mathf.Cos(angleInRads)) * Mathf.Sqrt(gravity / (2 * (-deltaPosition.y + Mathf.Tan(angleInRads) * deltaPosition.x)));
            Velocity = new Vector2(Mathf.Cos(angleInRads), Mathf.Sin(angleInRads)) * initialVelocity;
        }

        public virtual void Initialize (Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            TargetPosition = targetPosition;

            var lp = transform.localPosition;
            lp.x = initialPosition.x;
            lp.y = initialPosition.y;
            lp.z = TargetPosition.y;
            transform.position = lp;

            SetVelocity(initialPosition, targetPosition, initialRotationInRad);
        }

        protected virtual void Update()
        {
            Velocity = Velocity + new Vector2(0f, -9.81f * Time.deltaTime);
            transform.localPosition = transform.localPosition + ((Vector3)Velocity * Time.deltaTime);
            float angle = Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            if (((Vector2)transform.localPosition - TargetPosition).magnitude < 0f)
            {
                Destroy();
            }
            if (Core.Instance.Monster.CheckHitByPosition(transform.position))
            {
                gameObject.SetActive(false);
                Destroy();
            }
        }

        void Destroy()
        {
            Destroy(gameObject);
        }

    }
}