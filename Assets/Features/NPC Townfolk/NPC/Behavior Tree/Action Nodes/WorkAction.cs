using UnityEngine;

public class WorkAction : Node
{
    public override bool Execute()
    {
        Debug.Log("Currently working.");
        return true;
    }
}
