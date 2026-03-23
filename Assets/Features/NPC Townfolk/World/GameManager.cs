using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;
    [SerializeField] private Transform villagerContainer;
    private List<GameObject> villagers;

    void Start()
    {
        villagers = new List<GameObject>();
        for (int i = 0; i < villagerContainer.childCount; i++)
        {
            villagers.Add(villagerContainer.GetChild(i).gameObject);
        }
    }
    void Update()
    {
        if (debugMode)
        {
            foreach (GameObject villager in villagers)
            {
                villager.GetComponent<Villager>().DebugMode = true;
            }
        }
        else
        {
            foreach (GameObject villager in villagers)
            {
                villager.GetComponent<Villager>().DebugMode = false;
            }
        }
    }
}
