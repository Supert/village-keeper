using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.UI;

namespace VillageKeeper.Game
{
    [RequireComponent(typeof(Image))]
    public class Archer : MonoBehaviour
    {
        public enum ArcherAimingValues
        {
            Unloaded = 0,
            AimingDown,
            AimingStraight,
            AimingUp,
        }

        private Animator animator;
        private RectTransform rect;

        public bool IsLoaded { get { return Core.Instance.Data.Game.IsArrowForceOverThreshold.Get(); } }

        private ArcherAimingValues state = 0;
        public ArcherAimingValues State
        {
            get
            {
                return state;
            }
            private set
            {
                state = value;
                animator.SetInteger("CurrentState", (int)value);
            }
        }

        void Start()
        {
            animator = GetComponent<Animator>() as Animator;
            rect = GetComponent<RectTransform>() as RectTransform;
        }

        void Update()
        {
            if (IsLoaded)
            {
                if (Core.Instance.Monster.Sector == Monster.SectorValues.Close)
                    State = ArcherAimingValues.AimingDown;
                else
                {
                    if (Core.Instance.Monster.Sector == Monster.SectorValues.Middle)
                        State = ArcherAimingValues.AimingStraight;
                    else
                        State = ArcherAimingValues.AimingUp;
                }
            }
            else
                State = ArcherAimingValues.Unloaded;
        }

        public float GetAimingAngleInRads()
        {
            switch (State)
            {
                case (ArcherAimingValues.AimingDown):
                    return Mathf.Deg2Rad * (-11f);
                case (ArcherAimingValues.AimingStraight):
                    return 0f;
                case (ArcherAimingValues.AimingUp):
                    return Mathf.Deg2Rad * 9f;
            }
            return 0f;
        }

        public void Shoot(Vector2 targetPosition)
        {
            if (IsLoaded)
            {
                Core.Instance.Data.Game.ClampedArrowForce.Set(0f);
                var tp = targetPosition;
                var arrow = new GameObject("arrow", typeof(ArrowScript)).GetComponent<ArrowScript>();
                var initialPosition = (Vector2)transform.position + (Vector2)rect.TransformVector(new Vector2(rect.rect.width / 2, rect.rect.height * 0.6f));
                arrow.Init(initialPosition, tp, GetAimingAngleInRads());
                Core.Instance.AudioManager.PlayArrowShot();
            }
        }
    }
}