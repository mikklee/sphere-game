using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class gameOverState : MonoBehaviour {

    public GameObject trackCollider;
    public GameObject terrainCollider;
    private colStay col_track;
    private colStay col_terr;
    public GameObject gameOverMenu;
    public GameObject gameOverText;
    public GameObject buttonText;
    private Text txt_gameOver;
    private Text txt_button;
    private int time;
    private float annoyingTime;
    private string timeStr;

    void Awake() {
        col_track = trackCollider.GetComponent<colStay>();
        col_terr = terrainCollider.GetComponent<colStay>();
        txt_gameOver = gameOverText.GetComponent<Text>();
        txt_button = buttonText.GetComponent<Text>();
        time = 0;
    }
	
	// Update is called once per frame
	void Start () {
        InvokeRepeating("checkTrigger", 1f, .5f); 
        //Check whether player-object is outside of trigger every x seconds
        //Delays start with x second
	}

    void FixedUpdate()
    {
        tickTimer(); //Tick clock
        updateTime(); //Update clock
    }

    private void checkTrigger() {
        if (!col_track.active && !col_terr.active)
        {
            gameOver(); //Player failed
        }
    }

    public void gameOver()
    {
        txt_gameOver.text = "Game Over!\nDid you forget a power-up?\nTry using number buttons to activate them";
        txt_button.text = "Try again";
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void win()
    {
        txt_gameOver.text = "Thank you for playing the alpha!\nYour finish-time: " + getTime() + "\nStay tuned for more levels";
        txt_button.text = "Beat your time";
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void tickTimer()
    {
        annoyingTime += Time.deltaTime;
        if (annoyingTime >= 1)
        {
            time++;
            annoyingTime = 0;
        }
    }

    //Count time and display at end
    private void updateTime()
    {
        if (time < 10)
        {
            timeStr = "00:0" + time;
        }
        else if (time < 60)
        {
            timeStr = "00:" + time;
        }
        else if (time / 60 < 10 && time % 60 < 10)
        {
            timeStr = "0" + time / 60 + ":0" + time % 60;
        }
        else if (time / 60 < 10 && time % 60 < 60)
        {
            timeStr = "0" + time / 60 + ":" + time % 60;
        }
        else if (time / 60 < 60 && time % 60 < 10)
        {
            timeStr = time / 60 + ":0" + time % 60;
        }
        else if (time / 60 < 60 && time % 60 < 60)
        {
            timeStr = time / 60 + ":" + time % 60;
        }
        else {
            gameOver();
        }
    }

    public string getTime() {
        return timeStr;
    }

}
