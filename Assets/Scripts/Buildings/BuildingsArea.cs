using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using VillageKeeper.Model;
using DeenGames.Utils.AStarPathFinder;
using DeenGames.Utils;
using System;

namespace VillageKeeper.Game
{
    public class BuildingsArea : MonoBehaviour
    {
        private RectTransform rect;

        [SerializeField]
        private int numberOfColumns;
        [SerializeField]
        private int numberOfRows;

        private BuildingTile[,] buildingsGrid;
        private byte[,] pathGrid;

        public IEnumerable<Building> Buildings { get { return buildingsGrid.Cast<BuildingTile>().Where(c => c.Building != null).Select(c => c.Building); } }

        void Awake()
        {
            rect = GetComponent<RectTransform>() as RectTransform;
            buildingsGrid = new BuildingTile[numberOfColumns, numberOfRows];

            int pathGridDimension = Math.Max(
                Convert.ToInt32(Math.Pow(2, Math.Ceiling(Math.Log(buildingsGrid.GetLength(0)) / Math.Log(2)))),
                Convert.ToInt32(Math.Pow(2, Math.Ceiling(Math.Log(buildingsGrid.GetLength(1)) / Math.Log(2)))));
            pathGrid = new byte[pathGridDimension, pathGridDimension];
        }

        private void Start()
        {
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Battle, InitializeBuildings);
            Core.Instance.FSM.SubscribeToEnter(FSM.States.Build, LoadBuildings);
            Core.Instance.FSM.SubscribeToEnter(FSM.States.RoundFinished, SaveBuildings);

