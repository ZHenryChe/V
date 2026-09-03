using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phase1ManagerScript : MonoBehaviour
{
    public float platformSpawnInterval = 2.0f;

    public bool eternal = false;
    public bool phase1Start = false;

    public AudioHandlerScript audioHandler;
    public GameObject gameOverScreen;
    public GameObject player;
    public TextMeshProUGUI score;
    public GameObject scorePanel;
    public AudioSource backgroundMusic;
   
    public bool gameOver = false;

    public float gameTimer = 0.0f;

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameOver = false;
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Intro()
    {
        if (eternal)
        {
            player.SetActive(true);
            backgroundMusic.loop = true;
            scorePanel.SetActive(true);
            Time.timeScale = 1f;
            backgroundMusic.Play();

        }
        else
        {
            Time.timeScale = 0f;
            player.SetActive(true);

            backgroundMusic.Stop();
            backgroundMusic.loop = true;

            //yield return null;
            yield return StartCoroutine(audioHandler.NarrateAudioAndWait("Phase1Intro"));

            scorePanel.SetActive(true);

            Time.timeScale = 1f;
            backgroundMusic.Play();
        }
    }

    IEnumerator TrueGameOver()
    {
        if (eternal)
        {
            Time.timeScale = 0f;
            backgroundMusic.Stop();
            player.GetComponent<AudioSource>().mute = true;
            gameOverScreen.SetActive(true);
            if (Convert.ToInt32(score.text) > PlayerPrefs.GetInt("goonGameHighScore", 0))
            {
                PlayerPrefs.SetInt("goonGameHighScore", Convert.ToInt32(score.text));
                PlayerPrefs.Save();
            }

        }
        else 
        { 
            Time.timeScale = 0f;
            backgroundMusic.Stop();
            player.GetComponent<AudioSource>().mute = true;
            yield return new WaitForSecondsRealtime(2f);
            //gameOverScreen.SetActive(true);
            if (Convert.ToInt32(score.text) > 100000)
            {
                Debug.Log("Good ending");
                yield return StartCoroutine(audioHandler.NarrateAudioAndWait("Phase1GameOverGood"));
            }
            else
            {
                Debug.Log("Bad ending");
                yield return StartCoroutine(audioHandler.NarrateAudioAndWait("SLAP"));
                yield return StartCoroutine(audioHandler.NarrateAudioAndWait("Phase1GameOverBad"));
            }
            yield return new WaitForSecondsRealtime(2f);
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Phase1_5");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        string newscore = ((int)(gameTimer * 1000f)).ToString();
        if (newscore.Length < 8)
        {
            newscore = new string('0', 8 - newscore.Length) + newscore;
        }
        score.text = newscore;
        
        if (gameOver && !oneTime)
        {
            oneTime = true;
            StartCoroutine(TrueGameOver());
        }

        

    }
    private bool oneTime = false;
}
