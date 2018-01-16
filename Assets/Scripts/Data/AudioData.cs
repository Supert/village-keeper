using Shibari;

namespace VillageKeeper.Data
{
    public class AudioData : BindableData
    {
        [SerializeValue]
        public BindableField<string[]> ArrowShots { get; private set; }
        [SerializeValue]
        public BindableField<string[]> BackgroundPeace { get; private set; }
        [SerializeValue]
        public BindableField<string[]> BackgroundBattle { get; private set; }
        [SerializeValue]
        public BindableField<string[]> BuildingHit { get; private set; }
        [SerializeValue]
        public BindableField<string[]> Click { get; private set; }
        [SerializeValue]
        public BindableField<string[]> MonsterSounds { get; private set; }
        [SerializeValue]
        public BindableField<string[]> MonsterHit { get; private set; }
    }
}