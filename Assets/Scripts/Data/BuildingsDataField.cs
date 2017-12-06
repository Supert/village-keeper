using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using Shibari;

namespace VillageKeeper.Data
{
    public class BuildingsDataField : DataField<SerializableBuildingsList>
    {
        protected override SerializableBuildingsList GetDefaultValue()
        {
            if (PlayerPrefs.HasKey(Id))
            {
                var xsBuildings = new XmlSerializer(typeof(SerializableBuildingsList));
                var textReader = new StringReader(PlayerPrefs.GetString(Id));
                return (SerializableBuildingsList)xsBuildings.Deserialize(textReader);
            }
            return new SerializableBuildingsList();
        }

        public override void Set(SerializableBuildingsList value)
        {
            var xsBuildings = new XmlSerializer(typeof(SerializableBuildingsList));
            var s = new StringWriter();
            xsBuildings.Serialize(s, value);
            PlayerPrefs.SetString(Id, s.ToString());
            base.Set(value);
        }
    }
}