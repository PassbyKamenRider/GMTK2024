using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyCard : MonoBehaviour
{
    public List<Card> MyCardPool = new List<Card>();
    public Card SelectedCard;

    public static MyCard instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <10; i++)
        {
            int card_number = Random.Range(0, CardDatabase.cardList.Count);
            MyCardPool.Add(CardDatabase.cardList[card_number]);
        }
    }

    public void addCard(Card newCard)
    {
        MyCardPool.Add(newCard);
    }

    public void deleteCard(Card cardToDelete)
    {
        MyCardPool.Remove(cardToDelete);
    }

}
