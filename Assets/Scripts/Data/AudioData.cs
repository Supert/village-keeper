using Shibari;

namespace VillageKeeper.Data
{
    public class AudioData : BindableData
    {
        public SerializableField<StringArray> ArrowShots { get; private set; }
        public SerializableField<StringArray> BackgroundPeace { get; private set; }
        public SerializableField<StringArray> BackgroundBattle { get; private set; }
        public SerializableField<StringArray> BuildingHit { get; private set; }
        public SerializableField<StringArray> Click { get; private set; }
        public SerializableField<StringArray> MonsterSounds { get; private set; }
        public SerializableField<StringArray> MonsterHit { get; private set; }
    }
}