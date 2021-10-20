//////////////////////////////////////////////
//Assignment/Lab/Project: Jumpy Street
//Name: Brennan Sullivan & Jacob Coleman
//Section: 2021FA.SGD.285.2141
//Instructor: Aurore Wold
//Date: 9/13/2021
/////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    public static GameMenuController gMC;
    public bool paused;

    private int playerScoreNumber;
    public int playerScore {
        set { playerScoreNumber = value;
            menuScoreText.text = playerScoreNumber.ToString();
            gameScoreText.text = playerScoreNumber.ToString();
            finalScoreText.text = playerScoreNumber.ToString();
        }
        get {return playerScoreNumber;}
    }

    private AudioSource soundPlayer;
    private AudioClip menuBack;
    private AudioClip menuForward;
    private AudioClip menuSelect;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text menuScoreText;
    [SerializeField] private Text gameScoreText;
    [SerializeField] private Text finalScoreText;


    private void Awake() {
        gMC = this;
        paused = false;
        SetUpAudio();
        SetUpMenu();
        playerScore = 0;

    }

    private void Update() {
        if (!losePanel.activeInHierarchy) {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                OnEscapeKeyPress();
            }
        }
    }

    //This should make sure the menu sound gets to play before the scene is changed.
    public void OnMenuButtonClick() {
        StartCoroutine(MenuButtonSound());
    }
    private IEnumerator MenuButtonSound() {
        soundPlayer.PlayOneShot(menuForward);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    // Similar to the menu button.
    public void OnQuitButtonClick() {
        StartCoroutine(QuitButtonSound());
    }
    private IEnumerator QuitButtonSound() {
        soundPlayer.PlayOneShot(menuBack);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
        yield return null;
    }

    public void OnResumeuttonClick() {
        paused = false;
        soundPlayer.PlayOneShot(menuForward);
        SetUpMenu();
        //Call for resume game.
    }

    private void SetUpAudio() {
        soundPlayer = this.GetComponent<AudioSource>();
        menuBack = Resources.Load<AudioClip>("SFX/menuBack");
        menuForward = Resources.Load<AudioClip>("SFX/menuValidate");
        menuSelect = Resources.Load<AudioClip>("SFX/menuSelect");
    }

    public void SetUpMenu() {
        pausePanel.SetActive(false);
        scorePanel.SetActive(true);
        losePanel.SetActive(false);
    }
    public void LoseGame()
    {
        paused = true;
        pausePanel.SetActive(false);
        scorePanel.SetActive(false);
        losePanel.SetActive(true);
        finalScoreText.text = playerScore.ToString();
    }

    public void OnEscapeKeyPress() {
        paused = true;
        soundPlayer.PlayOneShot(menuSelect);
        pausePanel.SetActive(true);
        scorePanel.SetActive(false);
        menuScoreText.text = playerScore.ToString();

        int score = playerScore;
        int highscore = PlayerPrefs.GetInt("highscore");
        PlayerPrefs.SetInt("score", score);
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        //Call for pause game.
    }
}
