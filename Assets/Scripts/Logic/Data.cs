using UnityEngine;
using Shibari;

namespace VillageKeeper.Data
{
    public class Data
    {
        public LocalizationData Localization { get; private set; }
        public ResourceData Resources { get; private set; }
        public SavedData Saved { get; private set; }
        public CommonData Common { get; private set; }
        public GameData Game { get; private set; }
        public BalanceData Balance { get; private set; }
        public AudioData Audio { get; private set; }

        public Data()
        {
            Localization = Model.Get<LocalizationData>("Localization");
            Resources = Model.Get<ResourceData>("Resources");
            Saved = Model.Get<SavedData>("Saved");
            Common = Model.Get<CommonData>("Common");
            Game = Model.Get<GameData>("Game");
            Balance = Model.Get<BalanceData>("Balance");
            Audio = Model.Get<AudioData>("Audio");

            Model.DeserializeData("Saved", UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.SavedData").text);
            Model.DeserializeData("Resources", UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.ResourceData").text);
            Model.DeserializeData("Localization", UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.LocalizationData").text);
            Model.DeserializeData("Balance", UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.BalanceData").text);
            Model.DeserializeData("Audio", UnityEngine.Resources.Load<TextAsset>("Data/VillageKeeper.Data.AudioData").text);
        }

        public void Init()
        {
            Common.Init();
            Game.Init();
        }
    }
}
