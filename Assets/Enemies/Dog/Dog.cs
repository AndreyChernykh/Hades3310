using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Dog : Enemy {
    [SerializeField]
    private Vector2 waitTimeMs;
    [SerializeField]
    private float chaseTime;
    private float chaseTimeLeft;

    private State idleState = new State("idle");
    private State attackState = new State("attack");
    private State chaseState = new State("chase");
    private State hurtState = new State("hurt");
    private State deadState = new State("dead");

    private Coroutine waitAfterAttack;
    private Coroutine waitForChaseCoroutine;
    private Coroutine chaseCoroutine;
    private Coroutine waitStunned;

    private void Start() {
        InitEnemy();

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
        while (chaseTimeLeft >= 0.0f) {
            chaseTimeLeft -= Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            yield return null;
        }
        stateMachine.ChangeState(idleState);
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
