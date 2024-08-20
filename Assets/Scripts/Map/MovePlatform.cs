using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool isHovering;
    private Color highlight = new Color(200f / 255f, 200f / 255f, 200f / 255f);
    private Color normal = Color.white;
    private Vector3 originPosition;
    private bool isPositioning;
    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        originPosition = transform.position;
    }

    private void Update() {
        if (isPositioning)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(0))
            {
                isPositioning = false;
                Globals.isUsing = -1;
                sprite.color = normal;
                HandCardPool.instance.displayHandCardArea(true);
            }
        }

        if (Globals.isUsing == 1 && isHovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isPositioning)
                {
                    isPositioning = true;
                }
            }
        }
    }

    private void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = new Vector3(Mathf.Clamp(mousePosition.x, originPosition.x - Globals.card_val, originPosition.x + Globals.card_val),
        Mathf.Clamp(mousePosition.y, originPosition.y - Globals.card_val, originPosition.y + Globals.card_val),
        originPosition.z
        );
        transform.position = Vector2.Lerp(transform.position, mousePosition, 2f);
    }

    private void OnMouseEnter() {
        if (Globals.isUsing == 1)
        {
            isHovering = true;
            sprite.color = highlight;
        }
    }

    private void OnMouseExit() {
        if (Globals.isUsing == 1)
        {
            isHovering = false;
            sprite.color = normal;
        }
    }
}
