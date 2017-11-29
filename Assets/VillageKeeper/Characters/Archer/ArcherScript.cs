using UnityEngine;
using UnityEngine.UI;
using VillageKeeper.UI;

namespace VillageKeeper.Game
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class ArcherScript : MonoBehaviour
    {
        public enum ArcherAimingValues
        {
            Unloaded = 0,
            AimingDown,
            AimingStraight,
            AimingUp,
        }

        public Image hat;
        private Animator _animator;
        public ArrowLoadBarScript arrowBar;
        private RectTransform _rect;

        private bool _isLoaded;
        public bool IsLoaded
        {
            get
            {
                if (arrowBar.RelativeCurrentValue == 1)
                    return true;
                else return false;
            }
        }

        private ArcherAimingValues _state = 0;
        public ArcherAimingValues State
        {
            get
            {
                return _state;
            }
            private set
            {
                _state = value;
                _animator.SetInteger("CurrentState", (int)value);
                switch (CoreScript.Instance.TodaySpecial)
                {
                    case CoreScript.Specials.None:
                        hat.gameObject.SetActive(false);
                        break;
                    case CoreScript.Specials.Winter:
                        hat.gameObject.SetActive(true);
                        break;
                }
            }
        }

        public Sprite archerUnloadedSprite;
        public Sprite archerReadyAimingUpSprite;
        public Sprite archerReadyAimingStraightSprite;
        public Sprite archerReadyAimingDownSprite;

        void Start()
        {
            _animator = GetComponent<Animator>() as Animator;
            _rect = GetComponent<RectTransform>() as RectTransform;
        }

        void Update()
        {
            if (IsLoaded)
            {
                if (CoreScript.Instance.Monster.Sector == MonsterScript.SectorValues.Close)
                    State = ArcherAimingValues.AimingDown;
                else
                {
                    if (CoreScript.Instance.Monster.Sector == MonsterScript.SectorValues.Middle)
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
                var _targetPosition = targetPosition;
                var arrow = new GameObject("arrow", typeof(ArrowScript)).GetComponent<ArrowScript>();
                var initialPosition = (Vector2)transform.position + (Vector2)_rect.TransformVector(new Vector2(_rect.rect.width / 2, _rect.rect.height * 0.6f));
                arrow.Init(initialPosition, _targetPosition, GetAimingAngleInRads());
                CoreScript.Instance.Audio.PlayArrowShot();
            }
        }
    }
}