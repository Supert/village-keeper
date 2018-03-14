using UnityEngine;

namespace VillageKeeper.Game
{
    public class WatchtowerArrow : Arrow
    {
        private Vector2 initialPosition;
        public override void Initialize(Vector2 initialPosition, Vector2 targetPosition, float initialRotationInRad)
        {
            this.initialPosition = initialPosition;
            base.Initialize(initialPosition, targetPosition, initialRotationInRad);

            Shadow.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        protected override void Update()
        {
            base.Update();

            Vector2 intersection = Vector2.zero;

            FindSegmentsIntersection(initialPosition, TargetPosition, transform.position, transform.position + Vector3.down, out intersection);

            if (float.IsNaN(intersection.y))
                Shadow.transform.position = new Vector3(transform.position.x, TargetPosition.y, 1f);
            else
                Shadow.transform.position = new Vector3(transform.position.x, intersection.y, 1f);

            Shadow.rotation = Quaternion.identity;
        }

        //based on https://www.codeproject.com/Tips/862988/Find-the-Intersection-Point-of-Two-Line-Segments
        private static bool FindSegmentsIntersection(Vector2 p, Vector2 p2, Vector2 q, Vector2 q2, out Vector2 intersection)
        {
            intersection = new Vector2(float.NaN, float.NaN);

            Vector2 r = p2 - p;
            Vector2 s = q2 - q;
            float rxs = Vector3.Cross(r, s).magnitude;

            //parallel lines.
            if (rxs < 0.001f)
            {
                return false;
            }

            float t = Vector3.Cross(q - p, s).magnitude / rxs;
            float u = Vector3.Cross(q - p, r).magnitude / rxs;

            intersection = p + t * r;

            if (0 <= t
                && t <= 1
                && 0 <= u
                && u <= 1)
            {
                return true;
            }

            return false;
        }
    }
}