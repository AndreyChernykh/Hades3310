using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    public BoxCollider2D boxCollider2D;

    void Start() {
        boxCollider2D.enabled = false;
    }

    override public void Attack() {
        boxCollider2D.enabled = true;
        animator.Play("sword attack");
        StartCoroutine(WaitAfterAttack());
    }

    override public void SetAnimator(Animator animator) {
        this.animator = animator;
        animator.Play("sword walk");
    }

    IEnumerator WaitAfterAttack() {
        yield return new WaitForSeconds(0.1f);
        boxCollider2D.enabled = false;
    }
}
