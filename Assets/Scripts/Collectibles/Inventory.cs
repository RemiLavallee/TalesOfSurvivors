using System;
using TMPro;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public static Inventory instance;
    public TextMeshProUGUI[] coinsCountText;
    [SerializeField] private TextMeshProUGUI textKill;
    private EnemySpawner sa;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sa = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        textKill.text = sa.enemyKill.ToString();
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
