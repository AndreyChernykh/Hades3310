using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YouAreHere : MonoBehaviour {
    [SerializeField]
    private float blinkTime = 0.5f;
    [SerializeField]
    private Color color1;
    [SerializeField]
    private Color color2;

    [SerializeField]
    private float speed = 7;
    private List<Vector2> points = new List<Vector2>();

    private float timeBeforeNextBlink;
    private SpriteRenderer sprite;

    private Vector2 targetPoint;
    private bool isArrived = false;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();

        timeBeforeNextBlink = blinkTime;

        SetupPoints();
    }

    private void SetupPoints() {
        // Nothing ever easy with Nokia 3310
        points.Add(new Vector2(25, -20.6f));
        points.Add(new Vector2(2.2f, -20.6f));
        points.Add(new Vector2(-9, -20.6f));
        points.Add(new Vector2(-19.6f, -20.6f));
        points.Add(new Vector2(-32.7f, -20.6f));
        points.Add(new Vector2(-32.7f, -13.7f));
        points.Add(new Vector2(-23.6f, -13.7f));
        points.Add(new Vector2(-13.6f, -13.7f));
        points.Add(new Vector2(-4.7f, -13.7f));
        points.Add(new Vector2(4.4f, -13.7f));
        points.Add(new Vector2(15.4f, -13.7f));
        points.Add(new Vector2(15.4f, -7));
        points.Add(new Vector2(4.2f, -7));
        points.Add(new Vector2(4.2f, -0.6f));
        points.Add(new Vector2(4.2f, 4.5f));

        Vector2 initialPoint = points[Stats.currentRoom];
        transform.position = new Vector3(initialPoint.x, initialPoint.y, 0);
        targetPoint = points[Stats.currentRoom + 1];
    }

    private void Update() {
        Blink();
        MoveToNextPoint();
    }

    private void Blink() {
        if (timeBeforeNextBlink < 0) {
            sprite.color = sprite.color == color1 ? color2 : color1;
            timeBeforeNextBlink = blinkTime;
        }

        timeBeforeNextBlink -= Time.deltaTime;
    }

    public void MoveToNextPoint() {
        if (isArrived) {
            return;
        }

        if (Vector2.Distance(transform.position, targetPoint) < 0.1f) {
            isArrived = true;
            Stats.currentRoom += 1;
            LevelLoader.Instance.LoadNextLevel();
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        }
    }
}
