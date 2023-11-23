using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoldState : HandStateMachine
{
    public OnHoldState(StateMachineController stateMachineController, string stateName) : base(stateMachineController, stateName)
    {
        handController = stateMachineController.GetComponent<HandController>();
    }
    float currentTime = 0;
    float speedRelease = 0.2f;
    float onEnterSpeed;
    public override void StateInput()
    {
    }
    public override void Initializate()
    {
        onEnterSpeed= handController.handSpeed;

        handController.handSpeed = speedRelease;

    }

    public override void stateExit()
    {
        currentTime = 0;
        handController.handSpeed = onEnterSpeed;
    }

    public override void stateFixUpdate()
    {
    }

    public override void stateUpdate()
    {
        currentTime += Time.deltaTime;
        Debug.Log(currentTime);
        if (currentTime >= handController.holdTime)
        {
            StateMachineController.SetStateByName("Picked");
        }

        handController.SetHandPos();
        handController.SetHandHeight();
      
    }
}
