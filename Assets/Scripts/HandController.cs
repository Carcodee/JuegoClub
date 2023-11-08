using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Camera cam;
    public LayerMask playeableZone;
    public Transform body;
    public Transform plane;
    public Transform hand;
    public float maxHandHeight;
    public float sensibility;
    public float heightSpeed;
    float xRotation;
    float yRotation;
    public Transform currentObject;

    void Start()
    {
            currentObject.parent = transform;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RotateHand();
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Pick();
        }
        else
        {
            currentObject.parent = transform;
        }
        xRotation = 0;

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

    public void RotateHand()
    {
        float mouseX= Input.mousePosition.x* sensibility;
        float mouseY = Input.mousePosition.y* sensibility;
        xRotation -= mouseY;
        yRotation -= mouseX;
        xRotation=Mathf.Clamp(xRotation, -360, 360);
        yRotation = Mathf.Clamp(yRotation, 0, 360);

        hand.transform.localRotation=Quaternion.Euler(xRotation, 0, 0);
        hand.Rotate(Vector3.up, mouseX);
        hand.Rotate(Vector3.right, mouseY);

    }
    public void Pick()
    {
        
        if (currentObject != null)
        {
            currentObject.parent = transform;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Organo"))
        {
            currentObject = other.GetComponent<Transform>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Organo"))
        {
            currentObject.parent = null;
            currentObject = null;

        }
    }
    
}
