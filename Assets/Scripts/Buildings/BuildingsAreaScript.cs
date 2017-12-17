using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using VillageKeeper.Data;

namespace VillageKeeper.Game
{
    public class BuildingsAreaScript : MonoBehaviour
    {
        private RectTransform rect;
        public int numberOfColumns;
        public int numberOfRows;
        private BuildingTileScript[,] buildingsGrid;
        public List<Building> buildings = new List<Building>();

        void Awake()
        {
            rect = GetComponent<RectTransform>() as RectTransform;
            buildingsGrid = new BuildingTileScript[numberOfColumns, numberOfRows];
        }

        void Start()
        {
            var buildingTilesHolder = new GameObject("Building Tiles Holder");
            buildingTilesHolder.transform.localPosition = new Vector3(0, 0, 1f);
            for (int i = 0; i < numberOfColumns; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    buildingsGrid[i, j] = Instantiate(Resources.Load<BuildingTileScript>("Buildings/BuildingTile")) as BuildingTileScript;
                    buildingsGrid[i, j].gridX = i;
                    buildingsGrid[i, j].gridY = j;
                    buildingsGrid[i, j].gameObject.name = "Building Tile (" + i + "," + j + ")";
                    buildingsGrid[i, j].transform.parent = buildingTilesHolder.transform;
                    var lp = (Vector3)GetWorldPositionByGridPosition(new Vector2(i, j));
                    lp.z = lp.y;
                    buildingsGrid[i, j].transform.localPosition = lp;
                }
            }
            //CoreScript.Instance.GameStateChanged += (sender, e) =>
            //{
            //    switch (e.NewState)
            //    {
            //        case CoreScript.GameStates.InBuildMode:
            //            LoadBuildings();
            //            break;
            //        case CoreScript.GameStates.RoundFinished:
            //            SaveBuildings();
            //            break;
            //    }
            //};
        }

        public Vector2 CellWorldSize
        {
            get
            {
                return rect.TransformPoint(CellLocalSize) - rect.TransformPoint(Vector2.zero);
            }
        }

        public Vector2 CellLocalSize
        {
            get
            {
                return new Vector2(rect.rect.width / numberOfColumns, rect.rect.height / numberOfRows);
            }
        }

        public Vector2 GetClosestGridPositionIgnoringGridLimits(Vector2 worldPosition)
        {
            var point = (Vector2)worldPosition - GetWorldPositionByGridPosition(0, 0);
            var result = new Vector2(point.x / CellWorldSize.x, point.y / CellWorldSize.y);
            result.x = Mathf.RoundToInt(result.x);
            result.y = Mathf.RoundToInt(result.y);
            return result;
        }

        public Vector2 GetClosestGridPosition(Vector2 worldPosition)
        {
            var result = GetClosestGridPositionIgnoringGridLimits(worldPosition);
            if (result.x < 0)
                result.x = 0;
            if (result.x >= numberOfColumns)
                result.x = numberOfColumns - 1;
            if (result.y < 0)
                result.y = 0;
            if (result.y >= numberOfRows)
                result.y = numberOfRows - 1;
            return result;
        }

        public Vector2 GetWorldPositionByGridPosition(int x, int y)
        {
            var positionInLocal = new Vector2(CellLocalSize.x * (x + 0.5f), CellLocalSize.y * (y + 0.5f));
            var positionInLocalWithOffset = positionInLocal - (rect.rect.size / 2);
            return (Vector2)rect.TransformPoint(positionInLocalWithOffset);
        }

        public Vector2 GetWorldPositionByGridPosition(Vector2 cellIndex)
        {
            return GetWorldPositionByGridPosition((int)cellIndex.x, (int)cellIndex.y);
        }

        public bool IsCellFree(Vector2 cellIndex)
        {
            return IsCellsFree(cellIndex, cellIndex);
        }

        public bool IsCellsFree(Vector2 leftBottomCellIndex, Vector2 rightTopCellIndex)
        {
            var list = new List<Vector2>();
            for (int i = (int)leftBottomCellIndex.x; i <= (int)rightTopCellIndex.x; i++)
                for (int j = (int)leftBottomCellIndex.y; j <= (int)rightTopCellIndex.y; j++)
                    list.Add(new Vector2(i, j));
            return IsCellsFree(list);
        }

        public bool IsCellsFree(List<Vector2> cellsIndexes)
        {
            foreach (var c in cellsIndexes)
                if (buildingsGrid[(int)c.x, (int)c.y].Building != null)
                    return false;
            return true;
        }

        public void DamageCell(Vector2 gridPosition)
        {
            if (!IsCellFree(gridPosition))
            {
                buildingsGrid[(int)gridPosition.x, (int)gridPosition.y].Building.Damage();
            }
        }

        public void PlaceBuilding(Building building, int x, int y)
        {
            PlaceBuilding(building, new Vector2(x, y));
        }

        public void PlaceBuilding(Building building, Vector2 gridPosition)
        {
            if (IsCellFree(gridPosition))
            {
                building.Tile = buildingsGrid[(int)gridPosition.x, (int)gridPosition.y];
                building.Tile.Building = building;
                building.transform.parent = building.Tile.transform;
                building.transform.localPosition = (new Vector3(0, 0, -0.1f));
                buildings.Add(building);
            }
        }

        public void BuyBuilding(Building building, Vector2 gridPosition)
        {
            if (IsCellFree(gridPosition) && Core.Instance.Data.Saved.Gold.Get() >= building.GoldCost)
            {
                Core.Instance.Data.Saved.Gold.Set(Core.Instance.Data.Saved.Gold.Get() - building.GoldCost);
                PlaceBuilding(building, gridPosition);
                SaveBuildings();
            }
        }

        public void RemoveBuilding(Building building)
        {
            building.Tile.Building = null;
            building.Tile = null;
            buildings.Remove(building);
            Destroy(building.gameObject);
        }

        public void SaveBuildings()
        {
            var list = new SerializableBuildingsArray()
            {
                Values = buildings.Select(b => new SerializableBuilding(b.Type, b.Tile.gridX, b.Tile.gridY)).ToArray()
            };
            Core.Instance.Data.Saved.Buildings.Set(list);
        }

        public void LoadBuildings()
        {
            var list = Core.Instance.Data.Saved.Buildings.Get();
            foreach (var b in list.Values)
            {
                if (buildingsGrid[b.x, b.y].Building == null)
                {
                    var bs = ResourceMock.GetBuilding(b.type);
                    PlaceBuilding(bs, b.x, b.y);
                }
                else
                {
                    if (buildingsGrid[b.x, b.y].Building.Type != b.type)
                    {
                        RemoveBuilding(buildingsGrid[b.x, b.y].Building);
                        var bs = ResourceMock.GetBuilding(b.type);
                        PlaceBuilding(bs, b.x, b.y);
                    }
                }
            }
        }

        public BuildingTileScript GetCell(Vector2 gridPosition)
        {
            return GetCell((int)gridPosition.x, (int)gridPosition.y);
        }

        public BuildingTileScript GetCell(int x, int y)
        {
            if (x < 0 || x >= numberOfColumns || y < 0 || y >= numberOfRows)
                return null;
            return buildingsGrid[x, y];
        }
    }
}