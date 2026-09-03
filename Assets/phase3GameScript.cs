using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class phase3GameScript : MonoBehaviour
{
    public GameObject answerFieldObject;
    public GameObject panel;
    public TMP_InputField answerField;
    public AudioHandlerScript audioHandler;
    private float choiceTimer = 60f;

    private float timer = 0f;

    public void OnAnswerSubmitted(string value)
    {
        StartCoroutine(SaveAndLoad(value));
    }

    IEnumerator SaveAndLoad(string value)
    {
        Debug.Log("Player's answer: " + value);

        if (value.ToLower() == "dokibird")
        {
            PlayerPrefs.SetInt("ending3Unlocked", 1);
        }
        else if (value == "godILoveStrinovaSoMuchAndIfYouAreReadingThisIKnowItLooksTerribleButItWorks")
        {
            PlayerPrefs.SetInt("ending1Unlocked", 1);
            Debug.Log("Button works");
        }
        else
        {
            PlayerPrefs.SetInt("ending2Unlocked", 1);
        }

        PlayerPrefs.SetInt("gameCompleted", 1);
        PlayerPrefs.Save();

        // give Unity one frame before scene change
        yield return null;

        SceneManager.LoadScene("MainMenu");
    }


    IEnumerator Intro()
    {
        //Time.timeScale = 0f;
        if (PlayerPrefs.GetInt("fromSkipButton", 0) == 1)
        {
            PlayerPrefs.SetInt("fromSkipButton", 0);
            PlayerPrefs.Save();
            panel.SetActive(true);
            choiceTimer = 30f;

        }
        else
        {
            yield return audioHandler.StartCoroutine(audioHandler.NarrateAudioAndWait("Phase3Intro"));
            panel.SetActive(true);
            yield return audioHandler.StartCoroutine(audioHandler.NarrateAudioAndWait("Phase3Follow"));

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        answerFieldObject.SetActive(false);
        answerField.onEndEdit.AddListener(OnAnswerSubmitted);
        StartCoroutine(Intro());
        Debug.Log("Phase 3 Game Script started");

        //PlayerPrefs.DeleteAll(); //Test
        //PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= choiceTimer)
        {
            answerFieldObject.SetActive(true);
        }
    }
}
