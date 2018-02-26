using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorController : MonoBehaviour {
   // public PlayerController pc;
    public Rigidbody playerBody;
    public GameObject mydoor;

    private float translateSpeed = 0.005f;
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
            //float r = translateSpeed * pc.horizonRotation;
            Vector3 rv = Vector3.Cross((other.transform.position - transform.position), playerBody.angularVelocity);
           // float r = translateSpeed * playerBody.angularVelocity.y;

            BoxCollider box = gameObject.GetComponentInChildren<BoxCollider>();

            if ((mydoor.transform.localPosition.x <= box.size.x&&rv.x>0)||(mydoor.transform.localPosition.x>=0 &&rv.x<0))
            {
                mydoor.transform.Translate(new Vector3(translateSpeed * rv.x, 0, 0));
                box.center = new Vector3(mydoor.transform.localPosition.x, box.center.y, box.center.z);
            }
            
        }
       
    }
}
