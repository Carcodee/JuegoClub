using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HandStateMachine
{

    public string stateName;
    public HandController controller;
    public HandStateMachine(HandController handController, string stateName)
    {
        this.controller = handController;
        this.stateName = stateName;
    }
    public abstract void stateExit();

    public abstract void Initializate();

    public abstract void stateUpdate();

    public abstract void stateFixUpdate();




}
