using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Camera cam;
    public LayerMask playeableZone;
    public Transform body;
    public float heightSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        SetHandPos();
        SetBodyHeight();
    }
    public void SetHandPos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 15, playeableZone))
        {
            transform.position = hit.point;
        }
       
    }
    public void SetBodyHeight()
    {

        if (Input.GetKey(KeyCode.W))
        {
            body.Translate(Vector3.up * heightSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.Translate(-Vector3.up * heightSpeed * Time.deltaTime);
        }
    }
}
