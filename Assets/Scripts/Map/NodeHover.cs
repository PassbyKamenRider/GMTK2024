using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeHover : MonoBehaviour
{
    [SerializeField] private AudioSource audio_confirm;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private int NodeType; // 0: build, 1: upgrade
    [SerializeField] private PoI nodeInfo;
    [SerializeField] public GameObject Ring;
    private bool isHovering;
    private Color highlight = new Color(200f / 255f, 200f / 255f, 200f / 255f);
    private Color normal = Color.white;

    private void Update() {
        if (!Globals.isPausing && isHovering)
        {
            if (Input.GetMouseButtonDown(0) && NodeType == 0)
            {
                if ((Globals.isCurrentFinished && ContainsNode(Globals.nextPoIs))) // if starting node or choose next node
                {
                    // Update current progress
                    Globals.isCurrentFinished = false;
                    Globals.path.Add(new Vector2(nodeInfo.y, nodeInfo.x));
                    Globals.nextPoIs = new List<Vector2>(nodeInfo.nextPoIs);
                    Globals.currentY = nodeInfo.y;
                    Globals.currentX = nodeInfo.x;

                    // Start Stage
                    audio_confirm.Play();
                    SceneManager.LoadScene("Stage1");
                }
                else if (!Globals.isCurrentFinished && Globals.currentY == nodeInfo.y && Globals.currentX == nodeInfo.x) // if current node isn't finished)
                {
                    // Start Stage
                    audio_confirm.Play();
                    SceneManager.LoadScene("Stage1");
                }
            }

            if (Input.GetMouseButtonDown(0) && NodeType == 1)
            {
                if ((Globals.isCurrentFinished && ContainsNode(Globals.nextPoIs))) // if starting node or choose next node
                {
                    // Update current progress
                    Globals.isCurrentFinished = true;
                    Globals.path.Add(new Vector2(nodeInfo.y, nodeInfo.x));
                    Globals.nextPoIs = new List<Vector2>(nodeInfo.nextPoIs);
                    Globals.currentY = nodeInfo.y;
                    Globals.currentX = nodeInfo.x;

                    // Open upgrade
                    Ring.SetActive(true);
                    audio_confirm.Play();
                    FindObjectOfType<UIcontroller>().ShowUpgradeMenu();
                }
                else if (!Globals.isCurrentFinished && Globals.currentY == nodeInfo.y && Globals.currentX == nodeInfo.x) // if current node isn't finished)
                {
                    // Open upgrade
                    Ring.SetActive(true);
                    audio_confirm.Play();
                    FindObjectOfType<UIcontroller>().ShowUpgradeMenu();
                }
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

    private bool ContainsNode(List<Vector2> l)
    {
        foreach (Vector2 v in l)
        {
            if (v.x == nodeInfo.x && v.y == nodeInfo.y)
            {
                return true;
            }
        }
        return false;
    }
}
