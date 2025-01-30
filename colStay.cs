using UnityEngine;
using System.Collections;

public class colStay : MonoBehaviour {

    public bool active = false; //Rigidbody exists within zone

    void Awake() {
    }

    private void setActive() {
    }

    void OnTriggerStay(Collider other) {
        active = true;
        print("object on board");
    }

    void OnTriggerExit(Collider other)
    {
        active = false;
        print("object over board");
    }
}
