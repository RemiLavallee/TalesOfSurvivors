using TMPro;
using UnityEngine;

public class TextPulseEffect : MonoBehaviour
{

    public TextMeshProUGUI text;
    public float minScale;
    public float maxScale;
    public float speed;

    private Vector3 startingScale;
    private float currentScale;

    private void Start()
    {
        startingScale = transform.localScale;
        currentScale = minScale;
    }

    private void Update()
    {
        currentScale = Mathf.PingPong(Time.unscaledTime * speed, maxScale - minScale) + minScale;
        transform.localScale = startingScale * currentScale;
    }
}
