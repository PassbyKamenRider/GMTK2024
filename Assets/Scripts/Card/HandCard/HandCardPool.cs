using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class HandCardPool : MonoBehaviour
{
    public List<Card> TotalCardPool = new List<Card>(); //all cards from my cards
    /* public List<GameObject> unusedCardPool = new List<GameObject>();////discarded deck
     public List<GameObject> handCardPool = new List<GameObject>();//hand card pool
     public List<GameObject> usedCardPool = new List<GameObject>();//used card pool*/
    public List<GameObject> CardPoolObj = new List<GameObject>();
    public List<int> unusedCardPool = new List<int>();//discarded deck
    public List<int> handCardPool = new List<int>();//hand card pool
    public List<int> usedCardPool = new List<int>();//used card pool*/


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
        int cardCount = 0;
        TotalCardPool = MyCard.instance.MyCardPool;

        
        foreach (Card iCard in MyCard.instance.MyCardPool)
        {
            GameObject GenerateCard = HorizontalCardHolder.instance.GenerateCard(iCard, cardCount);

            CardPoolObj.Add(GenerateCard);
            unusedCardPool.Add(cardCount);
            cardCount = cardCount + 1;
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
            GameObject.Destroy(transform.gameObject);
        }
    }

    public void DisplayCards(List<int> cardList)
    {
        int count = 0;
        GameObject newRow = null;

        //reshape the scroll view content height
        if (cardList.Count != 0)
            CardPool_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(CardPool_Content.GetComponent<RectTransform>().rect.width, y_space * ((MyCard.instance.MyCardPool.Count - 1) / 6 + 1));

        //generate my cards
        foreach (int index in cardList)
        {
            if (count % numPerRow == 0)
            {
                newRow = Instantiate(cardRow, CardPool_Content.transform);
            }
            GameObject GenerateCard = Instantiate(handcardObj, newRow.transform);
            GenerateCard.GetComponent<CardInfo>().SetCardInfo(TotalCardPool[index]);
            GenerateCard.transform.localPosition = new Vector3(x_pos + (count % numPerRow) * x_space, y_pos, 0);

            count = count + 1;
        }
    }

    public void DealCards(int cardNum)
    {
        int maxNum = cardNum;
        if (maxNum > unusedCardPool.Count)
        {
            foreach (int index in usedCardPool)
            {
                unusedCardPool.Add(index);
            }
            usedCardPool.Clear();
        }
        for (int i = 0; i < maxNum; i++)
        {
            int rd = Random.Range(0, unusedCardPool.Count);
            int index = unusedCardPool[rd];
            GameObject newCard = CardPoolObj[index];
            newCard.transform.Find("HandCard").GetComponent<DragHandler>().enableCard(true);
            unusedCardPool.RemoveAt(rd);
            handCardPool.Add(index);
        }

        currentCardNum = maxNum;

        if (unusedCardPool.Count == 0)
        {
            foreach(int index in usedCardPool)
            {
                unusedCardPool.Add(index);
            }
            usedCardPool.Clear();
        }

        unusedCardNum_Text.text = unusedCardPool.Count.ToString();
        usedCardNum_Text.text = usedCardPool.Count.ToString();
        //  GameObject newCardObj = Instantiate(cardImageArray[iCard.cardType], newRow.transform);
    }


    public void useCard(int card_id)
    {
        Transform card = CardPoolObj[card_id].transform.Find("HandCard");
        card.GetComponent<DragHandler>().enableCard(false);
        card.transform.position = new Vector3(0, 0, 0);
        card.GetComponent<DragHandler>().cardVisual.transform.position = new Vector3(0, 0, 0);

        // Get card type and number for use
        CardInfo i = card.GetComponent<DragHandler>().cardVisual.transform.GetComponent<CardInfo>();
        int usedCardType = i.cardType;
        float usedCardVal = i.x_value;
        switch(usedCardType)
        {
            case 0:
            Globals.card_val = usedCardVal * 0.01f;
            Globals.isUsing = 0;
            break;

            case 1:
            Globals.card_val = usedCardVal;
            Globals.isUsing = 1;
            break;

            case 2:
            Globals.card_val = usedCardVal;
            Globals.isUsing = 2;
            break;

            default:
            break;
        }

        handCardPool.Remove(card_id);
        usedCardPool.Add(card_id);

        usedCardNum_Text.text = usedCardPool.Count.ToString();

        currentCardNum = currentCardNum - 1;
    }


    //discard card and draw new card
    public void discardCard(int card_id)
    {
        CardPoolObj[card_id].GetComponentInChildren<DragHandler>().enableCard(false);
        handCardPool.Remove(card_id);

        foreach (int index in handCardPool)
        {
            CardPoolObj[index].GetComponentInChildren<DragHandler>().enableCard(false);
            usedCardPool.Add(index);
        }
        handCardPool.Clear();

        DealCards(currentCardNum - 1);

    }


    public void displayHandCardArea(bool flag)
    {
        if (flag)
        {
            transform.DOLocalMoveY(-9, 0.15f);
        }
        else
        {
            transform.DOLocalMoveY(-154, 0.15f);
        }
    }
}
