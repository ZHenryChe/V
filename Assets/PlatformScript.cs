using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    Transform player;
    public float moveSpeed = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Phase1ManagerScript managerScript = GameObject.Find("GameManager").GetComponent<Phase1ManagerScript>();
        player = GameObject.Find("Player").transform;
        moveSpeed = 100f / managerScript.platformSpawnInterval;
        //Debug.Log("Platform Move Speed: " + moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
       if (transform.position.x < player.position.x - 200)
       {
           Destroy(gameObject);
       }
    }
}
