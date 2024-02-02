using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDebugLine : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
  
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + Vector3.right * 100);
    }
}
