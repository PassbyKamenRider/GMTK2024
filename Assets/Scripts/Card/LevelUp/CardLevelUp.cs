using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardLevelUp : MonoBehaviour
{
    int s_addedValue = 1; //scale added value
    int m_addedValue = 1; //move added value
    int b_addedValue = 1; //build added value

    public int numPerRow = 6;
    public float x_pos = 68;
    public float x_space = 116;
    public float y_pos = -33;
    public float y_space = 180;
    public GameObject myCardPoolContent;
    public GameObject cardRow;
    public GameObject cardToLevelUp;

    public GameObject SelectedCard;
    int selected_cardID = 0;
    int new_xValue = 0;

    public static CardLevelUp instance;

    private void Awake()
    {
        instance = this;
    }


    public void DisplayLevelCard(int id, int card_type)
    {
        selected_cardID = id;

        SelectedCard.transform.Find("Before_x").GetComponent<TextMeshProUGUI>().text = MyCard.instance.MyCardPool[id].x_value.ToString();

        if (card_type == 0)//scale card
        {
            new_xValue = MyCard.instance.MyCardPool[id].x_value + s_addedValue;
        }
        else if (card_type == 1)//move card
        {
            new_xValue = MyCard.instance.MyCardPool[id].x_value + m_addedValue;
        }
        else if (card_type == 2)//build card
        {
            new_xValue =  MyCard.instance.MyCardPool[id].x_value + b_addedValue;
        }

        SelectedCard.transform.Find("After_x").GetComponent<TextMeshProUGUI>().text = new_xValue.ToString();
        SelectedCard.GetComponent<Image>().sprite = MyCard.instance.MyCardPool[id].cardSprite;
    }

    public void ComfirmLevelUp()
    {
        MyCard.instance.MyCardPool[selected_cardID].x_value = new_xValue;
        this.gameObject.SetActive(false);
    }

    public void OpenLevelUpPanel()
    {
        DisplayLevelCard(0, MyCard.instance.MyCardPool[0].cardType);
        this.gameObject.SetActive(true);
    }

    public void CloseLevelUpPanel()
    {
        this.gameObject.SetActive(false);
    }

    void clearContent()
    {
        Transform transform;
        for (int i = 0; i < myCardPoolContent.transform.childCount; i++)
        {
            transform = myCardPoolContent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }

    public void displayCard(bool flag)
    {
        if (flag)
        {
            clearContent();
            myCardPoolContent.transform.parent.parent.gameObject.SetActive(true);
            GenerateCard();
        }
        else
        {
            myCardPoolContent.transform.parent.parent.gameObject.SetActive(false);
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
            GameObject GenerateCard = Instantiate(cardToLevelUp, newRow.transform);
            GenerateCard.GetComponent<LevelCardInfo>().id = count;
            GenerateCard.GetComponent<LevelCardInfo>().SetCardInfo(iCard);
            GenerateCard.transform.localPosition = new Vector3(x_pos + (count % numPerRow) * x_space, y_pos, 0);

            count = count + 1;
        }
    }
}
