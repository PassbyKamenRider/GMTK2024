using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public int collectableCount;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Globals.StarwberryCount += collectableCount;
            Globals.isCurrentFinished = true;
            SceneManager.LoadScene("Map");
        }
    }
}
