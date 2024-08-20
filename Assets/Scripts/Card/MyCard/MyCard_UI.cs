using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCard_UI : MonoBehaviour
{
    public GameObject deckBackground;
    public GameObject CardObj;
    public GameObject[] cardImageArray; //three card types
    public static MyCard_UI instance;
    public int numPerRow = 6;
    public float x_pos = 68;
    public float x_space = 116;
    public float y_pos = -33;
    public float y_space = 180;
    public GameObject myCardPoolContent;
    public GameObject cardRow;

    public bool displayCardFlag = false;
    private void Awake()
    {
        instance = this;
    }

    public void displayMyCard()
    {
        if (displayCardFlag)
        {
            //disable card scroll view
            myCardPoolContent.transform.parent.parent.gameObject.SetActive(false);
            deckBackground.SetActive(false);
        }
        else
        {
            //enable card scroll view
            myCardPoolContent.transform.parent.parent.gameObject.SetActive(true);
            deckBackground.SetActive(true);
            clearContent();
            GenerateCard();
        }
        displayCardFlag = !displayCardFlag;
    }
    void clearContent()
    {
        Transform transform;
        for (int i =0; i < myCardPoolContent.transform.childCount; i ++)
        {
            transform = myCardPoolContent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }
    void GenerateCard()
    {
        int count = 0;
        GameObject newRow = null;

        //reshape the scroll view content height
        if (MyCard.instance.MyCardPool.Count != 0)
            myCardPoolContent.GetComponent<RectTransform>().sizeDelta = new Vector2(myCardPoolContent.GetComponent<RectTransform>().rect.width, y_space * ((MyCard.instance.MyCardPool.Count - 1) / 6 + 1));
        
        //generate my cards
        foreach (Card iCard in MyCard.instance.MyCardPool)
        {
            if (count % numPerRow == 0)
            {
                newRow = Instantiate(cardRow, myCardPoolContent.transform);
            }
            GameObject GenerateCard = Instantiate(CardObj, newRow.transform);
            GenerateCard.GetComponent<CardInfo>().SetCardInfo(iCard);
            GenerateCard.transform.localPosition = new Vector3(x_pos + (count % numPerRow) * x_space, y_pos, 0);

            count = count + 1;
        }
    }
}
