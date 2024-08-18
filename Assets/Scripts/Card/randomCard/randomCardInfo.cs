using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class randomCardInfo : MonoBehaviour
{
    public int card_id = 0;
    public int cardType = 0; //0: scale; 1:move; 2:build
    public int x_value = 0; // scale to x%; move to x; create x-time platform
    public string cardDescription = "";
    public Sprite cardSprite;

    public TextMeshProUGUI x_value_Text;
    public Image cardImage;

    public void SetCardInfo(Card card)
    {
        this.cardType = card.cardType;
        this.x_value = card.x_value;
        this.cardDescription = card.cardDescription;
        this.cardSprite = card.cardSprite;
        SetUI();
    }
    public void SetUI()
    {
        x_value_Text.text = x_value.ToString();
        cardImage.sprite = cardSprite;
    }


    //select this card and add to my cards
    public void AddtoMyCard()
    {
        MyCard.instance.addCard(this.transform.parent.GetComponent<randomCard>().getCard(card_id));
        this.transform.parent.gameObject.SetActive(false);
    }
}
