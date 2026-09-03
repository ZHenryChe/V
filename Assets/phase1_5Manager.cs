using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class phase1_5Manager : MonoBehaviour
{
    public AudioHandlerScript audioHandlerScript;

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(3.0f);  
        yield return StartCoroutine(audioHandlerScript.NarrateAudioAndWait("Phase1_5"));
        SceneManager.LoadScene("Phase2");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Phase 1.5 Manager started");
        Time.timeScale = 1f; // Ensure time scale is set to normal
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
