using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    override public void Attack(float direction) {
        Physics2D.SyncTransforms();
        RaycastHit2D[] hits = BoxCastAll(gameObject.transform.position + new Vector3(-4 * direction, 0, 0), new Vector2(10, 8), 0, Vector2.right * direction, 1);

        foreach (RaycastHit2D hit in hits) {
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.Hit();
            }
        }

        animator.Play("sword attack");
    }

    override public void SetAnimator(Animator animator) {
        this.animator = animator;
        animator.Play("sword walk");
    }

    static public RaycastHit2D[] BoxCastAll(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance) {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origen, size, angle, direction, distance);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;

        //Drawing the cast
        Color castColor = Color.magenta;
        Debug.DrawLine(p1, p2, castColor, 1);
        Debug.DrawLine(p2, p3, castColor, 1);
        Debug.DrawLine(p3, p4, castColor, 1);
        Debug.DrawLine(p4, p1, castColor, 1);

        Debug.DrawLine(p5, p6, castColor, 1);
        Debug.DrawLine(p6, p7, castColor, 1);
        Debug.DrawLine(p7, p8, castColor, 1);
        Debug.DrawLine(p8, p5, castColor, 1);

        Debug.DrawLine(p1, p5, Color.grey, 1);
        Debug.DrawLine(p2, p6, Color.grey, 1);
        Debug.DrawLine(p3, p7, Color.grey, 1);
        Debug.DrawLine(p4, p8, Color.grey, 1);

        if (hits.Length > 0) {
            Debug.DrawLine(hits[0].point, hits[0].point + hits[0].normal.normalized * 0.2f, Color.red, 1);
        }

        return hits;
    }
}
