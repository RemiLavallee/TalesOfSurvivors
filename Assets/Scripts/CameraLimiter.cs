using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
    public float minY = -1f, maxY = 1f;
    public float minX, maxX;
    public Transform target;

    private void LateUpdate()
    {
        var xPosition = Mathf.Clamp(target.position.x, minX, maxX);

        var yPosition = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(xPosition, yPosition, -10f);
    }
}

