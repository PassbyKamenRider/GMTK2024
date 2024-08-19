using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Bounds MapBounds;
    public static int StarwberryCount;
    public static bool isPausing;
    public static int volume = 10;
    public static List<Vector2> nextPoIs = new List<Vector2>();
    public static int currentY;
    public static int currentX;
    public static bool isCurrentFinished = true;
    public static List<Vector2> path = new List<Vector2>();
    public static int seed = -1;
    public static bool isGenerated;
}
