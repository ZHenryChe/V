using UnityEngine;

public class irisScript : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.x = playerPos.x / 5f;
        playerPos.y = playerPos.y / 10f - 2.25f;
        playerPos.z = -1f;
        gameObject.transform.position = playerPos;
    }
}
