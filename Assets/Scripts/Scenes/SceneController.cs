using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("MainLevel", LoadSceneMode.Additive);
    }
}
