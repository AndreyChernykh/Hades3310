using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

[RequireComponent(typeof(Blink)), RequireComponent(typeof(Hit))]
abstract public class Enemy : MonoBehaviour, IRandom {
    public event Action<Enemy> OnDie = delegate { };

    [SerializeField]
    public int damage = 1;
    [SerializeField]
    public int health = 1;
    [SerializeField]
    public float speed = 1;

    private int seed;

    private Blink blink;
    private Hit hit;

    protected System.Random rng;
    protected StateMachine stateMachine;
    protected Player player;

    private void Awake() {
        blink = GetComponent<Blink>();
        hit = GetComponent<Hit>();
    }

    public void SetSeed(int seed) {
        this.seed = seed;
    }

    public void SetPlayer(Player player) {
        this.player = player;
    }

    protected void InitEnemy() {
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

    protected virtual void GotHit() { }

    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null) {
            player.GotHitBy(this);
        }
    }

    public void Hit() {
        blink.StartBlinking();
        hit.EmitParticles();
        GotHit();
    }
}
