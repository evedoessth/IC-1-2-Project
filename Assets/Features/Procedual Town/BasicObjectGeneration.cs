using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum cardinalDirection
{
    North,
    East,
    South,
    West
}

public class BasicObjectGeneration : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject house;
    private VectorCalculation vectorCalculation = new VectorCalculation();

    
    public GameObject CreateHouseOnRoad(Vector3 startPos, Vector3 goalPos, bool isOnRightSide, float distanceOnLine, int distance, Transform parent, HouseType houseType = HouseType.Home)
    {
        Vector3 roadPoint = Vector3.Lerp(goalPos, startPos, distanceOnLine);
        Vector3 objectPoint = vectorCalculation.CalculatePointPerpendicularToLine(startPos, goalPos, roadPoint, distance, isOnRightSide);
        Quaternion objectRot = Quaternion.LookRotation(roadPoint - objectPoint);
        GameObject roadHouse = CreateHouse(objectPoint, objectRot, parent, houseType);
        return roadHouse;
    }

    private GameObject CreateHouseAtEndOfRoad(Vector3 startPos, Vector3 goalPos, float distance, Transform parent, HouseType houseType)
    {
        Vector3 objectPoint = vectorCalculation.CalculatePointAtEndOfLine(startPos,goalPos, distance);
        Quaternion objectRot = Quaternion.LookRotation(startPos - goalPos);
        GameObject endRoadHouse = CreateHouse(objectPoint, objectRot, parent, houseType);
        return endRoadHouse;
    }
    
    private GameObject CreateHouse(Vector3 objectPoint, Quaternion objectRot, Transform parent, HouseType houseType = HouseType.Home)
    {
        GameObject houseObj = Instantiate(house, objectPoint, objectRot, parent);
        houseObj.layer = 3;
        houseObj.GetComponent<House>().SetHouseType(houseType);
        return houseObj;
    }

    private void CreateSphereAtPoint(GameObject prefab, Transform parent, Vector3 position)
    {
        GameObject sphere =  Instantiate(prefab, parent);
        sphere.transform.position = position;
    }


}
/*private void CreatePlaza(bool hasEntryRoad, cardinalDirection cardinal, int[] houseAtPoints)
    {
        GameObject plaza = Instantiate(plazaPrefab, vectorCalculation.GetRandomVectorInRange(5), Quaternion.identity, lSystemParent);

        GameObject wayPoint = plaza.transform.GetChild(1).gameObject;
        if (hasEntryRoad)
        {
            int directionalPointIndex = GetPlazaPosFromDirection(cardinal);
            Vector3 plazaPointPos = wayPoint.transform.GetChild(directionalPointIndex).gameObject.transform.position;
            Vector3 edgePos = vectorCalculation.GetStartPosAtEdge(cardinal, maxHouseGenerationDistance, maxHouseGenerationDistance, maxGenerationDistance);
            Road road = CreateRoad(edgePos, plazaPointPos);
        }

        for (int i = 0; i < houseAtPoints.Length; i++)
        {
            int plazaDirection = houseAtPoints[i];
            if (plazaDirection == 1)
            {
                GameObject house = CreateHouseAtEndOfRoad(plaza.transform.position, wayPoint.transform.GetChild(i).gameObject.transform.position, 1.1f, lSystemParent, HouseType.Home);
            }
        }
    }

    private int GetPlazaPosFromDirection(cardinalDirection direction)
    {
        switch(direction)
        {
            case cardinalDirection.West:
            return GetRandomIntOfThree(5,6,7);

            case cardinalDirection.North:
            return GetRandomIntOfThree(7,0,1);

            case cardinalDirection.East:
            return GetRandomIntOfThree(1,2,3);
            
            case cardinalDirection.South:
            return GetRandomIntOfThree(3,4,5);
            default:
            return 0;
        }
    }
    private int GetRandomIntOfThree(int v1, int v2, int v3)
    {
        int randomResult = Random.Range(0,3);
        return randomResult switch
        {
            0 => v1,
            1 => v2,
            2 => v3,
            _ => v2,
        };
    }
 */