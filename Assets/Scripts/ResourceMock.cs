using System;
using UnityEngine;
using VillageKeeper.Data;
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
            var path = string.Format(Core.Instance.Data.Resources.BuildingPrefabs.Get(), Enum.GetName(typeof(BuildingTypes), type));
            Building bs = UnityEngine.Object.Instantiate(Get<Building>(path));
            return bs;
        }
    }
}