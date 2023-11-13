using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLimiter : MonoBehaviour
{
    [SerializeField] private float minY = -1f, maxY = 1f;
    [SerializeField] private float minX, maxX;
    [SerializeField]private Transform target;
    [SerializeField] private Camera camera;

    private void Awake()
    {
        FindCameraInScene("MainLevel");
        
        var pl = FindObjectOfType<Player>();
        if (pl != null)
        {
            target = pl.transform;
        }
    }
    
    private void LateUpdate()
    {
        var xPosition = target.position.x;

        if (!(minX == 0 && maxX == 0))
        {
           xPosition = Mathf.Clamp(target.position.x, minX, maxX);
        }
        
        var yPosition = Mathf.Clamp(target.position.y, minY, maxY);

        camera.transform.position = new Vector3(xPosition, yPosition, -10f);
    }
    
    void FindCameraInScene(string sceneName)
    {
        var scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            foreach (var obj in scene.GetRootGameObjects())
            {
                var cam = obj.GetComponentInChildren<Camera>();
                if (cam != null)
                {
                    camera = cam;
                    break;
                }
            }
        }
    }
}

