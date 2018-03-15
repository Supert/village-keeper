using UnityEngine;
using UnityEngine.UI;

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

        private ProjectileSpawner projectileSpawner;
        private Animator animator;
        private RectTransform rect;

        public bool IsLoaded { get { return Core.Data.Game.IsArrowForceOverThreshold; } }

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

        void Awake()
        {
            projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
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
                projectileSpawner.Shoot(targetPosition, GetAimingAngleInRads());
                Core.Data.Game.ClampedArrowForce.Set(0f);
                Core.Instance.AudioManager.PlayArrowShot();
            }
        }
    }
}