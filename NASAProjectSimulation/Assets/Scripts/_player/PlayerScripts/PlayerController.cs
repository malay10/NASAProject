using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public int jumpTakeOff;
    private Rigidbody rb;
    private bool doubleJump;
   
    
    private float distanceToGround;
    // Start is called before the first frame update
    void Start()
    {

        
        jumpTakeOff = 5;
        speed = 10;
        doubleJump = false;
        rb = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float moveVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            //Vector3 move = new Vector3(moveHorizontal, 0, moveVertical);
            transform.Translate(moveHorizontal, 0, moveVertical);
            //rb.AddForce(move * speed);


            if (Input.GetKeyDown("escape"))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {

                doubleJump = true;
                rb.AddForce(Vector3.up * jumpTakeOff, ForceMode.Impulse);

            }
            else if (doubleJump && Input.GetButtonDown("Jump"))
            {
                doubleJump = false;

                rb.AddForce(Vector3.up * jumpTakeOff, ForceMode.Impulse);

            }
            else if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumping");
                rb.AddForce(Vector3.up * jumpTakeOff, ForceMode.Impulse);
            }
        

    }

    //Ground Check
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector2.up, distanceToGround + 0.1f);

    }
}

