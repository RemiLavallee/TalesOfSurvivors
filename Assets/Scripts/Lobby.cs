using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] private AudioSource lobbySound;
    public void Play()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void Start()
    {
        lobbySound.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
