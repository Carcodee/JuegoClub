using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : HandStateMachine
{
    public MovementState(StateMachineController stateMachineController, string stateName) : base(stateMachineController, stateName)
    {
        handController = stateMachineController.GetComponent<HandController>();
    }
    public override void StateInput()
    {

    }
    public override void Initializate()
    {

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
        if (Input.GetKeyDown(KeyCode.Mouse0)&&handController.currentObject!=null)
        {
            StateMachineController.SetStateByName("Picking");
        }
        
    }
}
