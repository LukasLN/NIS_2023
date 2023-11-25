using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float groundDrag;

    public float playerHeight;
    public LayerMask groundLayer;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        
        //grounded = Physics.Raycast(transform.position/*+ new Vector3(0,0.2f,0)*/, Vector3.down, 0.3f, groundLayer);
        grounded = Physics.Raycast(transform.position, -Vector3.up,playerHeight*0.5f+ 0.1f);
        Debug.Log(grounded);

        MyInput();
        SpeedControl();

        //handles drag
        if(grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
           // rb.velocity = new Vector3(rb.velocity.x, -9.82f*50, rb.velocity.z)*Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.VelocityChange);
    }

    private  void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f/* rb.velocity.y*/ , rb.velocity.z);

        //limits velocity
        if(flatVel.magnitude /*Math.Sqrt((rb.velocity.x*rb.velocity.x)+(rb.velocity.z*rb.velocity.z))*/ > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z)*Time.deltaTime;
        }
    }
}
