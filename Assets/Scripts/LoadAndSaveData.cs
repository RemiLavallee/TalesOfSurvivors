using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("2 instance");
            return;
        }

        instance = this;
    }

    void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);
        PlayerPrefs.SetInt("health", CharachterStat.instance.health);
        PlayerPrefs.SetInt("attack", CharachterStat.instance.attack);
        PlayerPrefs.SetInt("def", CharachterStat.instance.def);
        PlayerPrefs.SetInt("speed", CharachterStat.instance.speed);
        PlayerPrefs.SetInt("magnet", CharachterStat.instance.magnet);
    }

    public void LoadData()
    {
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUi();
        CharachterStat.instance.health = PlayerPrefs.GetInt("health", 0);
        CharachterStat.instance.attack = PlayerPrefs.GetInt("attack", 0);
        CharachterStat.instance.def = PlayerPrefs.GetInt("def", 0);
        CharachterStat.instance.speed = PlayerPrefs.GetInt("speed", 0);
        CharachterStat.instance.magnet = PlayerPrefs.GetInt("magnet", 0);
    }
}
