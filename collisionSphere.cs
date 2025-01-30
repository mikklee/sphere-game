using UnityEngine;
using System.Collections;

public class collisionSphere : MonoBehaviour {

    public GameObject InventoryObj;
    private Inventory inventory;


    void Awake() {
        inventory = InventoryObj.GetComponent<Inventory>();
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("powerUp")) {
            powerUp pwr = c.gameObject.GetComponent<powerUp>();
            if (inventory.addItem(pwr.itemType, pwr.usageTime, pwr.useCount, pwr.onlyCount)) Destroy(c.gameObject);
        }
    }
}
