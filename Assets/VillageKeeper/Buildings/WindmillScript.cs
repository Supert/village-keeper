using System.Collections.Generic;
using System.Linq;

namespace VillageKeeper.Game
{
    public class WindmillScript : BuildingScript
    {

        public override BuildingTypes Type
        {
            get
            {
                return BuildingTypes.Windmill;
            }
        }
        public int WindmillBonus
        {
            get
            {
                var listOfCells = new List<BuildingTileScript>();
                for (int i = (int)Tile.gridX - 1; i <= (int)Tile.gridX + 1; i++)
                    for (int j = (int)Tile.gridY - 1; j <= (int)Tile.gridY + 1; j++)
                    {
                        listOfCells.Add(CoreScript.Instance.BuildingsArea.GetCell(i, j));
                    }
                return listOfCells.Where(x => x != null).Where(y => y.Building != null).Where(z => z.Building.Type == BuildingTypes.Farm).Count();
            }
        }
    }
}