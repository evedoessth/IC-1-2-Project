using System.Collections.Generic;
using UnityEngine;

public class Plaza : MonoBehaviour {
    [SerializeField] private Transform wayPoint;
    private List<Vector3> cardinalWayPoints = new List<Vector3>();

    private void Start()
    {
        cardinalWayPoints.Clear();
        for (int i = 0; i < 7; i++)
        {
            cardinalWayPoints.Add(wayPoint.GetChild(i).gameObject.transform.position);
        }
    }
}