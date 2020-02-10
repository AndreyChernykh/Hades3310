using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Skull : Enemy {
    [SerializeField]
    private Vector2 xRange;
    [SerializeField]
    private Vector2 yRange;
    [SerializeField]
    private Vector2 waitTimeMs;

    private State idleState = new State("idle");
    private State walkState = new State("walk");
    private State attackState = new State("attack");
    private State hurtState = new State("hurt");
    private State deadState = new State("dead");

    private Coroutine waitForAttack;
    private Coroutine waitStunned;
    private bool isHorizontalMove = false;

    private void Start() {
        InitEnemy();

        Vector2 nextPosition = transform.position;

        idleState.enterActions.Add(new FSM.Action(() => {
            waitForAttack = StartCoroutine(Wait(rng.Next((int) waitTimeMs.x, (int) waitTimeMs.y) / 1000, attackState));
        }));
        idleState.exitActions.Add(new FSM.Action(() => {
            if (waitForAttack != null) {
                StopCoroutine(waitForAttack);
            }
        }));

        attackState.enterActions.Add(new FSM.Action(() => {
            if (isHorizontalMove) {
                nextPosition = new Vector2(transform.position.x < 0 ? xRange.y : xRange.x, transform.position.y);
            }
            else {
                nextPosition = new Vector2(transform.position.x, (float) rng.Next((int) yRange.x, (int) yRange.y));
            }
            isHorizontalMove = !isHorizontalMove;
        }));

        attackState.stayActions.Add(new FSM.Action(() => {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed);
        }));

        attackState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, nextPosition) < 1;
        }, idleState));

        deadState.enterActions.Add(new FSM.Action(() => {
            Die();
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
