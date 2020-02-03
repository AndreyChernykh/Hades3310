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
			foreach (Action action in enterActions) {
				action.Act();
			}
		}

		public void Stay() {
			foreach (Action action in stayActions) {
				action.Act();
			}
		}

		public void Exit() {
			foreach (Action action in exitActions) {
				action.Act();
			}
		}

		public Transition VerifyTransitions() {
			foreach (Transition t in transitions) {
				bool decisionSucceded = t.condition();
				if (decisionSucceded) {
					return t;
				}
			}

			return null;
		}

	}
}