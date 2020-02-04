using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

    override public void Attack() {
        animator.Play("bow attack");
    }

    override public void SetAnimator(Animator animator) {
        this.animator = animator;
        animator.Play("bow walk");
    }
}
