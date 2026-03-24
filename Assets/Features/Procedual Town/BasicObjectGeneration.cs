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
    [SerializeField] private Transform lSystemParent;
    [Header("Prefabs")]
    [SerializeField] private GameObject house;
    [SerializeField] private GameObject plazaPrefab;
    [SerializeField] private cardinalDirection cardinal = cardinalDirection.North;
    //private static readonly string objectPrefabLocation = "Map Object/";

    private VectorCalculation vectorCalculation = new VectorCalculation();
    [SerializeField] private GenerationSettings generationSettings;
    [SerializeField] private RoadGeneration roadGeneration;

    private int maxGenerationDistance = 30;
    private int generationBufferLength = 10;
    private int maxHouseGenerationDistance;
    private int HouseAmount = 4;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generateObjectsWIP();
    }

    public void generateObjectsWIP()
    {
        int townSize = generationSettings.townSize;
        maxHouseGenerationDistance = maxGenerationDistance - generationBufferLength;
        //CreateRandomHouses();
        bool hasEntryRoad = true;
        int[] houseAtPlaza = { 1, 0, 1, 0, 0, 0, 1, 1 };
        CreatePlaza(hasEntryRoad, cardinal, houseAtPlaza);

        Vector3 startPos = vectorCalculation.GetStartPosAtEdge(cardinalDirection.East, maxHouseGenerationDistance, maxHouseGenerationDistance, maxGenerationDistance);
        Vector3 goalPos = vectorCalculation.GetRandomVectorInRange(5);
        //Road road = CreateRoad(startPos, goalPos);
        int Distance = 5 * 10;
        //GameObject house = CreateHouseOnRoad(startPos, goalPos, true, Random.Range(0.1f, 0.2f), Distance, lSystemParent, HouseType.Work);
    }

    //Make this into a return thing for the GameObject so you can use inside information… May be even make plaza a individual type 
    private void CreatePlaza(bool hasEntryRoad, cardinalDirection cardinal, int[] houseAtPoints)
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
            if (plazaDirection == 1) // && wayPoint.transform.GetChild(i).gameObject.transform.position != plazaPointPos
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

    public GameObject CreateHouseOnRoad(Vector3 startPos, Vector3 goalPos, bool isOnRightSide, float distanceOnLine, int distance, Transform parent, HouseType houseType = HouseType.Home)
    {
        Vector3 roadPoint = Vector3.Lerp(goalPos, startPos, distanceOnLine);
        Vector3 objectPoint = vectorCalculation.CalculatePointPerpendicularToLine(startPos, goalPos, roadPoint, distance, isOnRightSide);
        /*CreateSphereAtPoint(wayPrefab, parent, roadPoint);
        CreateSphereAtPoint(wayPrefab, parent, objectPoint);*/
        Quaternion objectRot = Quaternion.LookRotation(roadPoint - objectPoint);
        GameObject roadHouse = CreateHouse(objectPoint, objectRot, parent, houseType);
        return roadHouse;
    }

    private GameObject CreateHouse(Vector3 objectPoint, Quaternion objectRot, Transform parent, HouseType houseType = HouseType.Home)
    {
        GameObject houseObj = Instantiate(house, objectPoint, objectRot, parent);
        houseObj.GetComponent<House>().SetHouseType(houseType);
        return houseObj;
    }

    private GameObject CreateHouseAtEndOfRoad(Vector3 startPos, Vector3 goalPos, float distance, Transform parent, HouseType houseType)
    {
        Vector3 objectPoint = vectorCalculation.CalculatePointAtEndOfLine(startPos,goalPos, distance);
        Quaternion objectRot = Quaternion.LookRotation(startPos - goalPos);
        GameObject endRoadHouse = CreateHouse(objectPoint, objectRot, parent, houseType);
        return endRoadHouse;
    }
    

    private void CreateSphereAtPoint(GameObject prefab, Transform parent, Vector3 position)
    {
        GameObject sphere =  Instantiate(prefab, parent);
        sphere.transform.position = position;
    }

    private Road CreateRoad(Vector3 startPos, Vector3 goalPos)
    {
        return roadGeneration.CreateRoad(startPos, goalPos, lSystemParent, true);
    }

    private void CreateRandomHouses()
    {
        for (int i = 0; i < HouseAmount; i++)
        {
            Vector3 randomPos = vectorCalculation.GetRandomVectorInRange(maxHouseGenerationDistance);
            Vector3 randomRot = new Vector3(0.0f, Random.Range(0.0f, 360.0f));
            GameObject h = Instantiate(house, randomPos, Quaternion.Euler(randomRot), lSystemParent);
        }
    }

}
