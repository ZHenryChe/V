using UnityEngine;
using System;

public class GameButtonScript : MonoBehaviour
{
    public int buttonID;
    public GameObject player;
    public Phase2GameManagerScript gameManager;
    public bool isPressed = false;

    public AudioSource buttonPressAudio;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Math.Abs(player.transform.position.x) < 83f && Math.Abs(player.transform.position.x) > 59f)
            {
                isPressed = true;
                Vector3 pos = gameObject.transform.position;
                float playerVelocityY = Math.Abs(player.GetComponent<Rigidbody2D>().linearVelocityY);
                if (playerVelocityY < 1f)
                {
                    playerVelocityY = 1f; // Minimum speed to ensure button press
                }
                pos.y -= playerVelocityY;
                gameObject.transform.position = pos;
            }

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -39f && buttonPress)
        {
            Debug.Log("Button pressed by player!");
            buttonPressAudio.Play();
            buttonPress = false;
            StartCoroutine(gameManager.ProcessAnswer(buttonID));
        }
    }

    public bool buttonPress = true;

}
