using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM {
	// Defer function to trigger activation condition
	// Returns true when transition can fire
	public delegate bool Condition();

	public class State : IState {
		// Arrays of actions to perform based on transitions fire (or not)
		public List<Action> enterActions = new List<Action>();
		public List<Action> stayActions = new List<Action>();
		public List<Action> exitActions = new List<Action>();
		public List<Transition> transitions = new List<Transition>();

		public string name;

		public State(string name) {
			this.name = name;
		}

		public string GetName() {
			return name;
		}

		public void Enter() {
			ExecuteActions(enterActions);
		}

		public void Stay() {
			ExecuteActions(stayActions);
		}

		public void Exit() {
			ExecuteActions(exitActions);
		}

		private void ExecuteActions(List<Action> actions) {
			for (int i = 0; i < actions.Count; i++) {
				actions[i].Act();
			}
		}

		public Transition VerifyTransitions() {
			for (int i = 0; i < transitions.Count; i++) {
				Transition t = transitions[i];
				bool decisionSucceded = t.condition();
				if (decisionSucceded) {
					return t;
				}
			}

			return null;
		}
	}
}
