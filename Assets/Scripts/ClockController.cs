using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {
    private Transform hourpointer;
    private Transform minutepointer;
    private Transform leftReminder;
    private Transform rightReminder;
    private ReminderFrameController rfc;
    private float accumulation;
    private float accumulationLimits = 1000;
    private int hour = 0;

    public Rigidbody playerBody;
    public Light dlight;
	public GameObject powerController;
	public GameObject elevatorBack;

    // Use this for initialization
    void Start () {
        Transform[] ts;
        ts= transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) {
            if (t.name == "hour") { hourpointer = t; }
            else if (t.name == "minute") { minutepointer = t; }
            else if (t.name == "leftReminder") { leftReminder = t; }
            else if (t.name == "rightReminder") { rightReminder = t; }
        }
        rfc = transform.GetComponentInChildren<ReminderFrameController>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { hourRotation(true); }
		if (hour == 21) {
			powerUp ();
		} else {
			powerDown ();
		}
    }

    private void updateReminder() {
        Color clear = new Color(1, 1, 1);
        if (accumulation > 0)
        {
            Color tmpcolor = new Color(1 - accumulation / accumulationLimits, 1, 1 - accumulation / accumulationLimits);
            rightReminder.GetComponent<MeshRenderer>().material.color = tmpcolor;
            leftReminder.GetComponent<MeshRenderer>().material.color = clear;
        }
        else {
            Color tmpcolor = new Color(1 + accumulation / accumulationLimits, 1, 1 + accumulation / accumulationLimits);
            leftReminder.GetComponent<MeshRenderer>().material.color = tmpcolor;
            rightReminder.GetComponent<MeshRenderer>().material.color = clear;
        }
    }
    private void fullRotationHandler() {
        if (Mathf.Abs( accumulation) >= accumulationLimits) {
            playerBody.angularVelocity.Set(0, 0, 0);
            hourRotation((accumulation>0));
            accumulation = 0;
            //change the camera and show the rotation;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)//player layer
        {
            rfc.activate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 10)//player layer
        {
            accumulation += playerBody.angularVelocity.y;
            //Debug.Log(accumulation + "here");
        }
        updateReminder();
        fullRotationHandler();
    }

    private void OnTriggerExit(Collider other)
    {
        accumulation = 0;
        updateReminder();
        rfc.deactivate();
    }

    IEnumerator forwardAnHour()
    {
        for (int i = 0; i < 180; i++)
        {
            
            hourpointer.transform.RotateAround(transform.position, transform.up, 2.0f/12.0f);
            minutepointer.transform.RotateAround(transform.position,transform.up,2);
            dlight.transform.Rotate(Vector3.right, 15f / 180f);// dlight.transform.localEulerAngles.x+ 1, dlight.transform.localEulerAngles.y, dlight.transform.localEulerAngles.z) ;
            yield return null;
        }
        
    }

    IEnumerator backwardAnHour()
    {
        for (int i = 0; i < 180; i++)
        {

            hourpointer.transform.RotateAround(transform.position, transform.up, -2.0f / 12.0f);
            minutepointer.transform.RotateAround(transform.position, transform.up, -2);
            dlight.transform.Rotate(Vector3.right, -15f / 180f);
            yield return null;
        }

    }

    void hourRotation(bool forword) {
        if (forword) { hour = (hour + 1) % 24; StartCoroutine(forwardAnHour()); }
        else { hour = (hour + 23) % 24; StartCoroutine(backwardAnHour()); }
    }

    public int getHour() {
        return hour;
    }

	private void  powerUp(){
		powerController.SendMessage ("powerUp");
		elevatorBack.SetActive (false);
	}

	private void powerDown(){
		powerController.SendMessage ("powerDown");
		elevatorBack.SetActive (true);
	}


}
