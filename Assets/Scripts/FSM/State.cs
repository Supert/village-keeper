using System;

namespace VillageKeeper.FSM
{
    public abstract class State
    {
        public abstract States Type { get; }

        public event Action OnEnter;
        public event Action OnExit;

        public virtual void Enter()
        {
            OnEnter?.Invoke();
        }

        public virtual States Event(StateMachineEvents type, params object[] args) { return States.Empty; }

        public virtual void Exit()
        {
            OnExit?.Invoke();
        }
    }
}