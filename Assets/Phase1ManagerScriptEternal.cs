using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class Phase1ManagerScriptEternal : MonoBehaviour
{
    public float platformSpawnInterval = 2.0f;

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
        yield return null;
        Time.timeScale = 1f;
        backgroundMusic.Play();
    }

    IEnumerator TrueGameOver()
    {
        yield return null;
        Time.timeScale = 0f;
        backgroundMusic.Stop();
        player.GetComponent<AudioSource>().mute = true;
        gameOverScreen.SetActive(true);

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
