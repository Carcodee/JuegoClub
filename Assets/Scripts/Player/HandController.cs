using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public CinemachineVirtualCamera lookStatsVcam;
    public CinemachineVirtualCamera lookDroppeableZoneVcam;
    public Camera cam;



    public LayerMask playeableZone;
    public Transform body;
    public Transform plane;
    public Transform hand;

    [Header("HandConfigs")]
    public float maxHandHeight;
    public float sensibility;
    public float heightSpeed;
    public float handSpeed;
    float xRotation;
    float yRotation;
    //this will control the height
    public float heightOffSet;
    public float maxHeight;
    public Vector3 cursorPos;
    [Header("Object")]
    
    public Transform currentObject;
    public Transform pickPoint;
    public bool objectPicked = false;
    public Vector3 targetPos;
    public Vector3 endPos;
    [Header("PickObj")]
    public float holdTime;
    public float currentTime;

    [Header("StateMachine")]
    public StateMachineController controller;
    
    
    void Start()
    {
        currentObject.parent = transform;
        pickPoint.parent = transform;
        controller.Initializate();
        //TODO: Do lerp of cursor pos
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse1))
        //{
        //    RotateHand();
        //    return;
        //}
        //
        // if (Input.GetKey(KeyCode.A))
        // {
        //     vcam.transform.Translate(-Vector3.right * Time.deltaTime * 2);
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     vcam.transform.Translate(Vector3.right * Time.deltaTime * 2);
        // }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            LookStats();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LookDroppeableZone();
        }
        if (Input.GetKeyUp(KeyCode.Tab)||Input.GetKeyUp(KeyCode.D))
        {
            LookPacient();
        }

        xRotation = 0;
        Debug.Log(controller.currentState.stateName);
        SetCursorPos();
        controller.StateUpdate();
    }
    public void SetCursorPos()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 15, playeableZone))
        {
            cursorPos= new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
       
    }
    public void LookStats()
    {
        vcam.Priority = 0;
        lookDroppeableZoneVcam.Priority =0;
        lookStatsVcam.Priority = 10;
    }
    public void LookPacient()
    {
        vcam.Priority = 10;
        lookDroppeableZoneVcam.Priority =0;
        lookStatsVcam.Priority = 0;
    }
    public void LookDroppeableZone()
    {
        vcam.Priority = 0;
        lookStatsVcam.Priority = 0;
        lookDroppeableZoneVcam.Priority = 10;
    }
    public void SetHandPos()
    {
        if ((transform.position - cursorPos).magnitude <= Mathf.Epsilon)
        {
            return;
        }
        
        Vector3 target = (cursorPos-transform.position).normalized;
        transform.position+=target * handSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, cursorPos.y + heightOffSet
            , transform.position.z);
    }
    public void SetHandToTargetPos(ref float timer)
    {
        if (currentObject!=null)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(targetPos, pickPoint.position, timer / 1);
            
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
        objectPicked = true;
        currentObject.position = hand.position;

        
    }
    public void GetPickableData()
    {
        currentObject.TryGetComponent<IPickableType<HoldeableData>>(out IPickableType<HoldeableData> pickable);
        HoldeableData data = pickable.PickableType();
        holdTime = data.timeToRelease;
        Debug.Log(holdTime);
        controller.SetStateByName(data.pickTypeName);
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
            if (controller.currentState.stateName == "Picked")
            {
                return;
            }
            currentObject = obj.transform;
            pickPoint = obj.pickSpot;
            obj.isPicked=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        objectPicked = true;
        if (currentObject==null)
        {
            if (other.TryGetComponent<ObjectController>(out ObjectController obj))
            {
                obj.isPicked = false;
                pickPoint = null;
                currentObject = null;
                objectPicked = false;
            }
        }


    }
    
}
