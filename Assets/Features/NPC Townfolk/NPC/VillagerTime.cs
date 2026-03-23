using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VillagerTime", menuName = "Scriptable Objects/VillagerTime")]
public class VillagerTime : ScriptableObject
{
    public int currentHour;
    public UnityEvent OnHourChanged;
}