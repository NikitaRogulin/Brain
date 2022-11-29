using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField,Range(0,0.3F)] private float accuracy;
    [SerializeField] private bool clockwise;

    public Vector3 FindWay(Vector3 position)
    {
        for (int i = 0; i < points.Count; i++)
        {
            var stPoint = points[i].position;
            Vector3 ndPoint = points[0].position;
            if (i+1 < points.Count)
                ndPoint = points[i + 1].position;

            if(Vector3.Distance(stPoint, position) + Vector3.Distance(ndPoint, position) - Vector3.Distance(stPoint, ndPoint) <= accuracy)
            {
                var direction = (ndPoint - stPoint).normalized;
                if (clockwise)
                    return direction;
                else
                    return -direction;
            }
        }
        return BackOnPath(position);
    }
    private Vector3 BackOnPath(Vector3 position)
    {
        Vector3 nearestPoint = FindNearestPoint(position);;
        return nearestPoint - position;
    }
    private Vector3 FindNearestPoint(Vector3 position)
    {
        Vector3 nearestPoint = points[0].position;
        float minDistance = Vector3.Distance(points[0].position, position);
        foreach (var point in points)
        {
            var distance = Vector3.Distance(point.position, position);
            if(distance< minDistance)
            {
                nearestPoint = point.position;
                minDistance = distance;
            }
        }
        return nearestPoint;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            var stPoint = points[i].position;
            Vector3 ndPoint = points[0].position;
            if (i + 1 < points.Count)
                ndPoint = points[i + 1].position;

            Debug.DrawLine(stPoint, ndPoint, Color.green);
        }
    }
}
