using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProceduralTown
{
    public class SimpleVisualizer : MonoBehaviour
    {
        List<Vector3> positions = new List<Vector3>();
        List<GameObject> houses = new List<GameObject>();
        List<Road> roads = new List<Road>();
        [SerializeField] private GameObject wayPoint;
        [SerializeField] private Transform lSystemParent;
        [SerializeField] private RoadGeneration roadGeneration;
        [SerializeField] private BasicObjectGeneration objectGeneration;
        public bool hasRandomRotation = false;
        

        private readonly static int defaultLength = 7;
        private int length = defaultLength;
        private float angle = 85;

        public int Length
        {
            get
            {
                if (length > 0)
                {
                    return length;
                }
                else
                {
                    return 1;
                }
            }
            set => length = value;
        }

        public void RunSimpleVisualizer(string sequence)
        {
            length = defaultLength;
            ClearParentObject(lSystemParent);
            
            VisualizeSequence(sequence);
            GenerateHousesOnRoads();
            CheckCorrectGeneration(roads, houses);
        }

        private void ClearParentObject(Transform parent)
        {
            var children = new List<GameObject>();
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                children.Add(parent.GetChild(i).gameObject);
            }
            children.ForEach(child => Destroy(child));
        }

        private void VisualizeSequence(string sequence)
        {
            Stack<AgentParameters> savePoints = new Stack<AgentParameters>();
            positions.Clear();
            roads.Clear();
            houses.Clear();
            
            var currentPos = Vector3.zero;

            Vector3 direction = Vector3.forward;
            Vector3 tempPos = Vector3.zero;

            positions.Add(currentPos);

            foreach (var letter in sequence)
            {
                if (hasRandomRotation)
                {
                    angle = Random.Range(70.0f, 90.0f);
                }
                EncodingLetters encoding = (EncodingLetters)letter;
                switch (encoding)
                {
                    case EncodingLetters.save:
                        savePoints.Push(new AgentParameters
                        {
                            position = currentPos,
                            direction = direction,
                            length = Length
                        });
                        break;
                    case EncodingLetters.load:
                        if (savePoints.Count > 0)
                        {
                            var agentParameter = savePoints.Pop();
                            currentPos = agentParameter.position;
                            direction = agentParameter.direction;
                            Length = agentParameter.length;
                        }
                        else
                        {
                            throw new System.Exception("There are no points saved in the stack.");
                        }
                        break;
                    case EncodingLetters.draw:
                        tempPos = currentPos;
                        currentPos += direction * length;
                        Road road = roadGeneration.CreateRoad(tempPos, currentPos, lSystemParent, false);
                        Length -= 1;
                        roads.Add(road);
                        positions.Add(currentPos);
                        break;
                    case EncodingLetters.turnRight:
                        direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                        break;
                    case EncodingLetters.turnLeft:
                        direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                        break;
                    default:
                        break;
                }
            }
            /* foreach (var position in positions)
            {
                Instantiate(wayPoint, position, Quaternion.identity, parent);
            } */
            
        }

        private void GenerateHousesOnRoads()
        {
            foreach (Road road in roads)
            {
                Debug.Log("Generating houses ");
                GenerateHouse(houses, road, false, 0);
                GenerateHouse(houses, road, false, 1);
                GenerateHouse(houses, road, true, 0);
                GenerateHouse(houses, road, true, 1);
            }
        }

        private void GenerateHouse(List<GameObject> houses, Road road, bool IsOnRightSide, int houseCount)
        {
            float randomPointOnRoad;
            if (houseCount == 1)
            {
                randomPointOnRoad = Random.Range(0.55f, 0.9f);
            }
            else
            {
                randomPointOnRoad = Random.Range(0.1f, 0.45f);
            }
            GameObject house = objectGeneration.CreateHouseOnRoad(road.GetStartPoint(), road.GetEndPoint(), IsOnRightSide, randomPointOnRoad, 10, lSystemParent);
            houses.Add(house);
            for (int j = 0; j < houses.Count - 1; j++)
            {
                GameObject existingHouse = houses[j];
                float distance = Vector3.Distance(existingHouse.transform.localPosition, house.transform.localPosition);
                if (distance <= 3.0f)
                {
                    houses.RemoveAt(houses.Count - 1);
                    Destroy(house);
                    break;
                }
            }
        }

        private void CheckCorrectGeneration(List<Road> roads, List<GameObject> houses)
        {
            int workAmount = houses.Count / 2;
            Debug.Log("Out of " + houses.Count + " houses, there are " + (workAmount - 1) + " workplaces.");
            while (workAmount > 0)
            {
                for (int i = 0; i < houses.Count; i++)
                {
                    GameObject house = houses[i];
                    if (!house.CompareTag("Work"))
                    {
                        UpdateNameAndTag(house, "Home", i);
                    }
                    if (workAmount == 0)
                    {
                        continue;
                    }
                    if (Random.value < 0.2f && !house.CompareTag("Work"))
                    {
                        house.GetComponent<House>().SetHouseType(HouseType.Work);
                        UpdateNameAndTag(house, "Work", workAmount);
                        workAmount--;
                    }
                }
            }
            houses[0].GetComponent<House>().SetHouseType(HouseType.Tavern);
            UpdateNameAndTag(houses[0], "Tavern");
        }

        private static void UpdateNameAndTag(GameObject gameObject, string name, int count = 0)
        {

            if (count == 0)
            {
                gameObject.name = name;
            }
            else
            {
                gameObject.name = name + " " + count;
            }
            gameObject.tag = name;
        }

        private bool DoesThisRoadCrossAnother()
        {
            return true;
        }

        private bool IsOnRoad(Road road, GameObject house)
        {
            //house.transform.localPosition <= road.GetEndPoint()
            return true;
        }

        private T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T) v.GetValue(Random.Range(0, v.Length));
        }

        public enum EncodingLetters
        {
            unknown = '1',
            save = '[',
            load = ']',
            draw = 'F',
            turnRight = '+',
            turnLeft = '-'
        }
    }

}