using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBtn : MonoBehaviour {

    public GameObject elevator;
    public float speed = 1;
    private bool trigged = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (trigged && elevator.transform.position.y >= 18.3)
        {
            elevator.transform.Translate(-elevator.transform.up * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("开始接触");
        trigged = true;
    }

    void OnTriggerExit(Collider collider)
    {
        trigged = false;
    }

}
