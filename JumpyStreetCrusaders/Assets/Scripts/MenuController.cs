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

public class MenuController : MonoBehaviour
{
    public AudioSource soundPlayer;
    private AudioClip menuBack;
    private AudioClip menuForward;
    public AudioClip menuSelect;
    [SerializeField] private string nextLevel;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject characterSelectPanel;

    [Header("Score")]
    [SerializeField] private Text highscoreText;
    [SerializeField] private int highscore;

    private void Awake() {
        SetUpAudio();
        SetUpMenu();

        if (PlayerPrefs.GetInt("highscore") < 0)
        {
            PlayerPrefs.SetInt("highscore", 0);
        }
        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = $"{highscore}";
    }

    //This should make sure the menu sound gets to play before the scene is changed.
    public void OnPlayButtonClick() {
        StartCoroutine(PlayButtonSound());
    }

    public void OnCharacterSelectButtonClick()
    {
        soundPlayer.PlayOneShot(menuForward);
        menuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }
    private IEnumerator PlayButtonSound() {
        soundPlayer.PlayOneShot(menuForward);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextLevel);
        yield return null;
    }
    
    // Similar to the play button.
    public void OnQuitButtonClick() {
        StartCoroutine(QuitButtonSound());
    }
    private IEnumerator QuitButtonSound() {
        soundPlayer.PlayOneShot(menuBack);
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
        yield return null;
    }

    public void OnHelpButtonClick() {
        soundPlayer.PlayOneShot(menuSelect);
        menuPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void OnCreditsButtonClick() {
        soundPlayer.PlayOneShot(menuSelect);
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OnBackButtonClick() {
        soundPlayer.PlayOneShot(menuBack);
        SetUpMenu();
    }

    private void SetUpAudio() {
        soundPlayer = this.GetComponent<AudioSource>();
        menuBack = Resources.Load<AudioClip>("SFX/menuBack");
        menuForward = Resources.Load<AudioClip>("SFX/menuValidate");
        menuSelect = Resources.Load<AudioClip>("SFX/menuSelect");
    }

    public void SetUpMenu() {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        helpPanel.SetActive(false);
        characterSelectPanel.SetActive(false);
    }

    private void OnValidate()
    {
        print("cleared stuff up");
        if (!Application.IsPlaying(this.gameObject))
        {
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }
}
