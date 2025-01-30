using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public GameObject menu;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && menu != null) {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                Time.timeScale = 1;
            }
            else if (menu != null){
                menu.SetActive(true);
                Time.timeScale = 0;
            }  
        }
	}

    public void quitGame() {
        Application.Quit();
    }

    public void play()
    {
        if (menu != null && menu.activeSelf)
        {
            menu.SetActive(false);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("prøve-Quetzalcoatl");
    }

    public void loadMenu() {
        SceneManager.LoadScene("Menu");
    }

    void OnGUI() {
    }
}
