using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

abstract public class Enemy : MonoBehaviour, IRandom {
    public event Action<Enemy> OnDie = delegate { };

    public int damage = 1;
    public int health = 1;
    public float speed = 1;

    public int customSeed;
    int seed;

    public Blink blink;
    public Hit hit;

    protected System.Random rng;
    protected StateMachine stateMachine;
    protected Player player;

    public void SetSeed(int seed) {
        this.seed = seed;
    }

    public void SetPlayer(Player player) {
        this.player = player;
    }

    protected void Init() {
        if (customSeed > 0) {
            seed = customSeed;
        }

        rng = new System.Random(seed);
        stateMachine = new StateMachine();
    }

    protected IEnumerator Wait(float seconds, State nextState) {
        yield return new WaitForSeconds(seconds);
        stateMachine.ChangeState(nextState);
    }

    protected void Die() {
        OnDie(this);
        gameObject.SetActive(false);
        Destroy(gameObject, 1);
        Stats.money += 1;
    }

    protected virtual void GotHit(Weapon weapon) { }

    private void OnTriggerEnter2D(Collider2D other) {
        Weapon weapon = other.gameObject.GetComponent<Weapon>();
        Player player = other.gameObject.GetComponent<Player>();
        if (weapon != null) {
            blink.StartBlinking();
            hit.EmitParticles();
            GotHit(weapon);
        }
        else if (player != null) {
            player.Damage(this);
        }
    }
}
