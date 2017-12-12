using System;
using Shibari;

namespace VillageKeeper.Data
{
    public class LocalizationMapper : BindableMapper
    {
        public string GetCurrentTip(int i)
        {
            return "";
        }
    }

    public class LocalizationData : BindableData
    {
        public SerializableField<string> TipFormat { get; private set; }
        public SerializableField<StringArray> BattleHelpTips { get; private set; }
        public SerializableField<StringArray> BuildHelpTips { get; private set; }
        public SerializableField<StringArray> BuildingDescriptions { get; private set; }
        public SerializableField<StringArray> BuildingNames { get; private set; }
    }
}