using Shibari;

namespace VillageKeeper.Model
{
    public class AudioData : BindableData
    {
        [SerializeValue]
        public PrimaryValue<string[]> ArrowShots { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> BackgroundPeace { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> BackgroundBattle { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> BuildingHit { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> Click { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> MonsterSounds { get; private set; }
        [SerializeValue]
        public PrimaryValue<string[]> MonsterHit { get; private set; }
    }
}