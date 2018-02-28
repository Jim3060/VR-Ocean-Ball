using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScript : MonoBehaviour {
	public Transform Win;
	public Rigidbody player;
	public Transform cameraMain;
	private bool win=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (win) {
			Win.position = player.transform.position + cameraMain.forward * 3;
		}
	}

	private void OnTriggerEnter(Collider other){
		win = true;
		player.AddForce (new Vector3 (0, 10000, 0));
	}
}
