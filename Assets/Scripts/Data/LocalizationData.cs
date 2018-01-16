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
        [SerializeValue]
        public BindableField<string> TipFormat { get; private set; }
        [SerializeValue]
        public BindableField<string[]> BattleHelpTips { get; private set; }
        [SerializeValue]
        public BindableField<string[]> BuildHelpTips { get; private set; }
        [SerializeValue]
        public BindableField<string[]> BuildingDescriptions { get; private set; }
        [SerializeValue]
        public BindableField<string[]> BuildingNames { get; private set; }
    }
}