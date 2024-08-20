using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TrushBin : MonoBehaviour
{
    public Transform target;
    [Header("Follow Parameters")]
    [SerializeField] private float followSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void HorizontalSmoothFollow()
    {
        Vector3 pos = new Vector3(target.position.x, transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
        transform.DOLocalMove(new Vector3(0, -200, 0), 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            HorizontalSmoothFollow();
        }

    }
}
