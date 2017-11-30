using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using VillageKeeper.Game;

namespace VillageKeeper.Data
{
    public class BuildingsDataField : DataField<SerializableBuildingsList>
    {
        private string id;
        protected override string Id { get { return id; } }

        public BuildingsDataField(string id, SerializableBuildingsList defaultValue = default(SerializableBuildingsList))
        {
            this.id = id;
            DefaultValue = defaultValue;
        }

        public override SerializableBuildingsList Get()
        {
            if (PlayerPrefs.HasKey("Buildings"))
            {
                var xsBuildings = new XmlSerializer(typeof(SerializableBuildingsList));
                var textReader = new StringReader(PlayerPrefs.GetString("Buildings"));
                return (SerializableBuildingsList)xsBuildings.Deserialize(textReader);
            }
            return DefaultValue;
        }

        public override void Set(SerializableBuildingsList value)
        {
            var xsBuildings = new XmlSerializer(typeof(SerializableBuildingsList));
            var s = new StringWriter();
            xsBuildings.Serialize(s, value);
            PlayerPrefs.SetString("Buildings", s.ToString());
            base.Set(value);
        }
    }
}