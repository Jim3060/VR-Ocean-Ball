using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    
    public float speed;
    public float rotationSpeed;
	public float jumpSpeed=8;
    public float horizonRotation;

    public ControllerManager manager;
	public Transform cameraMain;

	private Vector3 vecMult(Vector3 v, float i){
		return new Vector3 (v.x * i, v.y * i, v.z * i);
	}
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
		DoJumpVR ();
        DoJump();
    }

    private List<Vector3> sampleLeftPoints=new List<Vector3>();
    private List<Vector3> sampleRightPoints=new List<Vector3>();
    public int sampleGap=1;
    private int sampleCount=0;

    private void updateSamplePoints() {
        
		if (manager.leftTriggerAxis.x!=0)
        {
			//Debug.Log ("here");
            if (sampleLeftPoints.Count != 3)
            {
                sampleLeftPoints.Add(manager.leftControllerPosition*1000);

            }
            else {
                sampleLeftPoints[0] = sampleLeftPoints[1];
                sampleLeftPoints[1] = sampleLeftPoints[2];
                sampleLeftPoints[2] = manager.leftControllerPosition*1000;
            }
           
        }
        else {
            sampleLeftPoints.Clear();
        }


		if (manager.rightTriggerAxis.x!=0)
        {
			//Debug.Log ("hereright");
            if (sampleRightPoints.Count != 3)
            {
			    sampleRightPoints.Add(vecMult(manager.rightControllerPosition,1000));
				
            }
            else
            {
			
                sampleRightPoints[0] = sampleRightPoints[1];
                sampleRightPoints[1] = sampleRightPoints[2];
			    sampleRightPoints[2] = vecMult(manager.rightControllerPosition,1000);
			//Debug.Log (manager.rightControllerPosition.x);
            }

        }
        else
        {
            sampleRightPoints.Clear();
        }
		return ;
    }


	private float sign(float f){
		if (f > 0) {
			return 1f;
		}
		if (f == 0) {
			return 0f;
		}
		return -1f;

	}

	private Vector3 stickTocameraMainLarge(Vector3 v){
		if (Mathf.Abs (v.y) > Mathf.Abs (v.x) + Mathf.Abs (v.z)) {
			Debug.Log ("Spin");
			Debug.Log (v);
			if (Mathf.Abs(v.y) > 10) {
				return new Vector3 (0, 10 * v.y, 0);
			} else {
				return  Vector3.zero;
			}

		}
		if (Mathf.Abs (v.x) < 2 && Mathf.Abs (v.z) < 2) {
			return Vector3.zero;
		}
		Vector3 vcopy = v;
		v = new Vector3 (v.x, 0, v.z);
		Debug.Log ("v:" + v);
		//Debug.Log ("cameraMain:" + cameraMain.forward);
		Vector3 turningDir = Vector3.Normalize(new Vector3(cameraMain.forward.x,0,cameraMain.forward.z));// turning right m direction
		Vector3 forwordDir=Vector3.Normalize(Vector3.Cross(turningDir,Vector3.up));
		//Debug.Log ("forwordMain:" + forwordDir);
		float forwordIndex = Vector3.Dot (v, forwordDir);
		float turningIndex = Vector3.Dot (v, turningDir);
		if (Mathf.Abs (forwordIndex) / Mathf.Abs (turningIndex) >= 0.85 && Mathf.Abs (forwordIndex) / Mathf.Abs (turningIndex) <= 1.15) {
			if (forwordIndex * turningIndex > 0) {
				return   vecMult ((forwordDir + turningDir), -0.705f * Vector3.Magnitude (vcopy));
			} else {
				return  vecMult ((-forwordDir + turningDir), 0.705f * Vector3.Magnitude (vcopy));
			}
		}
		if (Mathf.Abs (forwordIndex) > Mathf.Abs (turningIndex)) {
			//Debug.Log ("forwording!" + forwordIndex);
			//Debug.Log ("vrst:" + vecMult(forwordDir, Vector3.Magnitude(vcopy)));
			return   vecMult(forwordDir, -1*Vector3.Magnitude(vcopy));

		} else {
			//Debug.Log ("turning!" + turningIndex);
			return vecMult( turningDir,sign(turningIndex)*Vector3.Magnitude(vcopy));
		}
	}

	private Vector3 hotresultm;


    private void DoMovementVR() {
        //Debug.Log ("here");
        //Debug.Log("test1");
        //Debug.Log("test2");
        sampleCount++;
		if ((sampleCount < sampleGap) && (sampleRightPoints.Count==3)&& (sampleLeftPoints.Count==3)) { rb.AddTorque(hotresultm); return; }
        else { sampleCount = 0; }


		updateSamplePoints();

        Vector3 result = Vector3.zero;
        Vector3 left,right;
       
        if (sampleLeftPoints.Count == 3) {
            left = Vector3.Cross((sampleLeftPoints[0] - sampleLeftPoints[1]), (sampleLeftPoints[1] - sampleLeftPoints[2]));
			left = stickTocameraMainLarge (left);
			result += left;
            //Debug.Log("left m:"+left);
            //Debug.Log(sampleLeftPoints);
        }
		//Debug.Log (sampleRightPoints.Count);
        if (sampleRightPoints.Count == 3)
        {
			//Debug.Log("0："+sampleRightPoints[0]);
			//Debug.Log("1："+sampleRightPoints[1]);
			//Debug.Log("2："+sampleRightPoints[2]);

            right = Vector3.Cross((sampleRightPoints[0] - sampleRightPoints[1]), (sampleRightPoints[1] - sampleRightPoints[2]));
           // Debug.Log("force quatity:" + Vector3.Magnitude(right));
			//if (manager.rightControllerTouchDown.trigger){Debug.Log("force quatity:" + Vector3.Magnitude(right));}
			right=stickTocameraMainLarge(right);
			//if (Vector3.Magnitude (right) > 10) {
				result += right;
			//}

			//right = stickTocameraMainLarge(right);
			//Debug.Log ("fuck");
			//result += vecMult(cameraMain.forward,Vector3.Magnitude(right));
			//right =  right*10;

			//Debug.Log ("1v:" + (sampleRightPoints [0] - sampleRightPoints [1]));
			//Debug.Log("right m:" + right);
            
        }
		hotresultm = vecMult (result, speed/ sampleGap);     
		//rb.angularVelocity.Set(0,0,0);
        rb.AddTorque(hotresultm);
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

	private void DoJumpVR() {
		bool moveJump = manager.leftControllerTouchDown.grip||manager.rightControllerTouchDown.grip;
		if (ground == true && moveJump)
		{


			Debug.Log("jump"); rb.AddForce(Vector3.up * jumpSpeed * speed); 
			ground = false;
		}


	}
	private bool nonVibrateFirst=true;

    private void OnCollisionEnter(Collision collision)
    {
        ground = true;
		if (!nonVibrateFirst) {
			manager.VibrateBoth ();
		}
		nonVibrateFirst = false;
    }

 
   
}

