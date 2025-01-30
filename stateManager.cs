
/* 
This class manages game states of a game, and makes necessarry adjustments.
Authors: Michael T. Lee, Edvard Blunck
Year: 2016
Sources for inspiration:

*/
using System.Collections;
using UnityEngine;

internal class stateManager : ArrayList
{
    private int currentState = 0;
    public bool stateChanged;

    public stateManager(string startState) : base(){
        Add("Paused");
        Add("Normal");
        Add("Gravity");
        Add("2D");
        currentState = IndexOf(startState);
    }

    public bool getState(string state) {
        if (currentState == IndexOf(state)) {
            return true;
        }
        return false;
    }

    public int getCurrentState()
    {
            return currentState;
    }

    public void setState(string state) {
        currentState = IndexOf(state); // Do not count paused as a current state
    }

    public void getStateChange()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (currentState == Count -1)
            {
                currentState = 1;
            }
            else
            {
                currentState++;
            }
            stateChanged = true;
        }
    }
}