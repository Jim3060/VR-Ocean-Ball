using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerController : MonoBehaviour {

	public GameObject powerUpBoard;
	public GameObject powerDownBoard;

	// Use this for initialization
	void Start () {
		powerUpBoard.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void powerUp(){
		powerUpBoard.SetActive (true);
		powerDownBoard.SetActive (false);
	}

	void powerDown(){
		powerUpBoard.SetActive (false);
		powerDownBoard.SetActive (true);
	}
}
