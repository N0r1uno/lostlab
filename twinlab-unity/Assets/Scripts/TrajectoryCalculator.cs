using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryCalculator : MonoBehaviour
{
    public LineRenderer lr;

    public float velocity;
    public float angle;
    public int resolution;

    float g;
    float radianAngle;

    private void Awake()
    {
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    public void RenderArc()
    {
        lr.positionCount = (resolution + 1);
        lr.SetPositions(CalculateArcArray());
    }

    private Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;
        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    private Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }
}
