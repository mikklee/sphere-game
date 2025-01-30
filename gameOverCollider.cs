using UnityEngine;
using System.Collections;

public class gameOverCollider : MonoBehaviour {

    public GameObject gameOverStateMgr;
    private gameOverState gameOvr;

    void Awake() {
        gameOvr = gameOverStateMgr.GetComponent<gameOverState>(); //Communication with the gameOver controller/manager
    }

    void OnTriggerEnter(Collider c) {
        c.gameObject.SetActive(false);
        gameOvr.gameOver();
	}
}
