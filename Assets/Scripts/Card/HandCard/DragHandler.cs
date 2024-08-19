using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour,IDragHandler, IEndDragHandler,IBeginDragHandler, IPointerEnterHandler,IPointerExitHandler
{
    public int card_id;

    private VisualCardsHandler visualHandler;
    private Vector3 offset;

    public Vector3 originalPos;
    public Vector3 originalScale;
    public bool onDragFlag = false;
    public bool onAnim = false;

    [Header("Events")]
    [HideInInspector] public UnityEvent<DragHandler> PointerEnterEvent;
    [HideInInspector] public UnityEvent<DragHandler> PointerExitEvent;
    [HideInInspector] public UnityEvent<DragHandler, bool> PointerUpEvent;
    [HideInInspector] public UnityEvent<DragHandler> PointerDownEvent;
    [HideInInspector] public UnityEvent<DragHandler> BeginDragEvent;
    [HideInInspector] public UnityEvent<DragHandler> EndDragEvent;
    [HideInInspector] public UnityEvent<DragHandler, bool> SelectEvent;

    [Header("Visual")]
    [SerializeField] private GameObject cardVisualPrefab;
    public HandCardVisual cardVisual;

    [Header("States")]
    public bool isHovering;
    public bool isDragging;
    [HideInInspector] public bool wasDragged;

    [Header("Selection")]
    public bool selected;
    public float selectionOffset = 50;

    // Start is called before the first frame update
    void Awake()
    {
        visualHandler = FindObjectOfType<VisualCardsHandler>();
        cardVisual = Instantiate(cardVisualPrefab, visualHandler.transform).GetComponent<HandCardVisual>();
        cardVisual.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent.Invoke(this);
        offset = (Vector3)eventData.position - this.transform.position;
        isDragging = true;
        wasDragged = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = (Vector3)eventData.position - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent.Invoke(this);
        isDragging = false;
        //  canvas.GetComponent<GraphicRaycaster>().enabled = true;
        //  imageComponent.raycastTarget = true;

        StartCoroutine(FrameWait());

        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            wasDragged = false;
        }
        if (this.transform.position.y > 290)
        {
            //use card
            HandCardPool.instance.useCard(card_id);
        }
        else if (this.transform.position.y <10)
        {
            HandCardPool.instance.discardCard(card_id);
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {

        PointerEnterEvent.Invoke(this);
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExitEvent.Invoke(this);
        isHovering = false;
    }

    private void OnDestroy()
    {
        if (cardVisual != null)
            Destroy(cardVisual.gameObject);
    }

    public void enableCard(bool flag)
    {
       cardVisual.gameObject.SetActive(flag);
       transform.parent.gameObject.SetActive(flag);
    }

}
