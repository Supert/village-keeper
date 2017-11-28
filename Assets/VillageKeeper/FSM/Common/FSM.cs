namespace VillageKeeper.FSM
{
    public class FSM<T>
    {
        private State<T> current;

        public FSM(State<T> state)
        {
            current = state;
            current.Enter();
        }

        public void Event(T args)
        {
            State<T> result = current.Event(args);
            if (result == current)
                return;

            current.Exit();
            current = result;
            current.Enter();
        }
    }
}