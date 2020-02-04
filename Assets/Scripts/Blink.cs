using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {
    SpriteRenderer sprite;
    public Color color1;
    public Color color2;
    Color initialColor;

    private void Awake() {
        color1 = new Color32(67, 82, 61, 255);
        color2 = new Color32(199, 240, 216, 255);

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = color1;
        initialColor = color1;
    }

    public void StartBlinking() {
        StartCoroutine(DoBlink());
    }

    IEnumerator DoBlink() {
        for (int i = 0; i < 5; i++) {
            yield return new WaitForSeconds(0.1f);
            sprite.color = sprite.color == color1 ? color2 : color1;
        }
        sprite.color = initialColor;
    }
}
