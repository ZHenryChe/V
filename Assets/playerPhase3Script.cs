using UnityEngine;
using System;

public class playerPhase3Script : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float moveSpeed = 5000.0f;
    public float rotateSpeed = -5000f;
    public float maxVelocity = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody.AddForceY(200f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(myRigidBody.linearVelocityY) < maxVelocity)
        {
            if (Input.GetKey(KeyCode.W))
            {
                myRigidBody.AddForceY(moveSpeed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                myRigidBody.AddForceY(-moveSpeed);
            }
        }

        if (Math.Abs(myRigidBody.linearVelocityX) < maxVelocity)
        {
            if (Input.GetKey(KeyCode.A))
            {
                myRigidBody.AddTorque(rotateSpeed * Time.deltaTime);
                myRigidBody.AddForceX(-moveSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myRigidBody.AddTorque(-rotateSpeed * Time.deltaTime);
                myRigidBody.AddForceX(moveSpeed);
            }
        }
    }
}
