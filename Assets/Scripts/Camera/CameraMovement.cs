using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof(Camera))]
public class CameraMovement : ACamera
{
    [Header("Components Ref's")]
    [SerializeField] private Camera cam;

    [Header("Camera Bounds")]
    [SerializeField] private Vector2 boundsMax;
    [SerializeField] private Vector2 boundsMin;

    [Header("Camera Zooming Settings")]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomMin;
    [SerializeField] private float zoomMax;


    private Vector3 dragOrigin;


    private void LateUpdate()
    {
        switch(cameraState)
        {
            case CameraState.Active:
                MouseDragMovement();
                Zooming();
                break;
        }
    }

    private void MouseDragMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            var posDifference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position += posDifference;
            ClampTransform();
        }
    }

    private void ClampTransform()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundsMin.x, boundsMax.x),
                                         Mathf.Clamp(transform.position.y, boundsMin.y, boundsMax.y));
    }

    private void Zooming()
    {
        if(cam.orthographic)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomMin, zoomMax);
        }
    }


    public override Vector2 CameraLCorner()
    {
        float height = cam.pixelHeight * 0.8f;
        float widht = cam.pixelWidth * 0.10f;
        Vector2 cornerL = cam.ScreenToWorldPoint(new Vector2(widht, height));
        return cornerL;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float height = cam.pixelHeight * 0.8f;
        float widht = cam.pixelWidth * 0.10f;
        Vector2 cornerL = cam.ScreenToWorldPoint(new Vector2(widht, height));
        Gizmos.DrawLine(cornerL, Vector2.one);
    }
}
   
