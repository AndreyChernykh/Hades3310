using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Animator animator;
    public AudioManager audioManager;
    public LayerMask wallLayer;
    public Blink blink;

    public GameObject bow;
    public GameObject sword;

    [HideInInspector]
    public Weapon currentWeapon;

    public float speed = 1;
    public float dashSpeed = 10;

    bool isDash;
    bool isFacingRight = false;

    private void Start() {
        audioManager.Play("music");
        SetSelectedWeapon();
    }

    void SetSelectedWeapon() {
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

    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            isDash = true;
        }

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

    Coroutine musicPaused;

    private void Attack() {
        currentWeapon.Attack();
    }

    private void Move(Vector2 input) {
        float dist = (isDash ? dashSpeed : speed);
        TryMoveAxis(Vector2.right * input.x, dist);
        TryMoveAxis(Vector2.up * input.y, dist);

        audioManager.Play("test");

        if (musicPaused != null) {
            StopCoroutine(musicPaused);
        }

        //musicPaused = StartCoroutine(PauseMusic());
        isDash = false;
    }

    void TryMoveAxis(Vector2 dir, float dist) {
        RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position, dir, dist + 3, wallLayer);

        Debug.DrawRay(transform.position, dir * (dist + 3), Color.magenta);

        if (hit.collider == null) {
            transform.Translate(dir * dist);
        }

        Physics2D.SyncTransforms();
    }

    private IEnumerator PauseMusic() {
        audioManager.Pause("music");
        yield return new WaitForSeconds(0.6f);
        audioManager.Play("music");
    }

    public void Damage(Enemy enemy) {
        Debug.Log("onTrigget");
        Stats.currentHealth -= enemy.damage;
        blink.StartBlinking();

        if (Stats.currentHealth <= 0) {
            LevelLoader.Instance.LoadLevel(LEVELS.GAME_OVER);
            gameObject.SetActive(false);
        }
    }
}
