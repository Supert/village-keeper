using System.Collections.Generic;
using System.Linq;
using VillageKeeper.Data;

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
    }
}