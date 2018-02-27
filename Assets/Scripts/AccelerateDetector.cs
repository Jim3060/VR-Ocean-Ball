using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateDetector : MonoBehaviour {

    public GameObject switchDetector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        switchDetector.SendMessage("enterAccZone");
    }

    void OnTriggerExit(Collider collider)
    {
        switchDetector.SendMessage("exitAccZone");
    }
}
