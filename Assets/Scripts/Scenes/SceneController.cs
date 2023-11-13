using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameManager.sceneToLoad = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("MainLevel");
        }
    }
}
