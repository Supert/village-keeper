using Shibari;

namespace VillageKeeper.Model
{
    public class AudioData : BindableData
    {
        [SerializeValue]
        public AssignableValue<string[]> ArrowShots { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> BackgroundPeace { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> BackgroundBattle { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> BuildingHit { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> Click { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> MonsterSounds { get; private set; }
        [SerializeValue]
        public AssignableValue<string[]> MonsterHit { get; private set; }
    }
}