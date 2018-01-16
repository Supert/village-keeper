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
        public SerializableField<string[]> BattleHelpTips { get; private set; }
        public SerializableField<string[]> BuildHelpTips { get; private set; }
        public SerializableField<string[]> BuildingDescriptions { get; private set; }
        public SerializableField<string[]> BuildingNames { get; private set; }
    }
}