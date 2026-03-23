using UnityEngine;

[CreateAssetMenu(fileName = "GenerationSettings", menuName = "Scriptable Objects/GenerationSettings")]
public class GenerationSettings : ScriptableObject
{
    public int seed;
    public int townSize;
}
