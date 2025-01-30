using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public GameObject spawnPointTerrain;
    public GameObject sphere;
    public GameObject stateManager;
    private stateManagerObj stMgr;
    public GameObject inventoryObj;
    private Inventory inventory;

    void Awake() {
        stMgr = stateManager.GetComponent<stateManagerObj>();
        inventory = inventoryObj.GetComponent<Inventory>();
    }
    void OnTriggerStay(Collider sphere)
	{
        sphere.transform.position = spawnPointTerrain.transform.position;
        if (!inventory.useItemType(Items.Secret))
        {
            stMgr.setState(States.Zero);
        }
        else
        {

            stMgr.setState(States.Gravity);
        }  
    }	

}