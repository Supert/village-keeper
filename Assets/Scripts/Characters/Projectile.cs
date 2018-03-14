using UnityEngine;

namespace VillageKeeper.Game
{
    public abstract class Projectile : MonoBehaviour
    {
        private static float Gravity { get { return 60f; } }

        protected Vector2 TargetPosition { get; set; }
        protected Vector2 InitialPosition { get; set; }
        protected Vector2 Velocity { get; set; }

        private void SetVelocity(float initialRotationInRad)
        {
            var deltaPosition = TargetPosition - InitialPosition;

            if (deltaPosition.x == 0f)
            {
                Destroy();
                return;
            }

            float angleInRads = initialRotationInRad;
            //http://physics.stackexchange.com/questions/60595/solve-for-initial-velocity-of-a-projectile-given-angle-gravity-and-initial-and
            float initialVelocity = (deltaPosition.x / Mathf.Cos(angleInRads)) * Mathf.Sqrt(Gravity / (2 * (-deltaPosition.y + Mathf.Tan(angleInRads) * deltaPosition.x)));
            Velocity = new Vector2(Mathf.Clamp(Mathf.Cos(angleInRads), -1f, 1f), Mathf.Clamp(Mathf.Sin(angleInRads), -1f, 1f)) * initialVelocity;
        }

        public virtual void Initialize(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            TargetPosition = targetPosition;
            InitialPosition = initialPosition;
            transform.position = initialPosition;
            var p = transform.position;
            p.z = TargetPosition.y;
            transform.position = p;

            SetVelocity(initialRotationInRad);
        }

        protected virtual void Update()
        {
            Velocity = Velocity + new Vector2(0f, -Gravity * Time.deltaTime);
            transform.position = transform.position + ((Vector3)Velocity * Time.deltaTime);
            float angle = Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            if ((InitialPosition.x < TargetPosition.x && transform.position.x > TargetPosition.x)
                || (InitialPosition.x > TargetPosition.x && transform.position.x < TargetPosition.x))
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