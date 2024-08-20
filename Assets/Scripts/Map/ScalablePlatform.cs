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
        if (Globals.isScaling && isHovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Globals.isScaling = false;
                sprite.color = normal;
                FindObjectOfType<VisualCardsHandler>(true).gameObject.SetActive(true);
                // Scale!
            }
        }
    }

    private void OnMouseEnter() {
        if (Globals.isScaling)
        {
            isHovering = true;
            sprite.color = highlight;
        }
    }

    private void OnMouseExit() {
        if (Globals.isScaling)
        {
            isHovering = false;
            sprite.color = normal;
        }
    }
}
