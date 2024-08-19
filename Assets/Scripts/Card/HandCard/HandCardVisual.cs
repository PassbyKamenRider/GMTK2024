using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using DG.Tweening;

public class HandCardVisual : MonoBehaviour
{
    private bool initalize = false;

    [Header("Card")]
    public DragHandler parentCard;
    private Transform cardTransform;
    private int savedIndex;
    Vector3 movementDelta;

    [Header("Follow Parameters")]
    [SerializeField] private float followSpeed = 30;

    [Header("Scale Parameters")]
    [SerializeField] private bool scaleAnimations = true;
    [SerializeField] private float scaleOnHover = 1.15f;
    [SerializeField] private float scaleOnSelect = 1.25f;
    [SerializeField] private float scaleTransition = .15f;
    [SerializeField] private Ease scaleEase = Ease.OutBack;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (!initalize || parentCard == null) return;

        SmoothFollow();
    }

    private void SmoothFollow()
    {
        transform.position = Vector3.Lerp(transform.position, cardTransform.position, followSpeed * Time.deltaTime);
    }

    public void Initialize(DragHandler target, int index = 0)
    {
        //Declarations
        parentCard = target;
        cardTransform = target.transform;

        //Event Listening
        parentCard.PointerEnterEvent.AddListener(PointerEnter);
        parentCard.PointerExitEvent.AddListener(PointerExit);
        parentCard.BeginDragEvent.AddListener(BeginDrag);
        parentCard.EndDragEvent.AddListener(EndDrag);

        initalize = true;
    }


    private void PointerEnter(DragHandler card)
    {
        if (scaleAnimations)
            transform.DOScale(scaleOnHover, scaleTransition).SetEase(scaleEase);
    }

    private void PointerExit(DragHandler card)
    {
        if (!parentCard.wasDragged)
            transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }

    private void BeginDrag(DragHandler card)
    {
        if (scaleAnimations)
            transform.DOScale(scaleOnSelect, scaleTransition).SetEase(scaleEase);
    }

    private void EndDrag(DragHandler card)
    {
        transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }

}
