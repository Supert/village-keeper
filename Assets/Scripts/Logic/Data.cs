using UnityEngine;
using Shibari;

namespace VillageKeeper.Data
{
    public class Data
    {
        public LocalizationData Localization { get; private set; }
        public ResourceData Resource { get; private set; }
        public SavedData Saved { get; private set; }
        public CommonData Common { get; private set; }
        public GameData Game { get; private set; }
        public BalanceData Balance { get; private set; }

        public Data()
        {
            Resource = Model.Get<ResourceData>("Resources");
            Saved = Model.Get<SavedData>("Saved");
            Common = Model.Get<CommonData>("Common");
            Game = Model.Get<GameData>("Game");
            Localization = Model.Get<LocalizationData>("Localization");

            Model.DeserializeData("Saved", Resources.Load<TextAsset>("Data/VillageKeeper.Data.GameData").text);
            Model.DeserializeData("Resources", Resources.Load<TextAsset>("Data/VillageKeeper.Data.ResourceData").text);
            Model.DeserializeData("Localization", Resources.Load<TextAsset>("Data/VillageKeeper.Data.LocalizationData").text);
            Model.DeserializeData("Balance", Resources.Load<TextAsset>("Data/VillageKeeper.Data.BalanceData").text);
        }
    }
}
