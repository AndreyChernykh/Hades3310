using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class Enemy : MonoBehaviour, IRandom
{
    public int health = 1;
    public float speed = 1;

    public int customSeed;
    int seed;

    protected System.Random rng;
    protected StateMachine stateMachine;
    protected Player player;

    public void SetSeed(int seed)
    {
        this.seed = seed;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    protected void Init()
    {
        if (customSeed > 0)
        {
            seed = customSeed;
        }

        rng = new System.Random(seed);
        stateMachine = new StateMachine();
    }

    protected IEnumerator Wait(float seconds, State nextState)
    {
        yield return new WaitForSeconds(seconds);
        stateMachine.ChangeState(nextState);
    }
}
