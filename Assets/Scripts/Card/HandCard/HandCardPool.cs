using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HandCardPool : MonoBehaviour
{
    public List<Card> TotalCardPool = new List<Card>(); //all cards from my cards
    public List<GameObject> unusedCardPool = new List<GameObject>();////discarded deck
    public List<GameObject> handCardPool = new List<GameObject>();//hand card pool
    public List<GameObject> usedCardPool = new List<GameObject>();//used card pool

    public GameObject CardPool_Content;//discarded deck content
    public GameObject handCardArea;
    public GameObject cardPool;

    public TextMeshProUGUI unusedCardNum_Text;
    public TextMeshProUGUI usedCardNum_Text;

    public GameObject handcardObj;


    public int numPerRow = 6;
    public float x_pos = 68;
    public float x_space = 116;
    public float y_pos = -33;
    public float y_space = 180;
    public GameObject cardRow;
    public int max_handcardNum = 6; //max hand card number is 6


    public static HandCardPool instance;

    public int currentCardNum = 0;

    bool displayFlag = false;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        InitHandCardPool();
        DealCards(max_handcardNum);
    }

    public void InitHandCardPool()
    {
        TotalCardPool = MyCard.instance.MyCardPool;

        
        foreach (Card iCard in MyCard.instance.MyCardPool)
        {
            GameObject GenerateCard = Instantiate(handcardObj, cardPool.transform);
            GenerateCard.GetComponent<CardInfo>().SetCardInfo(iCard);
            GenerateCard.transform.SetParent(cardPool.transform);
            GenerateCard.transform.localPosition = new Vector3(0,0,0);

            unusedCardPool.Add(GenerateCard);
        }


        unusedCardNum_Text.text = unusedCardPool.Count.ToString();
        usedCardNum_Text.text = "0";
    }

    public void DisplayUnusedCard()
    {
        displayFlag = !displayFlag;
        if (displayFlag)
        {
            CardPool_Content.transform.parent.parent.gameObject.SetActive(true);
            clearContent();
            DisplayCards(unusedCardPool);
        }
        else
        {
            CardPool_Content.transform.parent.parent.gameObject.SetActive(false);
        }
    }

    public void DisplayUsedCard()
    {
        displayFlag = !displayFlag;
        if (displayFlag)
        {
            CardPool_Content.transform.parent.parent.gameObject.SetActive(true);
            clearContent();
            DisplayCards(usedCardPool);
        }
        else
        {
            CardPool_Content.transform.parent.parent.gameObject.SetActive(false);
        }
    }

    void clearContent()
    {
        Transform transform;
        for (int i = 0; i < CardPool_Content.transform.childCount; i++)
        {
            transform = CardPool_Content.transform.GetChild(i);
            GameObject cardObj = transform.gameObject;
            cardObj.transform.SetParent(cardPool.transform);
            cardObj.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void DisplayCards(List<GameObject> cardList)
    {
        int count = 0;
        GameObject newRow = null;

        //reshape the scroll view content height
        if (cardList.Count != 0)
            CardPool_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(CardPool_Content.GetComponent<RectTransform>().rect.width, y_space * ((MyCard.instance.MyCardPool.Count - 1) / 6 + 1));

        //generate my cards
        foreach (GameObject iCard in cardList)
        {
            if (count % numPerRow == 0)
            {
                newRow = Instantiate(cardRow, CardPool_Content.transform);
            }
            iCard.transform.SetParent(newRow.transform);
            iCard.transform.localPosition = new Vector3(x_pos + (count % numPerRow) * x_space, y_pos, 0);

            count = count + 1;
        }
    }

    public void DealCards(int cardNum)
    {
        int maxNum = cardNum;
        if (maxNum > unusedCardPool.Count) maxNum = unusedCardPool.Count;
        for (int i = 0; i < maxNum; i++)
        {
            int rd = Random.Range(0, unusedCardPool.Count);
            GameObject newCard = unusedCardPool[rd];
            newCard.transform.SetParent(handCardArea.transform);
            unusedCardPool.RemoveAt(rd);
            handCardPool.Add(newCard);
        }

        currentCardNum = maxNum;

        if (unusedCardPool.Count == 0)
        {
            foreach(GameObject c in usedCardPool)
            {
                unusedCardPool.Add(c);
            }
            usedCardPool.Clear();
        }

        unusedCardNum_Text.text = unusedCardPool.Count.ToString();
        usedCardNum_Text.text = usedCardPool.Count.ToString();
        //  GameObject newCardObj = Instantiate(cardImageArray[iCard.cardType], newRow.transform);
    }


    public void useCard(GameObject cardObj)
    {
        cardObj.transform.SetParent(cardPool.transform);
        cardObj.transform.localPosition = new Vector3(0, 0, 0);

        handCardPool.Remove(cardObj);
        usedCardPool.Add(cardObj);

        usedCardNum_Text.text = usedCardPool.Count.ToString();

        currentCardNum = currentCardNum - 1;
    }


    //discard card and draw new card
    public void discardCard(GameObject cardObj)
    {
        cardObj.transform.SetParent(cardPool.transform);
        cardObj.transform.localPosition = new Vector3(0, 0, 0);

        handCardPool.Remove(cardObj);
        foreach (GameObject c in handCardPool)
        {
            c.transform.SetParent(cardPool.transform);
            c.transform.localPosition = new Vector3(0, 0, 0);
            usedCardPool.Add(c);
        }
        handCardPool.Clear();

        DealCards(currentCardNum - 1);

    }

}
