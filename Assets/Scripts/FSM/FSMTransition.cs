namespace FSM {
	public class Transition {
		// The method to evaluate if the transition is ready to fire
		public Condition condition;
		// A list of actions to perform when this transition fires
		private Action[] fireActions;

		public IState nextState;

		public Transition(Condition condition, IState nextState, Action[] actions = null) {
			this.condition = condition;
			this.nextState = nextState;
			this.fireActions = actions;
		}

		// Call all  actions
		public void Fire() {
			if (fireActions != null) {
				foreach (Action action in fireActions) {
					action.Act();
				}
			}
		}
	}
}