using System;
using TMPro;
using UnityEngine;

public class TextColorFlashEffect : MonoBehaviour
{
    [Serializable]
    public class TextColorFlash
    {
        public TextMeshProUGUI text;
        public Color colorStart;
        public Color colorEnd ;
        public float flashDuration = 10f;
    }

    private float timer;
    public TextColorFlash flashSettings;

    private void Update()
    {
        timer += Time.unscaledDeltaTime;

        var lerp = Mathf.PingPong(timer, flashSettings.flashDuration) / flashSettings.flashDuration;

        flashSettings.text.color = Color.Lerp(flashSettings.colorStart, flashSettings.colorEnd, lerp);
        
        flashSettings.text.ForceMeshUpdate();
    }
}
