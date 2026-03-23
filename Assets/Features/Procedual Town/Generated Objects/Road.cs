using System.Collections.Generic;
using UnityEngine;

public class Road {
    private Vector3 startPoint;
    private Vector3 endPoint;

    private List<Vector3> pivotPoints;

    public float length {get; private set;}

    public Road(Vector3 startPoint, Vector3 endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        length = Vector3.Distance(this.endPoint, this.startPoint);
    }

    public Vector3 GetStartPoint()
    {
        return startPoint;
    }

    public Vector3 GetEndPoint()
    {
        return endPoint;
    }
}