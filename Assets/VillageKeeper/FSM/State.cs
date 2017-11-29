namespace VillageKeeper.FSM
{
    public abstract class State
    {
        public abstract States Type { get; }

        public virtual void Enter() { }

        public virtual States Event(StateMachineEvents type, params object[] args) { return States.Empty; }

        public virtual void Exit() { }
    }
}