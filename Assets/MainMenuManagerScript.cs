using System.IO;
using TMPro;
using UnityEngine;
using static phase3GameScript;

public class MainMenuManagerScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsPanel;
    public GameObject ending1Achiv;
    public GameObject ending2Achiv;
    public GameObject ending3Achiv;
    public GameObject skipToPhase3Button;

    public GameObject textCounter;
    public GameObject goonGameHighScore;

    public void EternalPhase1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Phase1Eternal");
    }

    public void SkipToPhase3()
    {
        PlayerPrefs.SetInt("fromSkipButton", 1);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Phase3");
    }

    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Phase1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsPanel.SetActive(true);
    }
    
    public void BackToMainMenu()
    {
        creditsPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //PlayerPrefs.SetInt("gameCompleted", 1);
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("ending1Unlocked", 1);
        //PlayerPrefs.Save();

        int score = 0;

        if (PlayerPrefs.GetInt("ending1Unlocked", 0) == 1)
        {
            ending1Achiv.SetActive(true);
            score += 1;
        }
        if (PlayerPrefs.GetInt("ending2Unlocked", 0) == 1)
        {
            ending2Achiv.SetActive(true);
            score += 1;
        }
        if (PlayerPrefs.GetInt("ending3Unlocked", 0) == 1)
        {
            ending3Achiv.SetActive(true);
            score += 1;
        }
        if (score != 0)
        {
            textCounter.SetActive(true);
            textCounter.GetComponent<TextMeshProUGUI>().text = score+"/3";
        }

        if (PlayerPrefs.GetInt("gameCompleted", 0) == 1)
        {
            skipToPhase3Button.SetActive(true);
        }
        else
        {
            skipToPhase3Button.SetActive(false);
        }

        mainMenu.SetActive(true);

        float highScore = PlayerPrefs.GetInt("goonGameHighScore", 0);
        if (highScore > 0)
        {
            goonGameHighScore.GetComponent<TextMeshProUGUI>().text = highScore.ToString();
            goonGameHighScore.SetActive(true);
        }
        else
        {
            goonGameHighScore.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