            for (int i = 0; i < numberOfColumns; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    buildingsGrid[i, j] = Instantiate(Core.Data.Resources.BuildingTile.Get(), transform).GetComponent<BuildingTile>();
                    buildingsGrid[i, j].gameObject.SetActive(true);
                    buildingsGrid[i, j].GridX = i;
                    buildingsGrid[i, j].GridY = j;
                    buildingsGrid[i, j].gameObject.name = "Building Tile (" + i + "," + j + ")";
                    var p = (Vector3)GetWorldPositionByGridPosition(new Vector2(i, j));
                    p.z = p.y;
                    buildingsGrid[i, j].transform.position = p;
                }
            }
        }

        private void InitializeBuildings()
        {
            for (int i = 0; i < buildingsGrid.GetLength(0); i++)
                for (int j = 0; j < buildingsGrid.GetLength(1); j++)
                    if (buildingsGrid[i, j].Building != null)
                        buildingsGrid[i, j].Building.Initialize();

        }

        public BuildingTile GetClosestTile(Vector3 position)
        {
            Vector2 gridPosition = GetClosestGridPositionIgnoringGridLimits(position);

            if (gridPosition.x < 0 || gridPosition.y < 0 || gridPosition.x >= buildingsGrid.GetLength(0) || gridPosition.y >= buildingsGrid.GetLength(1))
                return null;
            return buildingsGrid[Convert.ToInt32(gridPosition.x), Convert.ToInt32(gridPosition.y)];
        }

        public Vector2 CellWorldSize { get { return rect.TransformPoint(CellLocalSize) - rect.TransformPoint(Vector2.zero); } }

        public Vector2 CellLocalSize { get { return new Vector2(rect.rect.width / numberOfColumns, rect.rect.height / numberOfRows); } }

        public Vector2 GetClosestGridPositionIgnoringGridLimits(Vector2 worldPosition)
        {
            var point = rect.InverseTransformPoint(worldPosition);
            var result = new Vector2(point.x / CellLocalSize.x, point.y / CellLocalSize.y);
            result.x = Mathf.RoundToInt(result.x - 0.5f);
            result.y = Mathf.RoundToInt(result.y - 0.5f);
            return result;
        }

        private Vector2 GetClosestGridPosition(Vector2 worldPosition)
        {
            var result = GetClosestGridPositionIgnoringGridLimits(worldPosition);
            result.x = Mathf.Clamp(result.x, 0, numberOfColumns - 1);
            result.y = Mathf.Clamp(result.y, 0, numberOfRows - 1);
            return result;
        }

        public Vector2 GetWorldPositionByPositionInArea(Vector2 position)
        {
            return rect.TransformPoint(position);
        }

        public Vector2 GetWorldPositionByGridPosition(int x, int y)
        {
            var positionInLocal = new Vector2(CellLocalSize.x * (x + 0.5f), CellLocalSize.y * (y + 0.5f));
            return rect.TransformPoint(positionInLocal);
        }

        public Vector2 GetWorldPositionByGridPosition(Vector2 cellIndex)
        {
            return GetWorldPositionByGridPosition((int)cellIndex.x, (int)cellIndex.y);
        }

        public bool IsCellFree(int gridX, int gridY)
        {
            return buildingsGrid[gridX, gridY].Building == null;
        }

        public bool IsCellsFree(int leftBottomX, int leftBottomY, int rightTopX, int rightTopY)
        {
            for (int i = leftBottomX; i <= rightTopX; i++)
                for (int j = leftBottomY; j <= rightTopY; j++)
                    if (!IsCellFree(i, j))
                        return false;
            return true;
        }

        public bool IsCellsFree(List<Tuple<int, int>> cellsIndexes)
        {
            return !cellsIndexes.Any(c => !IsCellFree(c.Item1, c.Item2));
        }

        public void DamageCell(int gridX, int gridY)
        {
            if (IsCellFree(gridX, gridY))
                return;

            buildingsGrid[gridX, gridY].Building.Damage();
        }

        private void PlaceBuilding(Building building, int gridX, int gridY)
        {
            if (IsCellFree(gridX, gridY))
            {
                PlaceBuilding(building, buildingsGrid[gridX, gridY]);
            }
        }

        private void PlaceBuilding(Building building, BuildingTile tile)
        {

            building.Tile = tile;
            building.Tile.Building = building;
            building.transform.SetParent(building.Tile.transform, false);
            building.transform.localPosition = (new Vector3(0, 0, -0.1f));
        }

        public void BuyBuilding(Building building, BuildingTile tile)
        {
            if (tile.Building == null && Core.Data.Saved.Gold >= building.GoldCost)
            {
                Core.Data.Saved.Gold.Set(Core.Data.Saved.Gold - building.GoldCost);
                PlaceBuilding(building, tile);
                SaveBuildings();
            }
        }

        private void BuyBuilding(Building building, int gridX, int gridY)
        {

            if (IsCellFree(gridX, gridY) && Core.Data.Saved.Gold >= building.GoldCost)
            {
                Core.Data.Saved.Gold.Set(Core.Data.Saved.Gold - building.GoldCost);
                PlaceBuilding(building, gridX, gridY);
                SaveBuildings();
            }
        }

        public void RemoveBuilding(Building building)
        {
            building.Tile.Building = null;
            building.Tile = null;
            Destroy(building.gameObject);
        }

        public void SaveBuildings()
        {
            var list = Buildings.Select(b => new SerializableBuilding(b.Type, b.Tile.GridX, b.Tile.GridY)).ToArray();

            Core.Data.Saved.Buildings.Set(list);
        }

        public void LoadBuildings()
        {
            SerializableBuilding[] list = Core.Data.Saved.Buildings;

            if (list == null)
                return;

            foreach (var b in list)
            {
                if (buildingsGrid[b.X, b.Y].Building == null)
                {
                    var bs = ResourceMock.GetBuilding(b.Type);
                    PlaceBuilding(bs, b.X, b.Y);
                }
                else
                {
                    if (buildingsGrid[b.X, b.Y].Building.Type != b.Type)
                    {
                        RemoveBuilding(buildingsGrid[b.X, b.Y].Building);
                        var bs = ResourceMock.GetBuilding(b.Type);
                        PlaceBuilding(bs, b.X, b.Y);
                    }
                }
            }
        }

        public BuildingTile GetCell(Vector2 gridPosition)
        {
            return GetCell((int)gridPosition.x, (int)gridPosition.y);
        }

        public BuildingTile GetCell(int x, int y)
        {
            if (x < 0 || x >= numberOfColumns || y < 0 || y >= numberOfRows)
                return null;
            return buildingsGrid[x, y];
        }

        public Building GetAdjacentBuilding(Vector2 worldPosition)
        {
            Vector2 gridPos = GetClosestGridPositionIgnoringGridLimits(worldPosition);
            BuildingTile cell = GetCell(gridPos - new Vector2(1f, 0f));
            if (cell == null)
                return null;
            return cell.Building;
        }

        private Building GetAdjacentBuilding(int gridX, int gridY)
        {
            var buildingsArea = Core.Instance.BuildingsArea;
            if (!buildingsArea.IsCellFree(gridX - 1, gridY))
                return buildingsGrid[gridX - 1, gridY].Building;
            return null;
        }

        public List<Vector2> GetPathToRandomTarget(bool isAgressive, Vector2 worldPosition)
        {
            Vector2 grid = GetClosestGridPosition(worldPosition);
            return GetPathToRandomTarget(isAgressive, Convert.ToInt32(grid.x), Convert.ToInt32(grid.y));
        }

        private List<Vector2> GetPathToRandomTarget(bool isAgressive, int gridX, int gridY)
        {
            List<Point> possibleTargets = new List<Point>();
            var buildingsArea = Core.Instance.BuildingsArea;
            
            for (int i = buildingsArea.numberOfColumns; i < pathGrid.GetLength(0); i++)
                for (int j = 0; j < buildingsArea.numberOfRows; j++)
                    pathGrid[i, j] = PathFinderHelper.EMPTY_TILE;

            for (int i = 0; i < buildingsArea.numberOfColumns; i++)
            {
                for (int j = 0; j < buildingsArea.numberOfRows; j++)
                {
                    if (buildingsArea.IsCellFree(i, j)
                        && (i == buildingsArea.numberOfColumns - 1 || buildingsArea.IsCellFree(i + 1, j)))
                    {
                        pathGrid[i, j] = PathFinderHelper.EMPTY_TILE;
                    }
                    else
                    {
                        pathGrid[i, j] = PathFinderHelper.BLOCKED_TILE;
                    }
                }
            }

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


            if (isAgressive && !possibleTargets.Any())
                return GetPathToRandomTarget(false, gridX, gridY);

            var pathFinder = new PathFinder(pathGrid)
            {
                Diagonals = false,
            };

            var monsterPoint = new Point(gridX, gridY);
            var paths = new List<List<PathFinderNode>>();

            foreach (var t in possibleTargets)
            {
                var path = pathFinder.FindPath(monsterPoint, t);
                if (path != null)
                    paths.Add(new List<PathFinderNode>(path));
            }
            if (paths == null || paths.Count == 0)
                return null;
            return paths[Core.Instance.Random.Next(0, paths.Count)].Select(w => GetWorldPositionByGridPosition(w.X, w.Y)).ToList();
        }


    }
}