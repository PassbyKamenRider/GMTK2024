using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomCard : MonoBehaviour
{
    public GameObject[] cardsObj;
    Card[] cards = new Card[3];
    public void generateRandomCard()
    {
        this.gameObject.SetActive(true);
        for(int i = 0; i<3; i++)
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
