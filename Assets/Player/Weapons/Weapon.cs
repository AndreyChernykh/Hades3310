using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public float attackTimeout = 0.5f;

    protected Animator animator;

    public virtual void Attack(float direction) { }
    public virtual void SetAnimator(Animator animator) { }
}
