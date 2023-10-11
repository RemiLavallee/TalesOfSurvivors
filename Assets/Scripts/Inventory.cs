using TMPro;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public static Inventory instance;
    public TextMeshProUGUI[] coinsCountText;

    private void Awake()
    {
        instance = this;
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        UpdateTextUi();
    }
    
    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        UpdateTextUi();
    }

    public void UpdateTextUi()
    {
        for (var i = 0; i < coinsCountText.Length; i++)
        {
            coinsCountText[i].text = coinsCount.ToString();
        }
    }
}
