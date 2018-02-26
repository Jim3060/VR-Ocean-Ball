using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public GameObject door;
    public GameObject key;

    private bool trigged = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (trigged) {
            Destroy(key);
            Destroy(door);
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("开始接触");
        trigged = true;
    }
}
