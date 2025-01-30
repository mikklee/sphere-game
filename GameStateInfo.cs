using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateInfo : MonoBehaviour {

    public GameObject txt;
    private Text text;
    private int gameState = 1;

    // Use this for initialization
    void Awake () {
        text = txt.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        getStateChange();
    }

    private void getStateChange()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (gameState == 3)
            {
                gameState = 1;
            }
            else {
                gameState++;
            }
        }
    }

    public void setGameState() {
        text.text = "GameState: ";
        switch (gameState) {
            case 1: text.text += "Normal";
            break;
            case 2: text.text += "Gravity";
            break;
            case 3: text.text += "2D";
            break;

        }
    }
}
