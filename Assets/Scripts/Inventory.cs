using TMPro;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public static Inventory instance;
    public TextMeshProUGUI coinsCountText;

    private void Awake()
    {
        instance = this;
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }
}
