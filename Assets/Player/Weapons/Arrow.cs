using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    float direction;
    public float speed;

    public void SetDirection(float direction) {
        this.direction = direction;
    }

    void Update() {
        transform.Translate(new Vector3(direction * speed * Time.deltaTime, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.Hit();
        }

        Destroy(gameObject);
    }
}
