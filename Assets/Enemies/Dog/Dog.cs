using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Dog : Enemy {
    public Vector2 waitTimeMs;
    public float chaseTime;
    float chaseTimeLeft;

    State idleState = new State("idle");
    State attackState = new State("attack");
    State chaseState = new State("chase");
    State hurtState = new State("hurt");
    State deadState = new State("dead");

    Coroutine waitAfterAttack;
    Coroutine waitForChaseCoroutine;
    Coroutine chaseCoroutine;
    Coroutine waitStunned;

    private void Start() {
        Init();

        idleState.enterActions.Add(new FSM.Action(() => {
            if (waitForChaseCoroutine != null) {
                StopCoroutine(waitForChaseCoroutine);
            }
            waitForChaseCoroutine = StartCoroutine(Wait(rng.Next((int) waitTimeMs.x, (int) waitTimeMs.y) / 1000, chaseState));
        }));
        idleState.exitActions.Add(new FSM.Action(() => {
            if (waitForChaseCoroutine != null) {
                StopCoroutine(waitForChaseCoroutine);
            }
        }));

        chaseState.enterActions.Add(new FSM.Action(() => {
            chaseTimeLeft = chaseTime;
            chaseCoroutine = StartCoroutine(Chase());
        }));
        chaseState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, player.transform.position) < 1;
        }, attackState));
        chaseState.exitActions.Add(new FSM.Action(() => {
            if (chaseCoroutine != null) {
                StopCoroutine(chaseCoroutine);
            }
        }));

        attackState.enterActions.Add(new FSM.Action(() => {
            waitAfterAttack = StartCoroutine(Wait(1, idleState));
        }));
        attackState.exitActions.Add(new FSM.Action(() => {
            if (waitAfterAttack != null) {
                StopCoroutine(waitAfterAttack);
            }
        }));

        deadState.enterActions.Add(new FSM.Action(() => {
            Die();
        }));

        hurtState.enterActions.Add(new FSM.Action(() => {
            if (waitStunned != null) {
                StopCoroutine(waitStunned);
            }
            waitStunned = StartCoroutine(Wait(1, chaseState));
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

    protected IEnumerator Chase() {
        Debug.Log(chaseTimeLeft);
        while (chaseTimeLeft >= 0.0f) {
            chaseTimeLeft -= Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);

            yield return null;
        }
        stateMachine.ChangeState(idleState);
    }

    protected override void GotHit(Weapon weapon) {
        health -= 1;

        if (health <= 0) {
            stateMachine.ChangeState(deadState);
        }
        else {
            stateMachine.ChangeState(hurtState);
        }
    }
}
