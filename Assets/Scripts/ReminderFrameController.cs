using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderFrameController : MonoBehaviour {
    // Use this for initialization
    private List<Transform> frames;

	void Start () {
        frames = new List<Transform>();
        Transform[] ts;
        ts = transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.name != "reminderFrame") { frames.Add(t); }
        }
    }

    public void activate() {
        foreach (Transform t in frames) {
            t.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
        }
    }

    public void deactivate()
    {
        foreach (Transform t in frames)
        {
            t.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
    }
}
