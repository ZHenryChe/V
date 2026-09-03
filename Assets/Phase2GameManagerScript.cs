using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase2GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject leftButton;
    public GameObject rightButton;
    public TextMeshPro question;
    public ChoiceImageSquareScript choiceImageSquareLeft;
    public ChoiceImageSquareScript choiceImageSquareRight;
    public AudioHandlerScript audioHandler;

    public AudioSource chalk;
    public AudioSource staticNoise;

    public class Question
    {
        public string questionText;
        public List<string> options;
        public int correctOptionIndex;
        public string audioTag;

        public Question(string questionText, List<string> options, int correctOptionIndex, string tag)
        {
            this.questionText = questionText;
            this.options = options;
            this.correctOptionIndex = correctOptionIndex;
            this.audioTag = tag;    // Audio: Edison-1, Edison0, etc...
        }
    }

    public void RoundSetUp()
    {
        player.transform.position = new Vector3(0, 0, -5); // Reset player position
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero; // Reset player velocity
        leftButton.transform.position = new Vector3(-70, -30, -1); // Reset left button position
        rightButton.transform.position = new Vector3(70, -30, -1); // Reset right button position
        leftButton.GetComponent<GameButtonScript>().buttonPress = true; // Reset button state
        rightButton.GetComponent<GameButtonScript>().buttonPress = true; // Reset button state

        question.text = "";
        choiceImageSquareLeft.textVer.text= "";
        choiceImageSquareRight.textVer.text = "";
    }

    public void LoadRound()
    {
        chalk.Play();
        question.text = questionList[gameRound].questionText;
        choiceImageSquareLeft.textVer.text = questionList[gameRound].options[0];
        choiceImageSquareRight.textVer.text = questionList[gameRound].options[1];
        //choiceImageSquareLeft.imageName = questionList[gameRound].options[0];
        //choiceImageSquareRight.imageName = questionList[gameRound].options[1];
        leftButton.GetComponent<GameButtonScript>().buttonID = (0 == questionList[gameRound].correctOptionIndex) ? 1 : 0;
        rightButton.GetComponent<GameButtonScript>().buttonID = (1 == questionList[gameRound].correctOptionIndex) ? 1 : 0;
    }

    public IEnumerator ProcessAnswer(int buttonID)
    {
        if (gameState[gameRound] == -1) // If not answered yet
        {
            if (buttonID == 1 && gameRound < questionList.Count)
            {
                gameState[gameRound] = 1; // Correct
                yield return StartCoroutine(audioHandler.NarrateAudioAndWait(questionList[gameRound].audioTag + "1"));
            }
            else
            {
                gameState[gameRound] = 0; // Incorrect
                yield return StartCoroutine(audioHandler.NarrateAudioAndWait(questionList[gameRound].audioTag + "0"));
            }
            RoundSetUp(); // Reset positions for next round
            gameRound++;
            if (gameRound >= questionList.Count)
            {
                yield return StartCoroutine(audioHandler.NarrateAudioAndWait("Phase2Outro"));
                SceneManager.LoadScene("Phase2.5");
            }
            else
            {
                yield return StartCoroutine(audioHandler.NarrateAudioAndWait(questionList[gameRound].audioTag + "-1"));
                LoadRound(); // Load next question
            }


        }
    }

    public readonly bool gameOver = false;

    public int gameRound = 0;
    public Dictionary<int, int> gameState = new Dictionary<int, int>
    // -1: Not answered, 0: Incorrect, 1: Correct
    {
        { 0, -1 },
        { 1, -1 },
        { 2, -1 },
        { 3, -1 },
        { 4, -1 },
        { 5, -1 },
        { 6, -1 },
        { 7, -1 },
        { 8, -1 },
        { 9, -1 }
    };

    // For random, shuffle list and add critical questions at end
    List<Question> questionList = new List<Question> {
        new Question("What is the capital of Canada?", new List<string> { "Toronto", "Ottawa"}, 1, "Geography"),
        new Question("What is the square root of 64?", new List<string> { "6", "8"}, 1, "Math"),
        new Question("What is Game Theory?", new List<string> { "The study of mathematical models of strategic interaction among rational decision-makers.", "FNAF"}, 0, "Social"),
        new Question("How do you spell 'accomodate'?", new List<string> { "accomodate", "accommodate"}, 1, "Spelling"),
        new Question("Who invented the lightbulb?", new List<string> { "Thomas Edison", "Nikola Tesla"}, 0, "Lightbulb"),
        new Question("What is the best way to tame a human?", new List<string> { "Through their stomach", "Through their ribcage"}, 1, "HumanTame"),
        new Question("Why did the other tomatoes already get their skin suit and not you?", new List<string> { "Because they are better than me", "Because I don't put in the effort"}, 1, "Others"),
        new Question("Why are you still stuck here?", new List<string> { "Because I am a failure", "Sorry"}, 0, "Failure"),
        new Question("What will you do tomorrow?", new List<string> { "I want to...", "Work harder to boost my CV and get a skin suit"}, 1, "Tomorrow"),
        new Question("How will you become a successful tomato?", new List<string> { "By becoming someone like Dokibird", "By becoming someone like Dokibird"}, 0, "SuccessMeaning"),
        };

    IEnumerator Intro()
    {
        RoundSetUp();
        yield return StartCoroutine(audioHandler.NarrateAudioAndWait("Phase2Intro"));
        LoadRound();
        yield return StartCoroutine(audioHandler.NarrateAudioAndWait(questionList[gameRound].audioTag + "-1"));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameRound = 0;
        Debug.Log("Gameround" + gameRound);
        StartCoroutine(Intro());
        staticNoise.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRound >= 6)
        {
            if (!staticNoise.isPlaying)
            {
                staticNoise.Play();
            }

            float maxVolume = (gameRound - 5) * 0.01f;

            if (staticNoise.volume < maxVolume)
            {
                staticNoise.volume += 0.00001f;
            }
            else
            {
                staticNoise.volume = maxVolume;
            }
        }
    }
}
