using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHover : MonoBehaviour
{
    [SerializeField] private AudioSource audio_confirm;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private int NodeType; // 0: build, 1: event, 2: upgrade
    private bool isHovering;
    private Color highlight = new Color(200f / 255f, 200f / 255f, 200f / 255f);
    private Color normal = Color.white;

    private void Update() {
        if (!Globals.isPausing && isHovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                audio_confirm.Play();
                Debug.Log("start!");
            }
        }
    }
    private void OnMouseEnter() {
        if (!Globals.isPausing)
        {
            isHovering = true;
            sprite.color = highlight;
        }
    }

    private void OnMouseExit() {
        if (!Globals.isPausing)
        {
            isHovering = false;
            sprite.color = normal;
        }
    }
}
