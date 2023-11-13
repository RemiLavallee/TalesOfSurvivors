using UnityEngine;

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
        var musicObject = GameObject.FindGameObjectWithTag("LevelMusic");
        if (musicObject != null)
        {
            musicSound = musicObject.GetComponent<AudioSource>();
            if (musicSound != null)
            {
                musicSound.Play();
            }
        }
    }

    public void PlayGameSound(AudioClip clip)
    {
        gameSound.PlayOneShot(clip);
    }
}
