using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseUI;
    public static bool gamePause = false;
    public GameObject optionsUI;
    public TextMeshProUGUI gamePaused;
    public Slider musicSlider;
    public Slider gameSoundSlider;
    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            AdjustMusicVolume();
            AdjustGameSoundVolume();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }
    }

    void Paused()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        gamePause = true;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        gamePause = false;
 
    }

    public void Confirm()
    {
        optionsUI.SetActive(false);
        gamePaused.enabled = true;
    }

    public void Options()
    {
        gamePaused.enabled = false;
        optionsUI.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("LobbyMenu");
    }
    
    public void AdjustMusicVolume()
    {
        var volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void AdjustGameSoundVolume()
    {
        var volume = gameSoundSlider.value;
        mixer.SetFloat("GameSound", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("gameSoundVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        gameSoundSlider.value = PlayerPrefs.GetFloat("gameSoundVolume");
        AdjustMusicVolume();
    }
    
    
}
