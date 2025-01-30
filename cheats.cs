using UnityEngine;
using System.Collections;

public class cheats : MonoBehaviour {

    public GameObject InventoryObj;
    private Inventory inventory;
    public bool cheatsOn = false;

    void Awake()
    {
        inventory = InventoryObj.GetComponent<Inventory>();
    }

    void Update() {
        if (cheatsOn) {
            inventory.addItem(Items.TwoD, 40, 0, false);
            inventory.addItem(Items.Secret, 0, 1, true);
            cheatsOn = false;
        }
    }
}
