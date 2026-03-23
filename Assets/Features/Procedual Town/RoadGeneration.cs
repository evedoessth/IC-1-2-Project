using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class RoadGeneration : MonoBehaviour {
    
    //[SerializeField] private Transform parent;g
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Material roadMaterial;



    public Road CreateRoad(Vector3 startPos, Vector3 goalPos, bool hasDebugState, Transform parent)
    {
        if (hasDebugState)  
        {
            CreateSphereAtPoint(startPrefab, startPos, parent);
            CreateSphereAtPoint(goalPrefab, goalPos, parent);
    
        }
        Road road = new Road(startPos, goalPos);
        CreateConnectingLine(road.GetStartPoint(), road.GetEndPoint(), parent);
        
        return road;
    }

    private void CreateConnectingLine(Vector3 startPos, Vector3 goalPos, Transform parent)
    {
        GameObject line = new GameObject("Line");
        line.transform.SetParent(parent);
        line.transform.localScale = new Vector3(1, 1, 1);
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;
        lineRenderer.materials[0] = roadMaterial;
        lineRenderer.material = roadMaterial;

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, startPos); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, goalPos);
    }

    void CreateSphereAtPoint(GameObject prefab, Vector3 startPos, Transform parent)
    {
        GameObject sphere =  Instantiate(prefab, parent);
        sphere.transform.position = startPos;
    }
}