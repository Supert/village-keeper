using UnityEngine;
using Shibari;
using static UnityEngine.Resources;

namespace VillageKeeper.Model
{
    public class Data : BindableData
    {
        public FormattedAndLocalizedData FormattedAndLocalized { get; } = new FormattedAndLocalizedData();
        public LocalizationData Localization { get; } = new LocalizationData();
        public ResourceData Resources { get; } = new ResourceData();
        public ResourcePathData ResourcePaths { get; } = new ResourcePathData();
        public SavedData Saved { get; } = new SavedData();
        public CommonData Common { get; } = new CommonData();
        public GameData Game { get; } = new GameData();
        public BalanceData Balance { get; } = new BalanceData();
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
