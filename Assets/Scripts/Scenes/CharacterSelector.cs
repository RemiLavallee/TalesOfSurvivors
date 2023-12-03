using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;
    [SerializeField] private List<CharacterScriptableObject> characters = new List<CharacterScriptableObject>();
    private int currentIndex = 0; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SelectCharacter(characters[0]);
    }

    public static CharacterScriptableObject GetData()
    {
        return instance.characterData;
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
        characterData = character;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
    
    public void NextCharacter()
    {
        if (characters.Count == 0) return;

        currentIndex++;
        if (currentIndex >= characters.Count) currentIndex = 1;

        SelectCharacter(characters[currentIndex]);
    }
    
    public void PreviousCharacter()
    {
        if (characters.Count == 0) return;

        currentIndex--;
        if (currentIndex <= characters.Count) currentIndex = 0;

        SelectCharacter(characters[currentIndex]);
    }
}
