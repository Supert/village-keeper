using UnityEngine;
using System.Collections;

namespace VillageKeeper.Game
{
    public class ArrowScript : MonoBehaviour
    {
        private Vector2 targetPosition;
        private SpriteRenderer shadowRenderer;
        private SpriteRenderer spriteRenderer;
        private Vector2 velocity;

        void Start()
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("arrow");
            spriteRenderer.sortingLayerName = "Characters";
            spriteRenderer.sortingOrder = 1;
        }

        IEnumerator InitCoroutine()
        {
            yield return new WaitForEndOfFrame();
            shadowRenderer.transform.position = new Vector3(transform.position.x - shadowRenderer.bounds.extents.x, targetPosition.y, 0);
            var scaleFactor = spriteRenderer.bounds.size.x / shadowRenderer.sprite.bounds.size.x;
            shadowRenderer.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            shadowRenderer.gameObject.SetActive(true);
        }

        void SetVelocity(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            var deltaPosition = targetPosition - initialPosition;
            var gravity = 9.81f;
            var angleInRads = initialRotationInRad;
            //http://physics.stackexchange.com/questions/60595/solve-for-initial-velocity-of-a-projectile-given-angle-gravity-and-initial-and
            var initialVelocity = (deltaPosition.x / Mathf.Cos(angleInRads)) * Mathf.Sqrt(gravity / (2 * (-deltaPosition.y + Mathf.Tan(angleInRads) * deltaPosition.x)));
            velocity = new Vector2(Mathf.Cos(angleInRads), Mathf.Sin(angleInRads)) * initialVelocity;
        }

        public void Init(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            this.targetPosition = targetPosition;


            var lp = transform.localPosition;
            lp.x = initialPosition.x;
            lp.y = initialPosition.y;
            lp.z = this.targetPosition.y;
            transform.position = lp;

            SetVelocity(initialPosition, targetPosition, initialRotationInRad);
            var shadowGO = new GameObject("arrow shadow");
            shadowGO.SetActive(false);
            shadowRenderer = shadowGO.AddComponent<SpriteRenderer>() as SpriteRenderer;
            shadowRenderer.sprite = Resources.Load<Sprite>("shadow");
            shadowRenderer.color = new Color(1, 1, 1, 0.5f);
            StartCoroutine(InitCoroutine());
        }

        void Update()
        {
            velocity.y -= 9.81f * Time.deltaTime;
            var newPosition = transform.localPosition + ((Vector3)velocity * Time.deltaTime);
            if (!(float.IsNaN(newPosition.x) || float.IsNaN(newPosition.y) || float.IsNaN(newPosition.z)))
            {
                transform.localPosition = newPosition;
            }
            else
            {
                Destroy();
            }
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            shadowRenderer.transform.position = new Vector3(transform.position.x - shadowRenderer.bounds.extents.x, targetPosition.y, 1f);


            if (((Vector2)transform.localPosition - (Vector2)targetPosition).magnitude < shadowRenderer.bounds.extents.y)
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
            Destroy(shadowRenderer.gameObject);
        }
    }
}