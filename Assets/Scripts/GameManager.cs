using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     [SerializeField] private AudioSource gameSound;
     public void Start()
     {
          gameSound.Play();
          Time.timeScale = 1;
     }
}
