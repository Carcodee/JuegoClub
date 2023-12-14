using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingState : HandStateMachine
{
    public PickingState(StateMachineController stateMachineController, string stateName) : base(stateMachineController, stateName)
    {
        handController=stateMachineController.GetComponent<HandController>();
    }
    bool actionFinished;
    bool objPicked;
    float timer;
    public override void StateInput()
    {
       
    }
    public override void Initializate()
    {
        handController.targetPos = handController.transform.position;
        actionFinished = false;
        timer = 0;
        objPicked = false;
    }

    public override void stateExit()
    {
        timer = 0;
    }

    public override void stateFixUpdate()
    {
    }

    public override void stateUpdate()
    {
        if (objPicked)
        {
            timer -= Time.deltaTime;
            handController.transform.position = Vector3.Lerp(handController.targetPos, handController.endPos, timer / 1);
            if (timer <= 0.1)
            {
                handController.GetPickableData();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            actionFinished = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            actionFinished = true;
            handController.endPos = handController.transform.position;

        }
        if (actionFinished)
        {
            ReturnToPos();
            if (timer <= 0.1)
            {
                StateMachineController.SetStateByName("Movement");
            }
            return;
        }
        else
        {
            handController.SetHandToTargetPos(ref timer);
            if (timer>=0.99f)
            {
                StateMachineController.SetStateByName("Picked");
                objPicked = true;
                
            }
        }

        

    }
    public void ReturnToPos()   
    {
        if (timer <= 1)
        {
            timer -= Time.deltaTime;
            handController.transform.position = Vector3.Lerp(handController.targetPos, handController.endPos, timer / 1);
        }
    }
}
