using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oy : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
       
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + Vector3.up * 5);
    }
}
