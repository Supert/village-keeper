using System;
using UnityEngine;
using VillageKeeper.Model;
using VillageKeeper.Game;
using Shibari.UI;

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
            var path = string.Format(Core.Data.ResourcePaths.BuildingPrefabs, Enum.GetName(typeof(BuildingTypes), type));
            Building bs = UnityEngine.Object.Instantiate(Get<Building>(path));
            return bs;
        }

        public static SelectableSprites GetButtonSprites(string[] paths)
        {
            if (paths == null || paths.Length != 4)
                return new SelectableSprites(null, null, null, null);
            return new SelectableSprites(Get<Sprite>(paths[0]), Get<Sprite>(paths[1]), Get<Sprite>(paths[2]), Get<Sprite>(paths[3]));
        }
    }
}