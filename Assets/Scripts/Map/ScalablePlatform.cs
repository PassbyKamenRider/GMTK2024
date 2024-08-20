using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalablePlatform : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool isHovering;
    private Color highlight = new Color(200f / 255f, 200f / 255f, 200f / 255f);
    private Color normal = Color.white;
    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (Globals.isUsing == 0 && isHovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Globals.isUsing = -1;
                sprite.color = normal;
                transform.localScale = new Vector3((1+Globals.card_val) * transform.localScale.x, (1+Globals.card_val) * transform.localScale.y, transform.localScale.z);
                HandCardPool.instance.displayHandCardArea(true);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Globals.isUsing = -1;
                sprite.color = normal;
                transform.localScale = new Vector3((1-Globals.card_val) * transform.localScale.x, (1-Globals.card_val) * transform.localScale.y, transform.localScale.z);
                HandCardPool.instance.displayHandCardArea(true);
            }
        }
    }

    private void OnMouseEnter() {
        if (Globals.isUsing == 0)
        {
            isHovering = true;
            sprite.color = highlight;
        }
    }

    private void OnMouseExit() {
        if (Globals.isUsing == 0)
        {
            isHovering = false;
            sprite.color = normal;
        }
    }
}
