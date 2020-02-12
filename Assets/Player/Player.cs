using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Blink)), RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {
    [SerializeField]
    private LayerMask wallLayer;

    [Header("Weapon prefabs")]
    [SerializeField]
    private GameObject bow;
    [SerializeField]
    private GameObject sword;

    private Weapon currentWeapon;

    [Header("Movement")]
    [SerializeField]
    private float speed = 1;

    [Header("Attack")]
    [SerializeField]
    private float attackTimeout = 0.5f;
    [SerializeField]
    private float timeBeforeCanAttack;

    private bool isFacingRight = false;

    private Animator animator;
    private Blink blink;

    private void Awake() {
        blink = GetComponent<Blink>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        SetSelectedWeapon();
    }

    private void SetSelectedWeapon() {
        if (Stats.selectedWeapon == Weapons.SWORD) {
            bow.SetActive(false);
            currentWeapon = sword.GetComponent<Sword>();
        }
        else {
            sword.SetActive(false);
            currentWeapon = bow.GetComponent<Bow>();
        }

        currentWeapon.SetAnimator(animator);
    }

    private void Update() {
        timeBeforeCanAttack -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1")) {
            Attack();
        }

        float xInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(xInput) > 0.1) {
            isFacingRight = xInput > 0;
        }

        transform.localScale = new Vector3(isFacingRight ? -1 : 1, 1, 1);
        Move(new Vector2(xInput, Input.GetAxisRaw("Vertical")).normalized);
    }

    private void Move(Vector2 input) {
        TryMove(Vector2.right * input.x);
        TryMove(Vector2.up * input.y);
    }

    private void TryMove(Vector2 direction) {
        float moveDistance = speed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position, direction, moveDistance, wallLayer);

        if (hit.collider == null) {
            transform.Translate(direction * moveDistance);
        }
    }

    private void Attack() {
        if (timeBeforeCanAttack <= 0) {
            timeBeforeCanAttack = attackTimeout;
            currentWeapon.Attack(transform.localScale.x * -1);
            AudioManager.Instance.Play("test");
        }
    }

    public void GotHitBy(Enemy enemy) {
        Stats.CurrentHealth -= enemy.damage;
        blink.StartBlinking();

        if (Stats.CurrentHealth <= 0) {
            LevelLoader.Instance.LoadLevel(LEVELS.GAME_OVER);
            gameObject.SetActive(false);
        }
    }
}
