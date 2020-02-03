using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioManager audioManager;
    public LayerMask wallLayer;

    public float speed = 1;
    public float dashSpeed = 10;

    bool isDash;

    private void Start()
    {
        audioManager.Play("music");
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        // {
        //     Move(new Vector3(0, speed, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        // {
        //     Move(new Vector3(0, -speed, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        // {
        //     Move(new Vector3(-speed, 0, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        // {
        //     Move(new Vector3(speed, 0, 0));
        // }

        if (Input.GetKeyDown(KeyCode.G))
        {
            isDash = true;
        }

        Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized);

    }

    Coroutine musicPaused;

    private void Move(Vector2 input)
    {
        float dist = (isDash ? dashSpeed : speed);
        TryMoveAxis(Vector2.right * input.x, dist);
        TryMoveAxis(Vector2.up * input.y, dist);


        audioManager.Play("test");

        if (musicPaused != null)
        {
            StopCoroutine(musicPaused);
        }

        //musicPaused = StartCoroutine(PauseMusic());
        isDash = false;
    }

    void TryMoveAxis(Vector2 dir, float dist)
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, dir, dist + 3, wallLayer);

        Debug.DrawRay(transform.position, dir * (dist + 3), Color.magenta);

        if (hit.collider == null)
        {
            transform.Translate(dir * dist);
        }

        Physics2D.SyncTransforms();
    }

    private IEnumerator PauseMusic()
    {
        audioManager.Pause("music");
        yield return new WaitForSeconds(0.6f);
        audioManager.Play("music");
    }
}
