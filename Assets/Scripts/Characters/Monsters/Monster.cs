using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DeenGames.Utils.AStarPathFinder;
using DeenGames.Utils;
using VillageKeeper.Model;

namespace VillageKeeper.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
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

        private Animator animator;
        private SpriteRenderer sprite;
        private SpriteRenderer shadow;
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
                Data.Game.ClampedMonsterHealth.Set(health / (float)maxHealth);
                if (health == 0)
                    Kill();
            }
        }

        public Vector2 Size
        {
            get
            {
                return sprite.bounds.size;
            }
        }

        public float monsterSpeed;
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
                var buildingsArea = Core.Instance.BuildingsArea;
                return buildingsArea.GetWorldPositionByGridPosition(new Vector2(buildingsArea.numberOfColumns, buildingsArea.numberOfRows - 1)) + GridToWorldOffset;
            }
        }

        public Vector2 HiddenPosition
        {
            get
            {
                var x = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
                return new Vector2(x + Size.x / 2, MaxPosition.y);
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

        private List<PathFinderNode> waypointsInGrid = new List<PathFinderNode>();

        private Vector2? Waypoint
        {
            get
            {
                var buildingsArea = Core.Instance.BuildingsArea;
                if (waypointsInGrid.Count > 0 && (Vector2)transform.localPosition == (buildingsArea.GetWorldPositionByGridPosition(waypointsInGrid[0].X, waypointsInGrid[0].Y)) + GridToWorldOffset)
                    waypointsInGrid.RemoveAt(0);
                if (waypointsInGrid.Count > 0)
                    return (Vector2?)buildingsArea.GetWorldPositionByGridPosition(waypointsInGrid[0].X, waypointsInGrid[0].Y) + GridToWorldOffset;
                return null;
            }
        }

        private List<PathFinderNode> GetPathToRandomTarget(bool isAgressive)
        {
            byte[,] pathGrid = new byte[16, 16];
            List<Point> possibleTargets = new List<Point>();
            var buildingsArea = Core.Instance.BuildingsArea;
            for (int i = 0; i < buildingsArea.numberOfColumns; i++)
            {
                for (int j = 0; j < buildingsArea.numberOfRows; j++)
                {
                    if (buildingsArea.IsCellFree(new Vector2(i, j))
                        && (i == buildingsArea.numberOfColumns - 1 || buildingsArea.IsCellFree(new Vector2(i + 1, j))))
                    {
                        pathGrid[i, j] = PathFinderHelper.EMPTY_TILE;
                    }
                    else
                        pathGrid[i, j] = PathFinderHelper.BLOCKED_TILE;
                }
            }
            for (int j = 0; j < buildingsArea.numberOfRows; j++)
            {
                string s = "";
                for (int i = 0; i < buildingsArea.numberOfColumns; i++)
                {
                    s += pathGrid[i, j] == PathFinderHelper.EMPTY_TILE ? "O" : "X";
                }
            }
            for (int i = buildingsArea.numberOfColumns; i < 16; i++)
                for (int j = 0; j < buildingsArea.numberOfRows; j++)
                    pathGrid[i, j] = PathFinderHelper.EMPTY_TILE;
            for (int i = 1; i < buildingsArea.numberOfColumns + 1; i++)
            {
                for (int j = 0; j < buildingsArea.numberOfRows; j++)
                {
                    if (isAgressive)
                    {
                        if (buildingsArea.GetCell(i - 1, j) != null && buildingsArea.GetCell(i - 1, j).Building != null)
                            possibleTargets.Add(new Point(i, j));
                    }
                    else
                        possibleTargets.Add(new Point(i, j));
                }
            }
            var pathFinder = new PathFinder(pathGrid)
            {
                Diagonals = false,
            };

            var monsterPoint = new Point((int)GridPosition.x, (int)GridPosition.y);
            var paths = new List<List<PathFinderNode>>();
            foreach (var t in possibleTargets)
            {
                var path = pathFinder.FindPath(monsterPoint, t);
                if (path != null)
                    paths.Add(new List<PathFinderNode>(path));
            }
            if (paths == null || paths.Count == 0)
                return null;
            return paths[Random.Range(0, paths.Count)];
        }

        public bool CheckHitByPosition(Vector3 projectilePosition)
        {
            if (col.OverlapPoint(projectilePosition))
            {
                if (Mathf.Abs(projectilePosition.z - transform.localPosition.y) <= sprite.bounds.extents.y)
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
            sprite.color = new Color(1f, 0.5f, 0.5f, 1f);
            Core.Instance.AudioManager.PlayMonsterHit();
        }

        public void Kill()
        {
            sprite.color = new Color(1f, 0.5f, 0.5f, 1f);
            var ghost = Instantiate(Data.Resources.GhostPrefab.Get());
            ghost.transform.localPosition = transform.localPosition;
            Core.Instance.FSM.Event(FSM.StateMachineEvents.RoundFinished);
        }

        void MoveTowardsColor(Color targetColor, float animationDuration)
        {
            if (sprite.color != targetColor)
                sprite.color = Vector4.MoveTowards(sprite.color, targetColor, Time.deltaTime / animationDuration);
        }

        private Vector2? GetAdjacentBuildingCell()
        {
            var buildingsArea = Core.Instance.BuildingsArea;
            if (GridPosition.x - 1 >= buildingsArea.numberOfColumns || GridPosition.x - 1 < 0)
                return null;
            if (!buildingsArea.IsCellFree(GridPosition - new Vector2(1, 0)))
                return GridPosition - new Vector2(1, 0);
            return null;
        }

        private void Attack(Vector2 buildingCell)
        {
            animator.SetTrigger("Attack");
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            Core.Instance.BuildingsArea.DamageCell(buildingCell);
            ChooseNewBehaviour();
        }

        private void ChooseNewBehaviour()
        {
            IsAgressive = UnityEngine.Random.value < agressiveness;
        }

        private void SetWaypoints(List<PathFinderNode> waypoints)
        {
            waypointsInGrid = waypoints;
            TargetPosition = Core.Instance.BuildingsArea.GetWorldPositionByGridPosition(waypoints.Last().X, waypoints.Last().Y) + GridToWorldOffset;
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
            if (Waypoint != null && Waypoint.HasValue)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, Waypoint.Value, monsterSpeed * Time.deltaTime);

            }
            if ((Vector2)transform.localPosition == TargetPosition)
            {
                ChooseNewBehaviour();
            }
        }

        private void SetMaxHealthAndAgressiveness()
        {
            float pointsPool = Data.Balance.GetMonsterPowerPoints();
            float minHealthPossible = Data.Balance.GetMonsterMinHealth();
            float maxHealthPossible = Data.Balance.GetMonsterMaxHealth();

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
            sprite = GetComponent<SpriteRenderer>() as SpriteRenderer;
            col = GetComponent<Collider2D>() as Collider2D;
            animator = GetComponent<Animator>() as Animator;
            shadow = transform.GetChild(transform.childCount - 1).GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        void Start()
        {
            var ls = transform.localScale;
            ls *= (Core.Instance.BuildingsArea.CellWorldSize.y * 2) / (sprite.bounds.size.y);
            transform.localScale = ls;

            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, OnBuildEnter);
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Battle, OnBattleEnter);
            Core.Instance.FSM.SubscribeToExit(FSM.States.Battle, OnBattleExit);
        }

        private void OnBattleExit()
        {
            animator.speed = 0f;
        }

        public void Init()
        {
            shadow.color = Color.white;
            sprite.color = Color.white;

            SetMaxHealthAndAgressiveness();
            Health = maxHealth;

            waypointsInGrid.Clear();
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
                            var adjacentBuildingCellCoordinates = GetAdjacentBuildingCell();
                            if (adjacentBuildingCellCoordinates != null)
                                Attack(adjacentBuildingCellCoordinates.Value);
                            else
                            {
                                var waypoints = GetPathToRandomTarget(true);
                                if (waypoints == null)
                                    waypoints = GetPathToRandomTarget(false);
                                SetWaypoints(waypoints);
                            }
                        }
                        else
                        {
                            SetWaypoints(GetPathToRandomTarget(false));
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