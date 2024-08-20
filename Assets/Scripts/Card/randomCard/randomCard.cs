using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class randomCard : MonoBehaviour
{
    public GameObject[] cardsObj;
    Card[] cards;
    private bool hasGenerated;

    private void Start() {
        generateRandomCard(); 
    }

    public void generateRandomCard()
    {
        if (hasGenerated)
        {
            if (Globals.StarwberryCount == 0)
            {
                return;
            }
            else
            {
                Globals.StarwberryCount -= 1;
            }
        }
        this.gameObject.SetActive(true);

        cards = new Card[3];
        for (int i = 0; i<3; i++)
        {
            int card_type = Random.Range(0, CardDatabase.cardType);
            Card new_card = new Card(card_type);
            Debug.Log(new_card.cardSprite.name);
            cardsObj[i].GetComponent<randomCardInfo>().SetCardInfo(new_card);
            cards[i] = new_card;
        }

        hasGenerated = true;
    }

    public Card getCard(int index)
    {
        return cards[index];
    }

    public void DisablePanel()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
    }
}
