using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class Cerberus : Enemy {
    [SerializeField]
    private Vector2 xRange;
    [SerializeField]
    private List<float> yRange;
    [SerializeField]
    private Vector2 waitTimeMs;

    private State idleState = new State("idle");
    private State relocateState = new State("relocate");
    private State attackState = new State("attack");
    private State returnState = new State("return");
    private State deadState = new State("dead");

    private Coroutine waitForAttack;
    private Coroutine waitStunned;
    private Vector2 prevPosition;
    Vector2 nextPosition;

    private void Start() {
        InitEnemy();
        nextPosition = transform.position;
        prevPosition = nextPosition;

        idleState.enterActions.Add(new FSM.Action(() => {
            waitForAttack = StartCoroutine(Wait(rng.Next((int) waitTimeMs.x, (int) waitTimeMs.y) / 1000, relocateState));
        }));
        idleState.exitActions.Add(new FSM.Action(() => {
            if (waitForAttack != null) {
                StopCoroutine(waitForAttack);
            }
        }));

        relocateState.enterActions.Add(new FSM.Action(() => {
            nextPosition = new Vector2(transform.position.x, yRange[rng.Next(0, yRange.Count)]);
        }));
        relocateState.stayActions.Add(new FSM.Action(() => {
            Move();
        }));
        relocateState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, nextPosition) < 0.1;
        }, attackState));

        attackState.enterActions.Add(new FSM.Action(() => {
            prevPosition = nextPosition;
            nextPosition = new Vector2(transform.position.x < 0 ? xRange.y : xRange.x, transform.position.y);
        }));
        attackState.stayActions.Add(new FSM.Action(() => {
            Move();
        }));
        attackState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, nextPosition) < 0.1;
        }, returnState));

        returnState.enterActions.Add(new FSM.Action(() => {
            nextPosition = prevPosition;
        }));
        returnState.stayActions.Add(new FSM.Action(() => {
            Move();
        }));
        returnState.transitions.Add(new FSM.Transition(() => {
            return Vector2.Distance(transform.position, nextPosition) < 1;
        }, idleState));

        deadState.enterActions.Add(new FSM.Action(() => {
            Die();
            LevelLoader.Instance.LoadLevel(LEVELS.VICTORY);
        }));

        stateMachine.ChangeState(idleState);
    }

    private void Update() {
        stateMachine.Tick();
    }

    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    protected override void GotHit() {
        health -= Stats.currentPower;

        if (health <= 0) {
            stateMachine.ChangeState(deadState);
        }
    }
}
