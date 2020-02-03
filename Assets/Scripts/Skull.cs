using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class Skull : MonoBehaviour, IRandom
{
    public int health = 1;
    public float speed = 1;

    public int customSeed;
    int seed;

    public Vector2 xRange;
    public Vector2 yRange;
    public Vector2 waitTimeMs;

    private System.Random rng;

    StateMachine stateMachine;
    State idleState = new State("idle");
    State walkState = new State("walk");
    State attackState = new State("attack");
    State hurtState = new State("hurt");
    State deadState = new State("dead");

    public void SetSeed(int seed)
    {
        this.seed = seed;
    }

    Coroutine waitForAttack;
    bool isHorizontalMove = false;

    private void Start()
    {
        if (customSeed > 0)
        {
            seed = customSeed;
        }

        rng = new System.Random(seed);
        stateMachine = new StateMachine();

        Vector2 nextPosition = transform.position;

        idleState.enterActions.Add(new FSM.Action(() =>
        {
            waitForAttack = StartCoroutine(Wait(rng.Next((int)waitTimeMs.x, (int)waitTimeMs.y) / 1000, attackState));
        }));
        idleState.exitActions.Add(new FSM.Action(() =>
        {
            if (waitForAttack != null)
            {
                StopCoroutine(waitForAttack);
            }
        }));

        attackState.enterActions.Add(new FSM.Action(() =>
        {
            if (isHorizontalMove)
            {
                nextPosition = new Vector2(transform.position.x < 0 ? xRange.y : xRange.x, transform.position.y);
            }
            else
            {
                nextPosition = new Vector2(transform.position.x, (float)rng.Next((int)yRange.x, (int)yRange.y));
            }
            isHorizontalMove = !isHorizontalMove;
        }));

        attackState.stayActions.Add(new FSM.Action(() =>
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed);
        }));

        attackState.transitions.Add(new FSM.Transition(() =>
        {
            return Vector2.Distance(transform.position, nextPosition) < 1;
        }, idleState));

        stateMachine.ChangeState(idleState);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    IEnumerator Wait(float seconds, State nextState)
    {
        yield return new WaitForSeconds(seconds);
        stateMachine.ChangeState(nextState);
    }
}
