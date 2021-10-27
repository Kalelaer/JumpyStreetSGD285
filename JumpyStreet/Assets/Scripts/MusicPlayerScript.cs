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

public class MusicPlayerScript : MonoBehaviour
{
    private AudioClip music;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        music = Resources.Load<AudioClip>("SFX/music");
        this.gameObject.GetComponent<AudioSource>().clip = music;
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
