using UnityEngine;
using static Shibari.Model;
using static UnityEngine.Resources;

namespace VillageKeeper.Model
{
    public static class Data
    {
        public static FormattedAndLocalizedData FormattedAndLocalizedData { get { return Get<FormattedAndLocalizedData>("FormattedAndLocalized"); } }
        public static LocalizationData Localization { get { return Get<LocalizationData>("Localization"); } }
        public static ResourceData Resources { get { return Get<ResourceData>("Resources"); } }
        public static ResourcePathData ResourcePaths { get { return Get<ResourcePathData>("ResourcePaths"); } }
        public static SavedData Saved { get { return Get<SavedData>("Saved"); } }
        public static CommonData Common { get { return Get<CommonData>("Common"); } }
        public static GameData Game { get { return Get<GameData>("Game"); } }
        public static BalanceData Balance { get { return Get<BalanceData>("Balance"); } }
        public static AudioData Audio { get { return Get<AudioData>("Audio"); } }

        public static void Init()
        {
            Saved.Deserialize(Load<TextAsset>("Data/SavedData").text);
            ResourcePaths.Deserialize(Load<TextAsset>("Data/ResourcePathData").text);
            Localization.Deserialize(Load<TextAsset>("Data/LocalizationData").text);
            Balance.Deserialize(Load<TextAsset>("Data/BalanceData").text);
            Audio.Deserialize(Load<TextAsset>("Data/AudioData").text);
            Resources.Deserialize(Load<TextAsset>("Data/ResourceData").text);

            Common.Init();
        }
    }
}
