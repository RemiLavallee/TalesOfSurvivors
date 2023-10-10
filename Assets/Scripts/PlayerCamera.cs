using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void LateUpdate()
    {
        var position = player.transform.position;
        position.z = -10;
        transform.position = position;
    }
}
