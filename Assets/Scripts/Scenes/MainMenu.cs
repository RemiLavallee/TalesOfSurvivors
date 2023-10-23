using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource menuSound;
    public void Play()
    {
        SceneManager.LoadSceneAsync("LobbyMenu");
    }

    public void Start()
    {
        menuSound.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
