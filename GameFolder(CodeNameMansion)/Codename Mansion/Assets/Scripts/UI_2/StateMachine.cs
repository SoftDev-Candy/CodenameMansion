using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static State previousState;
    public static bool restart = false;
    [SerializeField] List<State> states = new List<State>();
    State currentState;
    public static StateMachine Singleton;
    public void SwitchState<aState>()
    {
        foreach (State state in states)
        {
            if (state.GetType() == typeof(aState))
            {
                currentState?.ExitState();
                previousState = currentState;
                currentState = state;
                if (currentState.GetType() == typeof(PlayingState))
                {
                    if (previousState.GetType() == typeof(PausedState))
                    {
                        restart = false;
                    }
                }
                currentState?.EnterState();
            }
        }
        //Debug.Log(typeof(aState));
    }
    public void Start()
    {
        Singleton = this;
    }
    public void SwitchToPrevious()
    {
        currentState?.ExitState();
        currentState = previousState;
        previousState = null;
        currentState.EnterState();
    }

    protected void StateMachineUpdate()
    {
        //Debug.Log(currentState.hasEnteredState);
        if (currentState.hasEnteredState) currentState.UpdateState();
    }
}
