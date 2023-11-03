using System.Collections;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    [SerializeField] private EnemySkillScriptable skillData;
    [SerializeField] private GameObject attackPrefab;
    private GameObject player;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(AttackCoroutine());

    }
    
    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                DistanceAttack();
                yield return new WaitForSeconds(0.2f);
            }
            
            yield return new WaitForSeconds(skillData.CooldownDuration);
        }
    }


    private void DistanceAttack()
    {
        var playerPosition = player.transform.position;
        var distanceToPlayer = Vector3.Distance(transform.position, playerPosition); 

        if (distanceToPlayer <= skillData.DistanceToPlayer)
        {
            var position = transform.position;
            var attackDirection = playerPosition - position;
            attackDirection.Normalize();

            var distanceAttack = Instantiate(attackPrefab, position, Quaternion.identity);
            distanceAttack.transform.SetParent(transform);
            distanceAttack.transform.right = attackDirection;
        }
    }
}

