using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorController : MonoBehaviour {
    public PlayerController pc;
    public GameObject mydoor;

    private float translateSpeed = 0.01f;
	// Use this for initialization
	void Start () {
        mydoor = gameObject.GetComponentInChildren<Rigidbody>().gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 10)//player layer
        {
            Debug.Log(other.gameObject);
            float r = translateSpeed * pc.horizonRotation;
            
            BoxCollider box = gameObject.GetComponentInChildren<BoxCollider>();

            if ((mydoor.transform.localPosition.x <= box.size.x&&r>0)||(mydoor.transform.localPosition.x>=0 &&r<0))
            {
                mydoor.transform.Translate(new Vector3(r, 0, 0));
                box.center = new Vector3(mydoor.transform.localPosition.x, box.center.y, box.center.z);
            }
            
        }
       
    }
}
