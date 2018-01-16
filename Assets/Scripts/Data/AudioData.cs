using Shibari;

namespace VillageKeeper.Data
{
    public class AudioData : BindableData
    {
        public SerializableField<string[]> ArrowShots { get; private set; }
        public SerializableField<string[]> BackgroundPeace { get; private set; }
        public SerializableField<string[]> BackgroundBattle { get; private set; }
        public SerializableField<string[]> BuildingHit { get; private set; }
        public SerializableField<string[]> Click { get; private set; }
        public SerializableField<string[]> MonsterSounds { get; private set; }
        public SerializableField<string[]> MonsterHit { get; private set; }
    }
}