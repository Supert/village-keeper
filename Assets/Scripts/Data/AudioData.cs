using Shibari;

namespace VillageKeeper.Model
{
    public class AudioData : BindableData
    {
        [SerializeValue]
        public AssignableValue<string[]> ArrowShots { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> BackgroundPeace { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> BackgroundBattle { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> BuildingHit { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> Click { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> MonsterSounds { get; } = new AssignableValue<string[]>();
        [SerializeValue]
        public AssignableValue<string[]> MonsterHit { get; } = new AssignableValue<string[]>();
    }
}