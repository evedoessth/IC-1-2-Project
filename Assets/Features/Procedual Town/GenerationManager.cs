using ProceduralTown;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    [SerializeField] private SimpleVisualizer visualizer;
    [SerializeField] private BasicObjectGeneration objectGeneration;
    [SerializeField] private LSystemGenerator lSystemGenerator;
    [SerializeField] private GenerationSettings generationSettings;

    void Start()
    {
        //BeginGeneration();
    }

    public void BeginGeneration()
    {
        Random.InitState(generationSettings.seed);
        string sequence = lSystemGenerator.GenerateSentence();
        visualizer.RunSimpleVisualizer(sequence);
    }
}
