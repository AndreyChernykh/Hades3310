namespace FSM {
    public interface IState {
        void Enter();
        void Stay();
        void Exit();
        string GetName();
        Transition VerifyTransitions();
    }
}