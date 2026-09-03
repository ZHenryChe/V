using JetBrains.Annotations;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float jumpForce = 100f;
    public float moveSpeed = 5.0f;
    public float maxVelocity = 10.0f;
    public float rotateSpeed = -500f;
    private bool isGrounded = true;
    private bool firstJump = false;

    private bool moveUsed = false;

    public Phase1ManagerScript managerScript;
    public AudioSource jumpSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        moveUsed = false;
        myRigidBody.linearVelocityX = 0f; // Reset horizontal velocity on collision
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            managerScript.gameOver = true;
            Debug.Log("Player hit an enemy!");
        }
    }

    public void jump()
    {
        if (isGrounded)
        {
            firstJump = true;
            myRigidBody.linearVelocityY = jumpForce;
            isGrounded = false;
            jumpSound.Play();
        }
        else if (firstJump && !isGrounded)
        {
            firstJump = false;
            myRigidBody.linearVelocityY = jumpForce * 0.7f;
            jumpSound.Play();
        }

        moveUsed = false;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        rotateSpeed = -1000f / managerScript.platformSpawnInterval;
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        if (!isGrounded && !moveUsed)
        {
            if (Input.GetKey(KeyCode.A))
            {   
                transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
                myRigidBody.AddForceX(-moveSpeed * 250 / managerScript.platformSpawnInterval * 0.9f);
                moveUsed = true;
                Debug.Log("Left Used");
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
                myRigidBody.AddForceX(moveSpeed * 250 / managerScript.platformSpawnInterval * 0.9f);
                moveUsed = true;
                Debug.Log("Right Used");
            }
        }
    }
}
