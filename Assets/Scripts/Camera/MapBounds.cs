using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    private void Awake() {
        Globals.MapBounds = GetComponent<SpriteRenderer>().bounds;
    }
}
