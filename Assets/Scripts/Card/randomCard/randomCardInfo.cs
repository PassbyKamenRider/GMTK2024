using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.EventSystems;

public class randomCardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int card_id = 0;
    public int cardType = 0; //0: scale; 1:move; 2:build
    public int x_value = 0; // scale to x%; move to x; create x-time platform
    public int cardLevel = 0;
    public string cardDescription = "";
    public Sprite cardSprite;

    public TextMeshProUGUI x_value_Text;
    public Image cardImage;

    [Header("Scale Parameters")]
    [SerializeField] private float scaleOnHover = 1.15f;
    [SerializeField] private float scaleOnSelect = 1.25f;
    [SerializeField] private float scaleTransition = .15f;
    [SerializeField] private Ease scaleEase = Ease.OutBack;


    public void SetCardInfo(Card card)
    {
        this.cardType = card.cardType;
        this.x_value = card.x_value;
        this.cardLevel = card.cardLevel;
        this.cardDescription = card.cardDescription;
        this.cardSprite = card.cardSprite;
        SetUI();
    }
    public void SetUI()
    {
        if (cardType == 0)
            x_value_Text.text = $"Scale the size of platform by <color=red>{x_value}</color> percents";
        else if (cardType == 1)
            x_value_Text.text = $"Move the platform in a range of <color=red>{x_value}</color>";
        else if (cardType == 2)
            x_value_Text.text = $"Create a <color=red>{x_value}</color>x platform at a designed position";
        cardImage.sprite = cardSprite;
    }


    //select this card and add to my cards
    public void AddtoMyCard()
    {
        MyCard.instance.addCard(this.transform.parent.GetComponent<randomCard>().getCard(card_id));
        this.transform.parent.gameObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(scaleOnHover, scaleTransition).SetEase(scaleEase);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }
}
