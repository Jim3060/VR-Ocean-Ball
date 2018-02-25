using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    public Text countText;
    public Text winText;
    public float speed;
    public float rotationSpeed;
    private int count;
    public float horizonRotation;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    private void FixedUpdate()
    {
        DoRotationMovement();
        DoJump();
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

    }

    private void DoJump() {
        bool moveJump = Input.GetKeyDown(KeyCode.Space);
        if (moveJump) { Debug.Log("jump"); rb.AddForce(Vector3.up * 80 * speed); }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 7) {
            winText.text = "YOU WIN";
        }
    }
}

