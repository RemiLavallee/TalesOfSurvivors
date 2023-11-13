using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     internal static string sceneToLoad;

     private void Awake()
     {
          SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
     }
     private void Start()
     {
          Time.timeScale = 1f;
     }
}
