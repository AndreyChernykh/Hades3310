using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouAreHere : MonoBehaviour {
    public float transitionTime = 5;
    public float blinkTime = 0.5f;
    public Color color1;
    public Color color2;

    List<Vector2> points = new List<Vector2>();

    float timeBeforeNextBlink;
    SpriteRenderer sprite;

    private void Start() {
        points.Add(new Vector2(25.1f, -20.9f));
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

        sprite = GetComponent<SpriteRenderer>();
        timeBeforeNextBlink = blinkTime;
        MoveToNextPoint();

        Vector2 initialPoint = points[Stats.currentRoom];
        transform.position = new Vector3(initialPoint.x, initialPoint.y, 0);
    }

    void Update() {
        if (timeBeforeNextBlink < 0) {
            sprite.color = sprite.color == color1 ? color2 : color1;
            timeBeforeNextBlink = blinkTime;
        }

        timeBeforeNextBlink -= Time.deltaTime;
    }

    [ContextMenu("MoveToNextPoint")]
    public void MoveToNextPoint() {
        LeanTween.move(gameObject, points[Stats.currentRoom + 1], transitionTime).setOnComplete(() => {
            Stats.currentRoom += 1;
            LevelLoader.Instance.LoadNextLevel();
        });
    }
}
