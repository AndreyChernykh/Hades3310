using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    protected Animator animator;

    public virtual void Attack(float direction) { }
    public virtual void SetAnimator(Animator animator) { }
}
