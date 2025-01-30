using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour {

    public Items itemType;
    public float usageTime; //How long the powerUp can be used
    public int useCount; //How many times the powerUp may be used
    public bool onlyCount = false; //Should timer be ignored?
}
