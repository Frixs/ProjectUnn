using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
   

    public Vector3 defaultOffset;
    private Camera cam;
    private Vector3 offsetForOffset;
    public float maxDistance;
    public float scaleFactor;

    public Transform lookAt;

    private bool smooth = true;
    private float smoothSpeed = 2f;
    public Vector3 offset = new Vector3(0, 10, -5);
    private void Start()
    {
        cam = Camera.main;
    }
    private void LateUpdate()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            offsetForOffset = (hit.point - lookAt.position) * scaleFactor;
        }
        else
            offsetForOffset = Vector3.zero; 
        if (offsetForOffset.magnitude > maxDistance)
        {
            offsetForOffset.Normalize(); 
            offsetForOffset = offsetForOffset * maxDistance;
        }
        offset = defaultOffset + offsetForOffset;

        Vector3 desiredPosition = lookAt.transform.position + offset;

        if (smooth)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }




}
