using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    public static int cardType = 3;//scale/move/build
    public static int scaleMin = 10; //10%;
    public static int scaleMax = 200; //200%
    public static int moveMin = 0;
    public static int moveMax = 5;  //move at most 5 unit
    public static int buildMin = 1;
    public static int buildMax = 5;  //build at most 5-time platform
    void Awake()
    {
        cardList.Add(new Card(0, 150, "scale up to 150%", Resources.Load<Sprite>("0")));
        cardList.Add(new Card(1, 1, "move the platform in a range of 1", Resources.Load<Sprite>("1")));
        cardList.Add(new Card(2, 1, "build a 1-time platform", Resources.Load<Sprite>("2")));
    }

}
