using UnityEngine;

namespace VillageKeeper.Game
{
    public class WindScript : MonoBehaviour
    {
        private float strength;
        private float target;

        public float maxStrength;
        public float strengthAcceleration;
        public float minTimeBeforeChange;
        public float maxTimeBeforeChange;

        float timeTargetReached = 0f;
        float delay;
        
        private void Start()
        {
            strength = ((float) Core.Instance.Random.NextDouble() - 0.5f) * 2f * maxStrength;
            target = (Random.value - 0.5f) * 2 * maxStrength;
        }

        private void SetNewTargetStrength()
        {
            target = ((float)Core.Instance.Random.NextDouble() - 0.5f) * 2f * maxStrength;
        }
        
        private void Update()
        {
            float oldStrength = strength;
            strength = Mathf.MoveTowards(strength, target, strengthAcceleration * Time.deltaTime);

            if (strength == target)
            {
                if (timeTargetReached + delay > Time.time)
                {
                    SetNewTargetStrength();
                }
                else
                {
                    delay = minTimeBeforeChange + (float)Core.Instance.Random.NextDouble() * (maxTimeBeforeChange - minTimeBeforeChange);
                    timeTargetReached = Time.time;
                }
            }

            if (oldStrength != strength)
            {
                Core.Instance.Data.Common.Wind.Set(strength);
            }
        }
    }
}