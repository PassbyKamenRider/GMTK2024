using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card
{
    public int cardType = 0; //0: scale; 1:move; 2:build
    public int x_value = 0; // scale to x%; move to x; create x-time platform
    public string cardDescription = "";
    public Sprite cardSprite;

    public Card(int cardType)
    {
        if (cardType == 0)//scale
        {
            x_value = Random.Range(CardDatabase.scaleMin, CardDatabase.scaleMax+1);
        }
        else if (cardType == 1)
        {
            x_value = Random.Range(CardDatabase.moveMin, CardDatabase.moveMax +1);
        }
        else if (cardType == 2)
        {
            x_value = Random.Range(CardDatabase.buildMin, CardDatabase.buildMax+ 1);
        }

        cardSprite = Resources.Load<Sprite>(cardType.ToString());
    }
    public Card(int cardType, int x_value)
    {
        this.cardType = cardType;
        this.x_value = x_value;
        cardSprite = Resources.Load<Sprite>(cardType.ToString());
    }

    public Card(int cardType, int x_value, string desription)
    {
        this.cardType = cardType;
        this.x_value = x_value;
        this.cardDescription = desription;
        cardSprite = Resources.Load<Sprite>(cardType.ToString());
    }

    public Card(int cardType, int x_value, string desription, Sprite sprite)
    {
        this.cardType = cardType;
        this.x_value = x_value;
        this.cardDescription = desription;
        this.cardSprite = sprite;
    }



}
