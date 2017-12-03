using VillageKeeper.Data;

namespace VillageKeeper.Locale
{
    public class Localization
    {
        public string GetBuildingName(BuildingTypes type)
        {
            switch (type)
            {
                case BuildingTypes.Farm:
                    return "Farm";
                case BuildingTypes.WallStone:
                    return "Stone Wall";
                case BuildingTypes.WallWooden:
                    return "Wooden Wall";
                case BuildingTypes.WatchtowerStone:
                    return "Stone Tower";
                case BuildingTypes.WatchtowerWooden:
                    return "Watchtower";
                case BuildingTypes.Windmill:
                    return "Windmill";
                default:
                    return "";
            }
        }

        public string GetBuildingDescription(BuildingTypes type)
        {
            switch (type)
            {
                case BuildingTypes.Farm:
                    return "Provides one point of food. Food converts to gold each round.";
                case BuildingTypes.WallStone:
                    return "Steady stone wall.";
                case BuildingTypes.WallWooden:
                    return "Cheap stockade can take few hits";
                case BuildingTypes.WatchtowerStone:
                    return "Harder better faster stronger watchtower.";
                case BuildingTypes.WatchtowerWooden:
                    return "Shoots at monster if it came close.";
                case BuildingTypes.Windmill:
                    return "Provides extra food for each adjacent Farm at end of the round";
                default:
                    return "";
            }
        }

        public string[] GetBuildHelpTips()
        {
            return new string[] {
                "Welcome to Village Keeper, Keeper! We just settled down here, in beautiful Unknown.",
                "Drag and drop farm to build it. Build defenses, too.",
                "One or two wooden watchtowers behind stockade would be enough at first.",
                "Note that enemies always come from right side.",
                "Click red button at top to read these tips again. Good luck, Keeper!"
            };
        }
        public string[] GetBattleHelpTips()
        {
            return new string[] {
                "Whoa! This monster is huge! Well, the bigger it is, the harder it fall.",
                "Do you see me standing at cliff? Swipe there to the left to draw a bow, then click at monster to shoot.",
                "Don't hesitate to shoot as fast as you can. We are not short of arrows.",
                "Click red button at top to read these tips again. To arms, Keeper!"
            };
        }
    }
}