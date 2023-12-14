using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPickedState : HandStateMachine
{
    public ObjPickedState(StateMachineController stateMachineController, string stateName) : base(stateMachineController, stateName)
    {
        handController = stateMachineController.GetComponent<HandController>();
    }
    public override void StateInput()
    {

    }
    public override void Initializate()
    {

        handController.Pick();
        if (!handController.currentObject.GetComponent<ObjectController>().isReleased)
        {
            handController.currentObject.GetComponent<ObjectController>().isReleased = true;
            GameManager.OnClipRelease?.Invoke(handController.currentObject.GetComponent<ObjectController>().objType);
        }

        
    }

    public override void stateExit()
    {
    }

    public override void stateFixUpdate()
    {
    }

    public override void stateUpdate()
    {
        handController.SetHandPos();
        handController.SetHandHeight();
        handController.Pick();

        if (Input.GetKeyUp(KeyCode.Mouse0)||handController.currentObject==null)
        {
            handController.pickPoint = null;
            handController.currentObject.GetComponent<ObjectController>().isPicked = false;
            handController.currentObject.GetComponent<ObjectController>().ActivateRb();

            handController.currentObject = null;
            handController.objectPicked = false;
            StateMachineController.SetStateByName("Movement");

        }
    }
}
