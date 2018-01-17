using UnityEngine;
using Shibari;

namespace VillageKeeper.Model
{
    public static class Data
    {
        public static FormattedAndLocalizedData FormattedAndLocalizedData { get; private set; }
        public static LocalizationData Localization { get; private set; }
        public static ResourceData Resources { get; private set; }
        public static SavedData Saved { get; private set; }
        public static CommonData Common { get; private set; }
        public static GameData Game { get; private set; }
        public static BalanceData Balance { get; private set; }
        public static AudioData Audio { get; private set; }

        public static void Init()
        {
            Localization = Shibari.Model.Get<LocalizationData>("Localization");
            Resources = Shibari.Model.Get<ResourceData>("Resources");
            Saved = Shibari.Model.Get<SavedData>("Saved");
            Common = Shibari.Model.Get<CommonData>("Common");
            Game = Shibari.Model.Get<GameData>("Game");
            Balance = Shibari.Model.Get<BalanceData>("Balance");
            Audio = Shibari.Model.Get<AudioData>("Audio");

            Saved.Deserialize(UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.SavedData").text);
            Resources.Deserialize(UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.ResourceData").text);
            Localization.Deserialize(UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.LocalizationData").text);
            Balance.Deserialize(UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.BalanceData").text);
            Audio.Deserialize(UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.AudioData").text);

            Common.Init();
            Game.Init();
        }
    }
}
