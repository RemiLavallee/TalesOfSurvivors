using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    private EnemySpawner enemyS;
    [SerializeField] private TextMeshProUGUI[] textKill;

    private void Start()
    {
        enemyS = FindObjectOfType<EnemySpawner>();
    }

    private void Update()
    {
        foreach (var text in textKill)
        {
            text.text = enemyS.enemyKill.ToString();
        }
    }
}