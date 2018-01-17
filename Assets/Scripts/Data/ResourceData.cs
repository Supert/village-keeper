using Shibari;
using System;
using System.Collections.Generic;
using UnityEngine;
using VillageKeeper.Game;

namespace VillageKeeper.Model
{
    public class ResourceValue<TValue> : SecondaryValue<TValue> where TValue : UnityEngine.Object
    {
        public ResourceValue(Func<string> formatProvider, params IBindable[] formatValues) : base(() => ResourceMock.Get<TValue>(string.Format(formatProvider(), formatValues)), formatValues)
        {

        }
    }

    public class ResourceData : BindableData
    {
        [ShowInEditor]
        public ResourceValue<Sprite> CastleBackground { get; }
        [SerializeValue]
        public PrimaryValue<string> CastleBackgroundFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Sprite> MountainsBackground { get; }
        [SerializeValue]
        public PrimaryValue<string> MountainsBackgroundFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Sprite> VillageBackground { get; }
        [SerializeValue]
        public PrimaryValue<string> VillageBackgroundFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Sprite> Cliff { get; }
        [SerializeValue]
        public PrimaryValue<string> CliffFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Sprite> CastleUpgradeIcon { get; }
        [SerializeValue]
        public PrimaryValue<string> CastleUpgradeIconFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Sprite> ArrowBar { get; }
        [SerializeValue]
        public PrimaryValue<string> ArrowBarFormat { get; private set; }

        [ShowInEditor]
        public SecondaryValue<Building[]> BuildingPrefabs { get; }
        [SerializeValue]
        public PrimaryValue<string> BuildingPrefabsFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Sprite> FarmCrops { get; }
        [SerializeValue]
        public PrimaryValue<string> FarmCropsFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Sprite> BuildingTile { get; }
        [SerializeValue]
        public PrimaryValue<string> BuildingTileFormat { get; private set; }

        [ShowInEditor]
        public ResourceValue<Ghost> GhostPrefab { get; }
        [SerializeValue]
        public PrimaryValue<string> GhostPrefabFormat { get; private set; }

        [SerializeValue]
        public PrimaryValue<string> AdUnitId { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            CastleBackground = new ResourceValue<Sprite>(CastleBackgroundFormat.Get, Data.Common.Special, Data.Saved.VillageLevel);
        }
    }
}