using System;
using UnityEngine;
using VillageKeeper.Model;
using VillageKeeper.Game;

namespace VillageKeeper
{
    public static class ResourceMock
    {
        public static T Get<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public static Building GetBuilding(BuildingTypes type)
        {
            var path = string.Format(Data.ResourcePaths.CurrentBuildingPrefab, Enum.GetName(typeof(BuildingTypes), type));
            Building bs = UnityEngine.Object.Instantiate(Get<Building>(path));
            return bs;
        }
    }
}