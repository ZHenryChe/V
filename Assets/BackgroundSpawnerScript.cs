using System;
using UnityEngine;

public class BackgroundSpawnerScript : MonoBehaviour
{
    public bool doki = false;

    public GameObject player;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;

    public GameObject dokiFace2;
    public GameObject dokiFace3;
    public GameObject dokiFace4;
    public GameObject dokiFace5;
    private GameObject[] dokiFaceChoices;
    private float dokiFaceTimer = 0f;
    private int dokiChoice = 0;

    private GameObject[] cloudChoice = new GameObject[3];

    public Phase1ManagerScript managerScript;
    private float internalTimer = 0.0f;
    public float cloudSpawnInterval = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dokiFaceChoices = new GameObject[] {dokiFace2, dokiFace3, dokiFace4, dokiFace5};
        cloudChoice[0] = cloud1;
        cloudChoice[1] = cloud2;
        cloudChoice[2] = cloud3;
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        if (internalTimer > cloudSpawnInterval)
        {
            GameObject cloudSpawn = cloudChoice[UnityEngine.Random.Range(0, cloudChoice.Length)];
            float randomX = playerX + UnityEngine.Random.Range(200f, 300f);
            float randomY = UnityEngine.Random.Range(20f, 100f);
            Instantiate(cloudSpawn, new Vector3(randomX, randomY, 11), cloudSpawn.transform.rotation);
            internalTimer = 0.0f;
        }
        else
        {
            internalTimer += Time.deltaTime;
        }

        if (!doki)
        {
            return;
        }

        dokiFaceTimer += Time.deltaTime;

        if (dokiFaceTimer > 35.0f)
        {
            Debug.Log("Doki Face Spawned");
            GameObject dokiFaceSpawn = dokiFaceChoices[dokiChoice];
            float randomX = playerX + UnityEngine.Random.Range(0f, 150f);
            float randomY = UnityEngine.Random.Range(30f, 75f);
            Instantiate(dokiFaceSpawn, new Vector3(randomX, randomY, 12), dokiFaceSpawn.transform.rotation);
            dokiFaceTimer = 0.0f;
            dokiChoice++;
        }
    }
}
