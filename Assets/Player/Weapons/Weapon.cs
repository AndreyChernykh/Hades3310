using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    protected Animator animator;
    public int power;

    public virtual void Attack() { }
    public virtual void SetAnimator(Animator animator) { }
}
