using UnityEngine;
using Shibari;
using static UnityEngine.Resources;

namespace VillageKeeper.Model
{

    public class Data : BindableData
    {
        [ShowInEditor]
        public FormattedAndLocalizedData FormattedAndLocalized { get; private set; }

        [ShowInEditor]
        public LocalizationData Localization { get; private set; }

        [ShowInEditor]
        public ResourceData Resources { get; private set; }

        [ShowInEditor]
        public ResourcePathData ResourcePaths { get; private set; }

        [ShowInEditor]
        public SavedData Saved { get; private set; }

        [ShowInEditor]
        public CommonData Common { get; private set; }

        [ShowInEditor]
        public GameData Game { get; private set; }

        [ShowInEditor]
        public BalanceData Balance { get; private set; }

        [ShowInEditor]
        public AudioData Audio { get; private set; }

        [ShowInEditor]
        public ShopData Shop { get; private set; }

        public override void Initialize()
        {
            Common = new CommonData();
            Localization = new LocalizationData();
            Balance = new BalanceData();
            Saved = new SavedData();
            Game = new GameData();
            FormattedAndLocalized = new FormattedAndLocalizedData();
            ResourcePaths = new ResourcePathData();
            Resources = new ResourceData();
            Audio = new AudioData();
            Shop = new ShopData();

            base.Initialize();

            ResourcePaths.Deserialize(Load<TextAsset>("Data/ResourcePathData").text);
            Localization.Deserialize(Load<TextAsset>("Data/LocalizationData").text);
            Audio.Deserialize(Load<TextAsset>("Data/AudioData").text);
            Resources.Deserialize(Load<TextAsset>("Data/ResourceData").text);
            Balance.Deserialize(Load<TextAsset>("Data/BalanceData").text);
            Saved.Deserialize(Load<TextAsset>("Data/SavedData").text);
        }
    }
}
