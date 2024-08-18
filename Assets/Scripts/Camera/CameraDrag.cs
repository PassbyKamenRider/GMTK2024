using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private Vector3 origin;
    private Vector3 diff;
    private bool isDragging;
    private Bounds cameraBounds;

    private void Start()
    {
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;
        float minX = Globals.MapBounds.min.x + camWidth;
        float maxX = Globals.MapBounds.max.x - camWidth;
        float minY = Globals.MapBounds.min.y + camHeight;
        float maxY = Globals.MapBounds.max.y - camHeight;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0f),
            new Vector3(maxX, maxY, 0f)
        );
    }

    public void OnDrag(InputAction.CallbackContext c)
    {
        if (c.started)
        {
            origin = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
        isDragging = c.started || c.performed;
    }

    private void LateUpdate() {
        if (!isDragging)
        {
            return;
        }
        diff = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        Vector3 targetPosition = origin - diff;
        transform.position = new Vector3(Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x),
        Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y),
        transform.position.z);
    }


}
