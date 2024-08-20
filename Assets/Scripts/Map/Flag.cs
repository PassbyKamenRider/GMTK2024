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
            Globals.StarwberryCount += collectableCount;
            Globals.isCurrentFinished = true;
            SceneManager.LoadScene("Map");
        }
    }
}
