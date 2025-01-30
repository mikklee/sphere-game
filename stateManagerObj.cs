using UnityEngine;
using System.Collections;
using System;

public class stateManagerObj : MonoBehaviour {

    public States currentState;
    public bool stateChanged;
    private Array states = Enum.GetValues(typeof(States));



    void Awake() {

    }

    public void setState(States state)
    {
        currentState = state;
        stateChanged = true;
    }

    public States getCurrentState()
    {
        return currentState;
    }

    public bool getState(States state)
    {
        if (currentState == state) return true;
        return false;
    }
}
