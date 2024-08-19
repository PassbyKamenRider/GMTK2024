using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    [SerializeField] private AudioSource pickupAudio;
    private Flag flag;

    private void Start() {
        flag = FindObjectOfType<Flag>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            flag.collectableCount += 1;
            pickupAudio.Play();
        }
    }
}
