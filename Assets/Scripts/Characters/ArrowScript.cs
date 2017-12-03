using UnityEngine;
using System.Collections;

namespace VillageKeeper.Game
{
    public class ArrowScript : MonoBehaviour
    {
        private Vector2 _targetPosition;
        SpriteRenderer _shadow;
        SpriteRenderer _sprite;
        Vector2 _velocity;

        void Start()
        {
            _sprite = gameObject.AddComponent<SpriteRenderer>();
            _sprite.sprite = Resources.Load<Sprite>("arrow");
            _sprite.sortingLayerName = "Characters";
            _sprite.sortingOrder = 1;
        }

        IEnumerator InitCoroutine()
        {
            yield return new WaitForEndOfFrame();
            _shadow.transform.position = new Vector3(transform.position.x - _shadow.bounds.extents.x, _targetPosition.y, 0);
            var scaleFactor = _sprite.bounds.size.x / _shadow.sprite.bounds.size.x;
            _shadow.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            _shadow.gameObject.SetActive(true);
        }

        void SetVelocity(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            var deltaPosition = targetPosition - initialPosition;
            var gravity = 9.81f;
            var angleInRads = initialRotationInRad;
            //http://physics.stackexchange.com/questions/60595/solve-for-initial-velocity-of-a-projectile-given-angle-gravity-and-initial-and
            var initialVelocity = (deltaPosition.x / Mathf.Cos(angleInRads)) * Mathf.Sqrt(gravity / (2 * (-deltaPosition.y + Mathf.Tan(angleInRads) * deltaPosition.x)));
            _velocity = new Vector2(Mathf.Cos(angleInRads), Mathf.Sin(angleInRads)) * initialVelocity;
        }

        public void Init(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            _targetPosition = targetPosition;


            var lp = transform.localPosition;
            lp.x = initialPosition.x;
            lp.y = initialPosition.y;
            lp.z = _targetPosition.y;
            transform.position = lp;

            SetVelocity(initialPosition, targetPosition, initialRotationInRad);
            var _shadowGO = new GameObject("arrow shadow");
            _shadowGO.SetActive(false);
            _shadow = _shadowGO.AddComponent<SpriteRenderer>() as SpriteRenderer;
            _shadow.sprite = Resources.Load<Sprite>("shadow");
            _shadow.color = new Color(1, 1, 1, 0.5f);
            StartCoroutine(InitCoroutine());
        }

        void Update()
        {
            _velocity -= new Vector2(0 /*- CoreScript.Instance.Wind.Strength * Time.deltaTime*/, 9.81f * Time.deltaTime);
            var newPosition = transform.localPosition + ((Vector3)_velocity * Time.deltaTime);
            if (!(float.IsNaN(newPosition.x) || float.IsNaN(newPosition.y) || float.IsNaN(newPosition.z)))
            {
                transform.localPosition = newPosition;
            }
            else
            {
                Destroy();
            }
            float angle = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            _shadow.transform.position = new Vector3(transform.position.x - _shadow.bounds.extents.x, _targetPosition.y, 1f);


            if (((Vector2)transform.localPosition - (Vector2)_targetPosition).magnitude < _shadow.bounds.extents.y)
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
            Destroy(_shadow.gameObject);
        }
    }
}