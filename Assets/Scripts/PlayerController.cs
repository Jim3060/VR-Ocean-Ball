using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    
    public float speed;
    public float rotationSpeed;
    public float horizonRotation;

    public ControllerManager manager;

    bool ground = true;
    private void Start()
    {
        ground = true;
        rb = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {
        DoMovementVR();
        DoRotationMovement();
        DoJump();
    }

    private List<Vector3> sampleLeftPoints=new List<Vector3>();
    private List<Vector3> sampleRightPoints=new List<Vector3>();
    public int sampleGap=1;
    private int sampleCount=0;

    private void updateSamplePoints() {
        sampleCount += 1;
        if (sampleCount < sampleGap) { return; }
        else { sampleCount = 0; }
        if (manager.leftControllerTouchDown.grip)
        {
            if (sampleLeftPoints.Count != 3)
            {
                sampleLeftPoints.Add(manager.leftControllerPosition);

            }
            else {
                sampleLeftPoints[0] = sampleLeftPoints[1];
                sampleLeftPoints[1] = sampleLeftPoints[2];
                sampleLeftPoints[2] = manager.leftControllerPosition;
            }
           
        }
        else {
            sampleLeftPoints.Clear();
        }


        if (manager.rightControllerTouchDown.grip)
        {
            if (sampleRightPoints.Count != 3)
            {
                sampleRightPoints.Add(manager.rightControllerPosition);

            }
            else
            {
                sampleRightPoints[0] = sampleRightPoints[1];
                sampleRightPoints[1] = sampleRightPoints[2];
                sampleRightPoints[2] = manager.rightControllerPosition;
            }

        }
        else
        {
            sampleRightPoints.Clear();
        }
    }

    private void DoMovementVR() {
        updateSamplePoints();
        Vector3 result = Vector3.zero;
        Vector3 left,right;
       
        if (sampleLeftPoints.Count == 3) {
            left = Vector3.Cross((sampleLeftPoints[0] - sampleLeftPoints[1]), (sampleLeftPoints[1] - sampleLeftPoints[2]));
            result += left;
            Debug.Log("left m:"+left);
            Debug.Log(sampleLeftPoints);
        }
        if (sampleRightPoints.Count == 3)
        {
            right = Vector3.Cross((sampleRightPoints[0] - sampleRightPoints[1]), (sampleRightPoints[1] - sampleRightPoints[2]));
            result += right;
            Debug.Log("right m:" + right);
            Debug.Log(sampleRightPoints);
        }
        rb.AddTorque(result);
    }

    private void DoRotationMovement() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool leftRotate = Input.GetKey(KeyCode.Q);
        bool rightRotate = Input.GetKey(KeyCode.E);
        float rotate = 0;
        if (leftRotate) { rotate -= 1; }
        if (rightRotate) { rotate += 1; }
        horizonRotation = rotate;
        Vector3 horizontalRotation = new Vector3(0, rotate, 0);


        Vector3 angleV = new Vector3(0, 0, moveVertical);
        Vector3 angleH = new Vector3(moveHorizontal, 0, 0);
        Vector3 crossv = Vector3.Cross(Vector3.up, angleV);
        Vector3 crossh = Vector3.Cross(Vector3.up, angleH);

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddTorque(crossv + crossh+horizontalRotation);
        //Debug.Log(rb.angularVelocity);

    }

    private void DoJump() {
        bool moveJump = Input.GetKeyDown(KeyCode.Space);
        if (ground == true && moveJump)
        {
            
            
            Debug.Log("jump"); rb.AddForce(Vector3.up * 80 * speed); 
            ground = false;
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ground = true;
    }

 
   
}

