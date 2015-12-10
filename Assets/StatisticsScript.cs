using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
public class StatisticsScript : MonoBehaviour {
	public bool HasPremium {
		get {
			if (PlayerPrefs.HasKey ("HasPremium"))
				return PlayerPrefs.GetInt ("HasPremium") != 0;
			else
				return false;
		}
		set {
			PlayerPrefs.SetInt ("HasPremium", value ? 1 : 0);
		}
	}
	public int MonstersDefeated {
		get {
			if (PlayerPrefs.HasKey ("MonstersDefeated"))
				return PlayerPrefs.GetInt ("MonstersDefeated");
			else
				return 0;
		}
		set {
			PlayerPrefs.SetInt ("MonstersDefeated", value);
		}
	}
	public int Gold {
		get {
			if (PlayerPrefs.HasKey ("Gold"))
				return PlayerPrefs.GetInt ("Gold");
			else return 0;
		}
		set {
			PlayerPrefs.SetInt ("Gold", value);
		}
	}
	public int VillageLevel {
		get {
			if (PlayerPrefs.HasKey ("VillageLevel"))
				return PlayerPrefs.GetInt ("VillageLevel");
			else return 0;
		}
		set {
			PlayerPrefs.SetInt ("VillageLevel", value);
		}
	}
	public SerializableBuildingsList Buildings {
		get {
			if (PlayerPrefs.HasKey ("Buildings")) {
				var xsBuildings = new XmlSerializer (typeof (SerializableBuildingsList));
				var textReader = new StringReader (PlayerPrefs.GetString ("Buildings"));
				return (SerializableBuildingsList) xsBuildings.Deserialize (textReader);
			}
			return new SerializableBuildingsList ();
		}
		set {
			var xsBuildings = new XmlSerializer (typeof (SerializableBuildingsList));
			var s = new StringWriter ();
			xsBuildings.Serialize (s, value);
			PlayerPrefs.SetString ("Buildings", s.ToString ());
		}
	}
	// Use this for initialization
	public int GetMonsterBonusGold () {
		return 20;
	}
	public int GetFarmsFood () {
		return CoreScript.Instance.BuildingsArea.buildings.Where (x => x.Type == BuildingScript.BuildingTypes.Farm).Count ();
	}
	public int GetWindmillBonusFood () {
		int result = 0;
		foreach (WindmillScript w in CoreScript.Instance.BuildingsArea.buildings.Where (x => x.Type == BuildingScript.BuildingTypes.Windmill))
			result += w.WindmillBonus;
		return result;
	}
	public int GetRoundFinishedBonusGold () {
		int result = GetMonsterBonusGold ();
		int food = GetFarmsFood () + GetWindmillBonusFood ();
		switch (VillageLevel) {
		case 0:
			result += food * 1;
			break;
		case 1:
			result += food * 2;
			break;
		case 2: 
			result += food * 3;
			break;
		}
		return result;
		
	}
	void Start () {
		CoreScript.Instance.GameStateChanged += (sender, e) => {
			switch (e.NewState) {
			case CoreScript.GameStates.RoundFinished:
				this.MonstersDefeated++;
				this.Gold += GetRoundFinishedBonusGold ();
				break;
			}
		};
	}
	// Update is called once per frame
	void Update () {
	
	}
}
