using System.Collections;
using UnityEngine;

public class BrezierAttackBehaviour : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private int segments = 5;
    [SerializeField] private Transform point0, point1, point2, point3;
    [SerializeField] private int numPoints = 12;
    private bool isBezierMode = true;
    [SerializeField] private Vector3 offsetPoint1 = new Vector3(-1f, 1f, 0f);
    [SerializeField] private Vector3 offsetPoint2 = new Vector3(1f, -1f, 0f);
    [SerializeField] private Transform player;
    [SerializeField] private float movementSpeed = 1f;
    private float tParam = 0f;
    private bool coroutineAllowed;
    [SerializeField] private SkillScriptableObject weaponData;
    [SerializeField] private float destroyDelay;
    private float currentDamage;
    private Vector3 lastMousePosition;
    private bool updateControlPoints = true;
    private Vector3 lastPoint1Position;
    private Vector3 lastPoint2Position;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = isBezierMode ? numPoints : segments;
        coroutineAllowed = true;
        currentDamage = weaponData.Damage;
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10f;
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        if (updateControlPoints)
        {
            lastMousePosition = worldPosition;
            lastPoint1Position = worldPosition + offsetPoint1;
            lastPoint2Position = worldPosition + offsetPoint2;
        }

        lineRenderer.SetPosition(0, transform.position);

        if (coroutineAllowed)
        {
            lineRenderer.SetPosition(1, worldPosition);
            updateControlPoints = true;
        }
        else
        {
            lineRenderer.SetPosition(1, lastMousePosition);
            updateControlPoints = false;
            point1.position = lastPoint1Position;
            point2.position = lastPoint2Position;
        }


        if (Input.GetKeyDown(KeyCode.Z)) isBezierMode = true;
        if (Input.GetKeyDown(KeyCode.X)) isBezierMode = false;


        if (isBezierMode)
        {
            point0.position = player.position;
            point3.position = coroutineAllowed ? worldPosition : lastMousePosition;

            DrawBezierCurve();
        }


        if (coroutineAllowed)
            StartCoroutine(FollowBezierCurve());

        Destroy(gameObject, destroyDelay);
    }

    private void DrawBezierCurve()
    {
        for (var i = 0; i < numPoints; i++)
        {
            var t = i / (float)numPoints;
            lineRenderer.SetPosition(i,
                CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position));
        }
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        var u = 1 - t;
        var tt = t * t;
        var uu = u * u;
        var uuu = uu * u;
        var ttt = tt * t;

        var p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    private IEnumerator FollowBezierCurve()
    {
        coroutineAllowed = false;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * movementSpeed;

            Vector3 gameObjectPosition = CalculateBezierPoint(tParam, point0.position, point1.position, point2.position,
                point3.position);
            transform.position = gameObjectPosition;

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        coroutineAllowed = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}