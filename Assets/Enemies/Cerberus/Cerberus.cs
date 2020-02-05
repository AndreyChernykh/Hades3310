using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Cerberus : Enemy {
    public Vector2 xRange;
    public List<float> yRange;
    public Vector2 waitTimeMs;

    State idleState = new State("idle");
    State relocateState = new State("relocate");
    State attackState = new State("attack");
    State returnState = new State("return");
    State hurtState = new State("hurt");
    State deadState = new State("dead");

    Coroutine waitForAttack;
    Coroutine waitStunned;
    Vector2 prevPosition;

    private void Start() {
        Init();
        Vector2 nextPosition = transform.position;
        prevPosition = nextPosition;

        idleState.enterActions.Add(new FSM.Action(() => {
            Debug.Log("idle");
            waitForAttack = StartCoroutine(Wait(rng.Next((int) waitTimeMs.x, (int) waitTimeMs.y) / 1000, relocateState));
        }));
        idleState.exitActions.Add(new FSM.Action(() => {
            if (waitForAttack != null) {
                StopCoroutine(waitForAttack);
            }
        }));

        relocateState.enterActions.Add(new FSM.Action(() => {
            Debug.Log("relocateState");
            nextPosition = new Vector2(transform.position.x, yRange[rng.Next(0, yRange.Count)]);
            Debug.Log(nextPosition);
        }));
        relocateState.stayActions.Add(new FSM.Action(() => {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed);
        }));
        relocateState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, nextPosition) < 0.1;
        }, attackState));

        attackState.enterActions.Add(new FSM.Action(() => {
            Debug.Log("attackState");
            prevPosition = nextPosition;
            nextPosition = new Vector2(transform.position.x < 0 ? xRange.y : xRange.x, transform.position.y);
            Debug.Log(nextPosition);
        }));
        attackState.stayActions.Add(new FSM.Action(() => {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed);
        }));
        attackState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, nextPosition) < 0.1;
        }, returnState));

        returnState.enterActions.Add(new FSM.Action(() => {
            Debug.Log("returnState");
            nextPosition = prevPosition;
            Debug.Log(nextPosition);
        }));
        returnState.stayActions.Add(new FSM.Action(() => {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed);
        }));
        returnState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, nextPosition) < 1;
        }, idleState));

        deadState.enterActions.Add(new FSM.Action(() => {
            Die();
            LevelLoader.Instance.LoadLevel(LEVELS.VICTORY);
        }));

        hurtState.enterActions.Add(new FSM.Action(() => {
            if (waitStunned != null) {
                StopCoroutine(waitStunned);
            }
            waitStunned = StartCoroutine(Wait(2, idleState));
        }));
        hurtState.exitActions.Add(new FSM.Action(() => {
            if (waitStunned != null) {
                StopCoroutine(waitStunned);
            }
        }));

        stateMachine.ChangeState(idleState);
    }

    private void Update() {
        stateMachine.Tick();
    }

    protected override void GotHit() {
        health -= Stats.currentPower;

        if (health <= 0) {
            stateMachine.ChangeState(deadState);
        }
        else {
            stateMachine.ChangeState(hurtState);
        }
    }
}
