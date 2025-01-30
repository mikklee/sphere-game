using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrackCompleted : MonoBehaviour {

    public GameObject gameOverController;
    private gameOverState gameOverCtrl;

    void Awake() {
        gameOverCtrl = gameOverController.GetComponent<gameOverState>(); //Communication with the gameOver controller/manager
    }

	void OnTriggerEnter (Collider other){
        gameOverCtrl.win();
	}
}

