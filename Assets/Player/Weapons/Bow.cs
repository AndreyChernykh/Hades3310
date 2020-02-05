using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {
    public GameObject arrowPrefab;

    override public void Attack(float direction) {
        animator.Play("bow shoot");
        Arrow arrow = Instantiate(arrowPrefab, transform.position + new Vector3(2, 0, 0), Quaternion.identity).GetComponent<Arrow>();
        arrow.SetDirection(direction);
    }

    override public void SetAnimator(Animator animator) {
        this.animator = animator;
        animator.Play("bow walk");
    }
}
