using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    public Vector3 displayPosi;
    public Transform tutorial;
    public Vector3 hiddenPosi;
    public float moveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        Debug.Log("here1");
        if (other.gameObject.layer == 10)//player layer
        {
            tutorial.localPosition =Vector3.Lerp(tutorial.localPosition, displayPosi, moveSpeed * Time.deltaTime);
        }
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("here2");
        if (other.gameObject.layer == 10)//player layer
        {
            tutorial.localPosition = Vector3.Lerp(tutorial.localPosition, displayPosi, moveSpeed * Time.deltaTime);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)//player layer
        {
            tutorial.localPosition = hiddenPosi;
        }
    }

}
