using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController mc;
    [SerializeField] private GameObject targetMap;

    private void Start()
    {
        mc = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            mc.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (mc.currentChunk == targetMap)
        {
            mc.currentChunk = null;
        }
    }
}
