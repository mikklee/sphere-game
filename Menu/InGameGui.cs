using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InGameGui : MonoBehaviour {
    public GameObject t_oJump;
    public GameObject t_oGravFlip;
    public GameObject t_o2D;
    public GameObject t_oSecret;
    public GameObject inventoryObj;
    public GameObject timerObj;
    public GameObject gameOverStateMgr;
    private gameOverState gameOvr;
    private Inventory inventory;
    private Text txt_Jump;
    private Text txt_2D;
    private Text txt_GravFlip;
    private Text txt_Secret;
    private Text txt_timer;


    // Use this for initialization
    void Awake () {
        inventory = inventoryObj.GetComponent<Inventory>();
        gameOvr = gameOverStateMgr.GetComponent<gameOverState>();
        txt_Jump = t_oJump.GetComponent<Text>();
        txt_2D = t_o2D.GetComponent<Text>();
        txt_GravFlip = t_oGravFlip.GetComponent<Text>();
        txt_Secret = t_oSecret.GetComponent<Text>();
        txt_timer = timerObj.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        txt_Jump.text = inventory.getCount(Items.Jump);
        txt_2D.text = inventory.getTime(Items.TwoD);
        txt_GravFlip.text = inventory.getTime(Items.FlipGravity);
        txt_Secret.text = inventory.getCount(Items.Secret);
        txt_timer.text = gameOvr.getTime();
    }
}
