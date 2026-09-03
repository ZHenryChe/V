using UnityEngine;

public class cloudScript : MonoBehaviour
{
    Transform player;
    public float moveSpeed = 50f;
    private float initialYPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Phase1ManagerScript managerScript = GameObject.Find("GameManager").GetComponent<Phase1ManagerScript>();
        player = GameObject.Find("Player").transform;
        moveSpeed = UnityEngine.Random.Range(40f, 60f) / managerScript.platformSpawnInterval;
        initialYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, 
            initialYPosition + player.position.y * 0.25f, 
            transform.position.z);

        if (transform.position.x < player.position.x - 200)
        {
            Destroy(gameObject);
        }
    }
}
