using UnityEngine;
using System.Collections;

public class mainMenuBgMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Time.timeScale != 1) Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation *= Quaternion.Euler(-.04f, 0, 0);  
	}
}
