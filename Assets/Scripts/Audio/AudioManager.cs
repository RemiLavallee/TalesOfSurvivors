using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")] 
    [SerializeField] private AudioSource musicSound;
    [SerializeField] private AudioSource gameSound;

    [Header("Audio Clip")] 
    public AudioClip xp;
    public AudioClip spinAttack;
    public AudioClip bomb;
    public AudioClip mainAttack;
    public AudioClip levelUp;

    private void Start()
    {
        musicSound.Play();
    }

    public void PlayGameSound(AudioClip clip)
    {
        gameSound.PlayOneShot(clip);
    }
}
