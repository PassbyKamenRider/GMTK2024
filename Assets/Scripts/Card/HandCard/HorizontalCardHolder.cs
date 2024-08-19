using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class HorizontalCardHolder : MonoBehaviour
{
    [SerializeField] private DragHandler selectedCard;
    [SerializeReference] private DragHandler hoveredCard;

    [SerializeField] private GameObject slotPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 6;
    public List<DragHandler> cards;

    bool isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;
    
    public static HorizontalCardHolder instance;
    private void Awake()
    {
        instance = this;
    }


    public GameObject GenerateCard(Card info, int card_id)
    {
        GameObject new_card = Instantiate(slotPrefab, transform);

        DragHandler dragHandler = new_card.GetComponentInChildren<DragHandler>();
        dragHandler.card_id = card_id;
        dragHandler.PointerEnterEvent.AddListener(CardPointerEnter);
        dragHandler.PointerExitEvent.AddListener(CardPointerExit);
        dragHandler.BeginDragEvent.AddListener(BeginDrag);
        dragHandler.EndDragEvent.AddListener(EndDrag);

        Debug.Log(dragHandler.cardVisual);
        dragHandler.cardVisual.gameObject.GetComponent<CardInfo>().SetCardInfo(info);

        dragHandler.enableCard(false);

        return new_card;
    }

    private void BeginDrag(DragHandler card)
    {
        selectedCard = card;
    }


    void EndDrag(DragHandler card)
    {
        if (selectedCard == null)
            return;

        selectedCard.transform.DOLocalMove(Vector3.zero, tweenCardReturn ? .15f : 0).SetEase(Ease.OutBack);

        selectedCard = null;

    }

    void CardPointerEnter(DragHandler card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(DragHandler card)
    {
        hoveredCard = null;
    }

}
