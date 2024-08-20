using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] HandCardPool handCardPool;
    public int collectableCount;

    private void Start() {
        handCardPool.startGame();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Time.timeScale = 0f;
            HandCardPool.instance.gameObject.SetActive(false);
            Globals.StarwberryCount += collectableCount;
            Globals.isCurrentFinished = true;
            FindObjectOfType<randomCard>(true).gameObject.SetActive(true);
        }
    }
}
