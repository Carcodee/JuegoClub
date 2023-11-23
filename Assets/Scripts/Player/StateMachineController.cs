using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    public HandStateMachine[] handStates;
    public HandStateMachine currentState;
    public MovementState movement;
    public PickingState pickingState;
    public ObjPickedState objPicked;
    public OnHoldState onHold;

    public void Initializate()
    {
        handStates = new HandStateMachine[4];
        movement = new MovementState(this, "Movement");
        pickingState = new PickingState(this, "Picking");
        objPicked = new ObjPickedState(this, "Picked");
        onHold = new OnHoldState(this, "Holdeable");
        handStates[0] = movement;
        handStates[1] = pickingState;
        handStates[2] = objPicked;
        handStates[3] = onHold;
        currentState = handStates[0];

        if (currentState != null)
        {
            currentState.Initializate();
        }
    }
    public void StateUpdate()
    {
        if (currentState != null)
        {
            currentState.stateUpdate();
        }
    }
    public void StateFixUpdate()
    {
        if (currentState != null)
        {
            currentState.stateFixUpdate();
        }
    }
    public void StateLateUpdate()
    {
        if (currentState != null)
        {
            currentState.stateUpdate();
        }
    }

    public HandStateMachine GetStateByName(string name)
    {
        foreach (var state in handStates)
        {
            if (state.stateName==name)
            {
                return state;
            }
        }
        return null;
    }
    public void SetStateByName(string name)
    {
        HandStateMachine nextState= GetStateByName(name);
        if (nextState == null) return;
        //Exit state execution
        if (nextState != null)
        {
            nextState.stateExit();
        }
        currentState = nextState;
        currentState.Initializate();
    }
}
