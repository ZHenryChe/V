using JetBrains.Annotations;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class playerPhase2Script : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float jumpForce = 100f;
    public float moveSpeed = 500.0f;
    public float maxVelocity = 10.0f;
    public float rotateSpeed = -500f;
    private bool isGrounded = true;
    private bool firstJump = false;

    public bool canMove = true;

    public Phase2GameManagerScript managerScript;
    public AudioSource narrator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void jump()
    {
        if (isGrounded)
        {
            firstJump = true;
            myRigidBody.linearVelocityY = jumpForce;
            isGrounded = false;
        }
        else if (firstJump && !isGrounded)
        {
            firstJump = false;
            myRigidBody.linearVelocityY = jumpForce * 0.7f;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (narrator.isPlaying)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump();
            }

            if (Input.GetKey(KeyCode.A) && myRigidBody.linearVelocityX > -maxVelocity)
            {
                myRigidBody.AddTorque(rotateSpeed * Time.deltaTime);
                myRigidBody.AddForceX(-moveSpeed);
            }

            if (Input.GetKey(KeyCode.D) && myRigidBody.linearVelocityX < maxVelocity)
            {
                myRigidBody.AddTorque(-rotateSpeed * Time.deltaTime);
                myRigidBody.AddForceX(moveSpeed);
            }
        }

    }
}
