using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        cardList.Add(new Card(0, 150, "scale up to 150%", Resources.Load<Sprite>("0")));
        cardList.Add(new Card(1, 1, "move the platform in a range of 1", Resources.Load<Sprite>("1")));
        cardList.Add(new Card(2, 1, "build a 1-time platform", Resources.Load<Sprite>("2")));
    }

}
