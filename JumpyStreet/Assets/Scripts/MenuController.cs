using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private AudioSource soundPlayer;
    private AudioClip menuBack;
    private AudioClip menuForward;
    private AudioClip menuSelect;
    [SerializeField] private string nextLevel;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject creditsPanel;

    
    private void Awake() {
        SetUpAudio();
        SetUpMenu();
    }

    //This should make sure the menu sound gets to play before the scene is changed.
    public void OnPlayButtonClick() {
        StartCoroutine(PlayButtonSound());
    }
    private IEnumerator PlayButtonSound() {
        soundPlayer.PlayOneShot(menuForward);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(nextLevel);
        yield return null;
    }
    
    // Similar to the play button.
    public void OnQuitButtonClick() {
        StartCoroutine(QuitButtonSound());
    }
    private IEnumerator QuitButtonSound() {
        soundPlayer.PlayOneShot(menuBack);
        yield return new WaitForSeconds(0.25f);
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
    }

}
