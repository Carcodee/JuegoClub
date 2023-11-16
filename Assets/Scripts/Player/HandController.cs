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

    [Header("HandConfigs")]
    public float maxHandHeight;
    public float sensibility;
    public float heightSpeed;
    float xRotation;
    float yRotation;
    //this will control the height
    public float heightOffSet;
    public float maxHeight;

    [Header("Object")]
    
    public Transform currentObject;
    public Transform pickPoint;
    public bool objectPicked = false;

    [Header("PickObj")]
    public float holdTime;
    public float currentTime;
    
    void Start()
    {
        currentObject.parent = transform;
        pickPoint.parent = transform;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RotateHand();
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0) && currentObject!=null &&!objectPicked)
        {
            SetHandToTargetPos();
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0) && currentObject != null && objectPicked)
        {
            Pick();
        }
        else if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
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
        SetHandHeight();
    }
    public void SetHandPos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 15, playeableZone))
        {
            transform.position =new Vector3(hit.point.x, hit.point.y + heightOffSet, hit.point.z);
        }
       
    }
    public void SetHandToTargetPos()
    {
        if (currentObject!=null)
        {
            if (currentTime >= 1)
            {
                Pick();
                return;
            }
            currentTime += Time.deltaTime;
            Vector3 dir= pickPoint.position- currentObject.transform.position;
            transform.position = Vector3.Lerp(transform.position, pickPoint.position, currentTime);
            
        }
    }
    
    public void SetHandHeight()
    {

        if (Input.GetKey(KeyCode.S) && heightOffSet > 0 )
        {
            heightOffSet -= 0.01f ;
        }
        if (Input.GetKey(KeyCode.W)&& heightOffSet < maxHandHeight)
        {
            heightOffSet += 0.01f;
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
        if (other.TryGetComponent<IDangerZone>(out IDangerZone dangerElement))
        {
            dangerElement.HandleDamage();
            //actions when the player gets hit
        }
        //interface
        //TODO: This is wrong, we need to check if the object is pickable
        if (other.TryGetComponent<ObjectController>(out ObjectController obj))
        {
            currentObject = obj.transform;
            pickPoint = obj.pickSpot;
            obj.isPicked=true;
            objectPicked=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        objectPicked = true;
        if (other.TryGetComponent<ObjectController>(out ObjectController obj))
        {
            obj.isPicked = false;
            pickPoint = null;
            currentObject = null;
        }

    }
    
}
