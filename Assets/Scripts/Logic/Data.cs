using UnityEngine;
using Shibari;
using static UnityEngine.Resources;

namespace VillageKeeper.Model
{
    public class Data : BindableData
    {
        [ShowInEditor]
        public FormattedAndLocalizedData FormattedAndLocalized { get; } = new FormattedAndLocalizedData();

        [ShowInEditor]
        public LocalizationData Localization { get; } = new LocalizationData();

        [ShowInEditor]
        public ResourceData Resources { get; } = new ResourceData();

        [ShowInEditor]
        public ResourcePathData ResourcePaths { get; } = new ResourcePathData();

        [ShowInEditor]
        public SavedData Saved { get; } = new SavedData();

        [ShowInEditor]
        public CommonData Common { get; } = new CommonData();

        [ShowInEditor]
        public GameData Game { get; } = new GameData();

        [ShowInEditor]
        public BalanceData Balance { get; } = new BalanceData();

        [ShowInEditor]
        public AudioData Audio { get; } = new AudioData();

        public override void Initialize()
        {
            base.Initialize();

            Saved.Deserialize(Load<TextAsset>("Data/SavedData").text);
            ResourcePaths.Deserialize(Load<TextAsset>("Data/ResourcePathData").text);
            Localization.Deserialize(Load<TextAsset>("Data/LocalizationData").text);
            Balance.Deserialize(Load<TextAsset>("Data/BalanceData").text);
            Audio.Deserialize(Load<TextAsset>("Data/AudioData").text);
            Resources.Deserialize(Load<TextAsset>("Data/ResourceData").text);
        }
    }
}
