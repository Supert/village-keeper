using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System;
using Soomla.Store;
using VillageKeeper.Game;

namespace VillageKeeper
{
    public class DataScript : MonoBehaviour
    {
        public enum DataFields
        {
            HasPremium,
            MonstersDefeated,
            Gold,
            VillageLevel,
            Buildings,
            WasInBuildTipShown,
            WasInBattleTipShown,
            IsSoundEffectsEnabled,
            IsMusicEnabled,
        }

        public class DataFieldChangedEventArgs : EventArgs
        {
            public DataFields FieldChanged;
            public DataFieldChangedEventArgs(DataFields field)
            {
                FieldChanged = field;
            }
        }

        public event EventHandler<DataFieldChangedEventArgs> DataFieldChanged;

        public bool HasPremium
        {
            get
            {
                if (PlayerPrefs.HasKey("HasPremium"))
                    return PlayerPrefs.GetInt("HasPremium") != 0;
                else
                    return false;
            }
            set
            {
                PlayerPrefs.SetInt("HasPremium", value ? 1 : 0);
                FireDataFieldChanged(DataFields.HasPremium);
            }
        }

        public bool WasInBuildTipShown
        {
            get
            {
                if (PlayerPrefs.HasKey("WasInBuildTipShown"))
                    return PlayerPrefs.GetInt("WasInBuildTipShown") != 0;
                else
                    return false;
            }
            set
            {
                PlayerPrefs.SetInt("WasInBuildTipShown", value ? 1 : 0);
                FireDataFieldChanged(DataFields.WasInBuildTipShown);
            }
        }

        public bool WasInBattleTipShown
        {
            get
            {
                if (PlayerPrefs.HasKey("WasInBattleTipShown"))
                    return PlayerPrefs.GetInt("WasInBattleTipShown") != 0;
                else
                    return false;
            }
            set
            {
                PlayerPrefs.SetInt("WasInBattleTipShown", value ? 1 : 0);
                FireDataFieldChanged(DataFields.WasInBattleTipShown);
            }
        }

        public int MonstersDefeated
        {
            get
            {
                if (PlayerPrefs.HasKey("MonstersDefeated"))
                    return PlayerPrefs.GetInt("MonstersDefeated");
                else
                    return 0;
            }
            set
            {
                PlayerPrefs.SetInt("MonstersDefeated", value);
                FireDataFieldChanged(DataFields.MonstersDefeated);
            }
        }

        public int Gold
        {
            get
            {
                if (PlayerPrefs.HasKey("Gold"))
                    return PlayerPrefs.GetInt("Gold");
                else return 50;
            }
            set
            {
                PlayerPrefs.SetInt("Gold", value);
                FireDataFieldChanged(DataFields.Gold);
            }
        }

        public int VillageLevel
        {
            get
            {
                if (PlayerPrefs.HasKey("VillageLevel"))
                    return PlayerPrefs.GetInt("VillageLevel");
                else return 0;
            }
            set
            {
                PlayerPrefs.SetInt("VillageLevel", value);
                FireDataFieldChanged(DataFields.VillageLevel);
            }
        }

        public SerializableBuildingsList Buildings
        {
            get
            {
                if (PlayerPrefs.HasKey("Buildings"))
                {
                    var xsBuildings = new XmlSerializer(typeof(SerializableBuildingsList));
                    var textReader = new StringReader(PlayerPrefs.GetString("Buildings"));
                    return (SerializableBuildingsList)xsBuildings.Deserialize(textReader);
                }
                return new SerializableBuildingsList();
            }
            set
            {
                var xsBuildings = new XmlSerializer(typeof(SerializableBuildingsList));
                var s = new StringWriter();
                xsBuildings.Serialize(s, value);
                PlayerPrefs.SetString("Buildings", s.ToString());
                FireDataFieldChanged(DataFields.Buildings);
            }
        }

        public bool IsSoundEffectsEnabled
        {
            get
            {
                if (PlayerPrefs.HasKey("IsSoundEffectsEnabled"))
                    return PlayerPrefs.GetInt("IsSoundEffectsEnabled") != 0;
                return true;
            }
            set
            {
                PlayerPrefs.SetInt("IsSoundEffectsEnabled", value ? 1 : 0);
                FireDataFieldChanged(DataFields.IsSoundEffectsEnabled);
            }
        }

        public bool IsMusicEnabled
        {
            get
            {
                if (PlayerPrefs.HasKey("IsMusicEnabled"))
                    return PlayerPrefs.GetInt("IsMusicEnabled") != 0;
                return true;
            }
            set
            {
                PlayerPrefs.SetInt("IsMusicEnabled", value ? 1 : 0);
                FireDataFieldChanged(DataFields.IsMusicEnabled);
            }
        }

        private void FireDataFieldChanged(DataFields field)
        {
            if (DataFieldChanged != null)
                DataFieldChanged(this, new DataFieldChangedEventArgs(field));
        }

        public int GetMonsterBonusGold()
        {
            return 20;
        }

        public int GetFarmsFood()
        {
            return CoreScript.Instance.BuildingsArea.buildings.Where(x => x.Type == BuildingScript.BuildingTypes.Farm).Count();
        }

        public int GetWindmillBonusFood()
        {
            int result = 0;
            foreach (WindmillScript w in CoreScript.Instance.BuildingsArea.buildings.Where(x => x.Type == BuildingScript.BuildingTypes.Windmill))
                result += w.WindmillBonus;
            return result;
        }

        public int GetBreadToGoldMultiplier()
        {
            return GetBreadToGoldMultiplier(VillageLevel);
        }

        public int GetBreadToGoldMultiplier(int villageLevel)
        {
            return villageLevel + 1;
        }

        public int GetRoundFinishedBonusGold()
        {
            int result = GetMonsterBonusGold();
            int food = GetFarmsFood() + GetWindmillBonusFood();
            result += food * GetBreadToGoldMultiplier();
            return result;
        }

        public int GetCastleUpgradeCost()
        {
            if (VillageLevel == 0)
                return 600;
            if (VillageLevel == 1)
                return 6000;
            return 0;
        }

        public void OnItemPurchased(PurchasableVirtualItem pvi, string payload)
        {
            if (pvi.ItemId == EconomyAssets.THOUSAND_COINS.ItemId)
                Gold += 1000;
            if (pvi.ItemId == EconomyAssets.TEN_THOUSAND_COINS.ItemId)
                Gold += 10000;
            HasPremium = true;
        }

        IEnumerator InitCoroutine()
        {
            yield return null;
            StoreEvents.OnItemPurchased += OnItemPurchased;
        }

        void Start()
        {
            StartCoroutine(InitCoroutine());
            //CoreScript.Instance.GameStateChanged += (sender, e) =>
            //{
            //    switch (e.NewState)
            //    {
            //        case CoreScript.GameStates.RoundFinished:
            //            MonstersDefeated++;
            //            Gold += GetRoundFinishedBonusGold();
            //            break;
            //    }
            //};
        }
    }
}