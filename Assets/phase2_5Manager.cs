using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class phase2_5Manager : MonoBehaviour
{

    public AudioHandlerScript audioHandlerScript;

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(3.0f);
        yield return StartCoroutine(audioHandlerScript.NarrateAudioAndWait("Phase2_5"));
        SceneManager.LoadScene("Phase3");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(Intro());
        Debug.Log("Phase 2.5 Manager started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
