using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour,IDragHandler, IEndDragHandler
{
    public Vector3 originalPos;
    public Vector3 originalScale;
    public bool onDragFlag = false;
    public bool onAnim = false;

    public void OnDrag(PointerEventData eventData)
    {
        onDragFlag = true;
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.transform.position.y > 290)
        {
            //use card
            HandCardPool.instance.useCard(this.gameObject);
        }
        else if (this.transform.position.y <10)
        {
            HandCardPool.instance.discardCard(this.gameObject);
        }
        else
        {
            onDragFlag = true;
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 150, this.gameObject.transform.position.z);
           // this.transform.localScale = originalScale;
        }
    }

   /* public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log(eventData.position.y);
        if (eventData.position.y > 30) StartCoroutine(EnlargeCard());
    }*/

    IEnumerator EnlargeCard()
    {
            for (int i = 0; i < 10; i++)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + 0.02f, this.transform.localScale.y + 0.02f, this.transform.localScale.z);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5f, this.transform.position.z);
                yield return new WaitForSeconds(0.01f);
            }
        
    }

  /*  public void OnPointerExit(PointerEventData eventData)
    {
        if (!onDragFlag && eventData.position.y > 30)
            StartCoroutine(ShrinkCard());

        onDragFlag = false;
    }*/

    IEnumerator ShrinkCard()
    {
            for (int i = 0; i < 10; i++)
            {
            this.transform.localScale = new Vector3(this.transform.localScale.x + 0.02f, this.transform.localScale.y + 0.02f, this.transform.localScale.z);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 5f, this.transform.position.z);
                // this.transform.localScale = new Vector3(this.transform.localScale.x - 0.05f, this.transform.localScale.y - 0.05f, this.transform.localScale.z);
                yield return new WaitForSeconds(0.01f);
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        originalScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
