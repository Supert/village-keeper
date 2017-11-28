namespace VillageKeeper.FSM
{
    public abstract class State<T>
    {
        public virtual void Enter() { }

        public virtual State<T> Event(T args) { return this; }

        public virtual void Exit() { }
    }
}