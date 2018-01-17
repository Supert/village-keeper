using UnityEngine;
using VillageKeeper.Model;

namespace VillageKeeper.UI
{
    public class Wind
    {
        public float Strength { get; private set; }
        private float target;

        private float maxStrength;
        private float acceleration;
        private float minChangeDelay;
        private float maxChangeDelay;

        float timeTargetReached = 0f;
        float delay;

        private bool isNewDelaySelected;

        public Wind(float maxStrength, float acceleration, float minChangeDelay, float maxChangeDelay)
        {
            this.maxStrength = maxStrength;
            this.acceleration = acceleration;
            this.minChangeDelay = minChangeDelay;
            this.maxChangeDelay = maxChangeDelay;

            Strength = ((float)Core.Instance.Random.NextDouble() - 0.5f) * 2f * maxStrength;
            target = (Random.value - 0.5f) * 2 * maxStrength;
        }

        private void SetNewTargetStrength()
        {
            target = ((float)Core.Instance.Random.NextDouble() - 0.5f) * 2f * maxStrength;
        }

        public void Update()
        {
            float oldStrength = Strength;
            Strength = Mathf.MoveTowards(Strength, target, acceleration * Time.deltaTime);

            if (Strength == target)
            {
                if (!isNewDelaySelected)
                {
                    delay = minChangeDelay + (float)Core.Instance.Random.NextDouble() * (maxChangeDelay - minChangeDelay);
                    timeTargetReached = Time.time;
                    isNewDelaySelected = true;
                }
                else if (timeTargetReached + delay < Time.time)
                {
                    SetNewTargetStrength();
                    isNewDelaySelected = false;
                }
            }

            if (oldStrength != Strength)
            {
                Data.Common.Wind.Set(Strength);
            }
        }
    }
}