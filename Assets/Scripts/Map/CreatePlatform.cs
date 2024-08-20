using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatform : MonoBehaviour
{
    [SerializeField] Sprite[] buildPlatformSprites;
    public int duration = -1;
    private bool canMove = true;
    private SpriteRenderer sprite;

    private void Start() {
        Time.timeScale = 0f;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (canMove)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                canMove = false;
                Globals.isUsing = -1;
                HandCardPool.instance.displayHandCardArea(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Player")
        {
            ReduceDuration();
        }
    }

    private void ReduceDuration()
    {
        duration -= 1;
        if (duration == 0)
        {
            gameObject.SetActive(false);
        }
        else if (duration == 1)
        {
            // change sprite!
            sprite.sprite = buildPlatformSprites[2];
        }
        else
        {
            // change sprite!
            sprite.sprite = buildPlatformSprites[1];
        }
    }

    private void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 2f);
    }
}
