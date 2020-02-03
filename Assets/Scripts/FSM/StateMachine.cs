using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine
    {
        IState currentState;

        public void ChangeState(IState nextState)
        {
            // Debug.Log($"Enter state: {nextState.GetName()}");
            currentState?.Exit();
            currentState = nextState;
            currentState.Enter();
        }

        public void Tick()
        {
            Transition transition = currentState?.VerifyTransitions();
            if (transition != null)
            {
                transition.Fire();
                ChangeState(transition.nextState);
            }
            else
            {
                currentState?.Stay();
            }
        }
    }
}