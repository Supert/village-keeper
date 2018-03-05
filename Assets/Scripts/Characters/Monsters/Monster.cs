using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace VillageKeeper.Game
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class Monster : MonoBehaviour
    {
        public enum SectorValues
        {
            Close,
            Middle,
            Far,
        }

        private int maxHealth;

        private RectTransform rectTransform;
        private RectTransform monsterWalkableArea;
        private Animator animator;
        private Image image;
        private Image shadow;
        private Collider2D col;

        private float agressiveness;

        private int health;
        public int Health
        {
            get
            {
                return health;
            }
            private set
            {
                health = value;
                Core.Data.Game.ClampedMonsterHealth.Set(health / (float)maxHealth);
                if (health == 0)
                    Kill();
            }
        }

        public Vector2 Size
        {
            get
            {
                return rectTransform.rect.size;
            }
        }

        public bool IsAgressive
        {
            get;
            private set;
        }

        public SectorValues Sector
        {
            get
            {
                if (transform.localPosition.x < (MaxPosition.x / 3))
                    return SectorValues.Close;
                if (transform.localPosition.x >= MaxPosition.x / 3 && transform.localPosition.x < (MaxPosition.x * 2 / 3))
                    return SectorValues.Middle;
                return SectorValues.Far;
            }
        }

        public Vector2 MinPosition
        {
            get
            {
                var buildingsArea = Core.Instance.BuildingsArea;
                return buildingsArea.GetWorldPositionByGridPosition(new Vector2(0, 0)) + GridToWorldOffset;
            }
        }

        public Vector2 MaxPosition
        {
            get
            {
                return monsterWalkableArea.rect.size;
            }
        }

        public Vector2 HiddenPosition
        {
            get
            {
                return MaxPosition + new Vector2(Size.x / 2, 0);
            }
        }

        private Vector2 TargetPosition { get; set; }

        public Vector2 GridToWorldOffset
        {
            get
            {
                return new Vector2(Core.Instance.BuildingsArea.CellWorldSize.x / 2, Size.y / 3);
            }
        }
        public Vector2 GridPosition
        {
            get
            {
                var buildingsArea = Core.Instance.BuildingsArea;
                var result = buildingsArea.GetClosestGridPositionIgnoringGridLimits(new Vector2(transform.localPosition.x, transform.localPosition.y) - GridToWorldOffset);
                return result;
            }
        }

        private List<Vector2> waypoints = new List<Vector2>();
        //private List<PathFinderNode> waypointsInGrid = new List<PathFinderNode>();

        private Vector2 Waypoint
        {
            get
            {
                var buildingsArea = Core.Instance.BuildingsArea;
                if (waypoints.Count > 0 && ((Vector2)transform.localPosition - waypoints[0]).sqrMagnitude < 0.01f)
                    waypoints.RemoveAt(0);
                if (waypoints.Count > 0)
                    return buildingsArea.GetWorldPositionByGridPosition(waypoints[0]);
                ChooseNewBehaviour();
                SetWaypoints(Core.Instance.BuildingsArea.GetPathToRandomTarget(IsAgressive, transform.position));
                return waypoints[0];
            }
        }



        public bool CheckHitByPosition(Vector3 projectilePosition)
        {
            if (col.OverlapPoint(projectilePosition))
            {
                if (Mathf.Abs(projectilePosition.z - transform.localPosition.y) <= Size.y)
                {
                    TakeDamage();
                    return true;
                }
            }
            return false;
        }

        public void TakeDamage()
        {
            Health--;
            image.color = new Color(1f, 0.5f, 0.5f, 1f);
            Core.Instance.AudioManager.PlayMonsterHit();
        }

        public void Kill()
        {
            image.color = new Color(1f, 0.5f, 0.5f, 1f);
            var ghost = Instantiate(Core.Data.Resources.GhostPrefab.Get());
            ghost.transform.localPosition = transform.localPosition;
            Core.Instance.FSM.Event(FSM.StateMachineEvents.RoundFinished);
        }

        void MoveTowardsColor(Color targetColor, float animationDuration)
        {
            if (image.color != targetColor)
                image.color = Vector4.MoveTowards(image.color, targetColor, Time.deltaTime / animationDuration);
        }

        private void Attack(Building building)
        {
            animator.SetTrigger("Attack");
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            Core.Instance.BuildingsArea.DamageCell(building.Tile.GridX, building.Tile.GridY);
            ChooseNewBehaviour();
        }

        private void ChooseNewBehaviour()
        {
            IsAgressive = Core.Instance.Random.NextDouble() < agressiveness;
        }

        private void SetWaypoints(List<Vector2> waypoints)
        {
            this.waypoints = waypoints;
            TargetPosition = waypoints.Last();
            var scale = transform.localScale;
            if (((Vector2)transform.localPosition - TargetPosition).x >= 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else
                if (((Vector2)transform.localPosition - TargetPosition).x < 0)
                scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        private void Move()
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Waypoint, Core.Data.Balance.MonsterSpeed * Time.deltaTime);
        }

        private void SetMaxHealthAndAgressiveness()
        {
            float pointsPool = Core.Data.Balance.GetMonsterPowerPoints();
            float minHealthPossible = Core.Data.Balance.GetMonsterMinHealth();
            float maxHealthPossible = Core.Data.Balance.GetMonsterMaxHealth();

            if (pointsPool < minHealthPossible)
            {
                maxHealth = (int)minHealthPossible;
                agressiveness = pointsPool / minHealthPossible;
            }
            else
            {
                if (pointsPool > maxHealthPossible)
                {
                    maxHealth = (int)maxHealthPossible;
                    agressiveness = 1f;
                }
                else
                {
                    agressiveness = Random.Range(pointsPool / maxHealthPossible, 1);
                    maxHealth = (int)(pointsPool / agressiveness);
                }
            }
        }

        void Awake()
        {
            rectTransform = transform as RectTransform;
            monsterWalkableArea = transform.parent as RectTransform;
            image = GetComponent<Image>();
            col = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
            shadow = transform.GetChild(transform.childCount - 1).GetComponent<Image>();
        }

        void Start()
        {
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, OnBuildEnter);
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Battle, OnBattleEnter);
            Core.Instance.FSM.SubscribeToExit(FSM.States.Battle, OnBattleExit);
        }

        private void OnBattleExit()
        {
            animator.speed = 0f;
        }

        public void Initialize()
        {
            shadow.color = Color.white;
            image.color = Color.white;

            SetMaxHealthAndAgressiveness();
            Health = maxHealth;

            waypoints.Clear();
            transform.localPosition = TargetPosition = HiddenPosition;

            gameObject.SetActive(true);
        }

        private void OnBattleEnter()
        {
            animator.speed = 1f;
        }

        private void OnBuildEnter()
        {
            transform.localPosition = HiddenPosition;
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (Core.Instance.FSM.Current == FSM.States.Battle)
            {
                MoveTowardsColor(Color.white, 0.200f);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("GorillaWandering"))
                {
                    if ((Vector2)transform.localPosition == TargetPosition)
                    {
                        if (IsAgressive)
                        {
                            var adjacentBuilding = Core.Instance.BuildingsArea.GetAdjacentBuilding(transform.position);
                            if (adjacentBuilding != null)
                                Attack(adjacentBuilding);
                            else
                            {
                                var waypoints = Core.Instance.BuildingsArea.GetPathToRandomTarget(true, transform.position);
                                if (waypoints == null)
                                    waypoints = Core.Instance.BuildingsArea.GetPathToRandomTarget(false, transform.position);
                                SetWaypoints(waypoints);
                            }
                        }
                        else
                        {
                            SetWaypoints(Core.Instance.BuildingsArea.GetPathToRandomTarget(false, transform.position));
                        }
                    }
                    else
                    {
                        Move();
                    }
                }
            }
            else if (Core.Instance.FSM.Current == FSM.States.RoundFinished)
            {
                MoveTowardsColor(new Color(1, 1, 1, 0), 1f);
            }

            var lp = transform.localPosition;
            lp.z = lp.y;
            transform.localPosition = lp;
        }
    }
}