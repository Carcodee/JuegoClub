using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HandStateMachine
{

    public string stateName;
    public HandController handController;
    public StateMachineController StateMachineController;
    public HandStateMachine(StateMachineController stateMachineController, string stateName)
    {
        this.StateMachineController = stateMachineController;
        this.stateName = stateName;
    }
    public abstract void stateExit();
    public abstract void StateInput();
    public abstract void Initializate();
    public abstract void stateUpdate();
    public abstract void stateFixUpdate();




}
