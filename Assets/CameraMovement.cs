using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        gameObject.transform.position = new Vector3(playerPosition.x + 75, playerPosition.y + 20, -10f);
    }
}
