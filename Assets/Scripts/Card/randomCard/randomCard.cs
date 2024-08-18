using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomCard : MonoBehaviour
{
    public GameObject[] cardsObj;
    Card[] cards;
    public void generateRandomCard()
    {
        this.gameObject.SetActive(true);

        cards = new Card[3];
        for (int i = 0; i<3; i++)
        {
            int card_type = Random.Range(0, CardDatabase.cardType);
            Card new_card = new Card(card_type);

            cardsObj[i].GetComponent<randomCardInfo>().SetCardInfo(new_card);
            cards[i] = new_card;
        }
    }

    public Card getCard(int index)
    {
        return cards[index];
    }

    public void DisablePanel()
    {
        this.gameObject.SetActive(false);
    }
}
