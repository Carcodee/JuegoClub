using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
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

    [Header("Object")]
    
    public Transform currentObject;
    public bool objectPicked = false;
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
        if (Input.GetKey(KeyCode.Mouse0)&&currentObject!=null)
        {
            Pick();
        }

        if (Input.GetKey(KeyCode.A))
        {
            vcam.transform.Translate(-Vector3.right * Time.deltaTime * 2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            vcam.transform.Translate(Vector3.right * Time.deltaTime * 2);
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

        if (Input.GetKey(KeyCode.W) )
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
        if (objectPicked)
        {
            currentObject.position = hand.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropZone>(out DropZone d)) return;
        
        //interface
        //TODO: This is wrong, we need to check if the object is pickable
        if (other.transform.parent.transform.parent.TryGetComponent<ObjectController>(out ObjectController obj))
        {
            currentObject = other.GetComponent<Transform>();
            obj.isPicked=true;
            objectPicked=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DropZone>(out DropZone d)) return;
        objectPicked = true;
        if (other.transform.parent.transform.parent.TryGetComponent<ObjectController>(out ObjectController obj))
        {
            obj.isPicked = false;
            currentObject = null;
        }

    }
    
}
