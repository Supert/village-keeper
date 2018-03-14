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

        public Vector2 WorldSize { get { return rectTransform.TransformVector(rectTransform.rect.size); } }

        private Vector2 LocalSize { get { return rectTransform.rect.size; } }

        public bool IsAgressive
        {
            get;
            private set;
        }

        public SectorValues Sector
        {
            get
            {
                float normalized = (transform.position.x - MinPosition.x) / (MaxPosition.x - MinPosition.x);
                if (normalized < 0.33f)
                    return SectorValues.Close;
                if (normalized >= 0.33f && normalized < 0.67f)
                    return SectorValues.Middle;
                return SectorValues.Far;
            }
        }

        public Vector2 Position { get { return transform.position; } }

        public Vector2 MinPosition
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                monsterWalkableArea.GetWorldCorners(corners);
                return corners[0];
            }
        }

        public Vector2 MaxPosition
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                monsterWalkableArea.GetWorldCorners(corners);
                return corners[2];
            }
        }

        public Vector2 HiddenPosition
        {
            get
            {
                return MaxPosition + new Vector2(LocalSize.x / 2, 0);
            }
        }

        private Vector2 TargetPosition { get; set; }

        public Vector2 GridToWorldOffset
        {
            get
            {
                return new Vector2(Core.Instance.BuildingsArea.CellWorldSize.x / 2, LocalSize.y / 3);
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
                Vector2 result = waypoints[0];
                return waypoints[0];
            }
        }



        public bool CheckHitByPosition(Vector3 projectilePosition)
        {
            if (col.OverlapPoint(projectilePosition))
            {
                if (Mathf.Abs(projectilePosition.z - transform.localPosition.y) <= LocalSize.y)
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
            if (((Vector2)transform.position - TargetPosition).x >= 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else
                if (((Vector2)transform.position - TargetPosition).x < 0)
                scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, Waypoint, Core.Data.Balance.MonsterSpeed * Time.deltaTime);
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
            transform.position = TargetPosition = HiddenPosition;

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
                    if (waypoints.Count > 0 && ((Vector2)transform.position - waypoints[0]).sqrMagnitude < 0.01f)
                    {
                        waypoints.RemoveAt(0);
                    }

                    if ((Vector2)transform.localPosition == TargetPosition || waypoints.Count == 0)
                    {
                        if (IsAgressive)
                        {
                            var adjacentBuilding = Core.Instance.BuildingsArea.GetAdjacentBuilding(transform.position);
                            if (adjacentBuilding != null)
                            {
                                Attack(adjacentBuilding);
                            }
                            else
                            {
                                var waypoints = Core.Instance.BuildingsArea.GetPathToRandomTarget(IsAgressive, transform.position);
                                if (waypoints == null)
                                    waypoints = Core.Instance.BuildingsArea.GetPathToRandomTarget(IsAgressive, transform.position);
                                SetWaypoints(waypoints);
                            }
                        }
                        else
                        {
                            ChooseNewBehaviour();
                            SetWaypoints(Core.Instance.BuildingsArea.GetPathToRandomTarget(IsAgressive, transform.position));
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