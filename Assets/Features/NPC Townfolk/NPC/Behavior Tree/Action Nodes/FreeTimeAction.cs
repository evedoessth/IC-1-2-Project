using UnityEngine;

public class FreeTimeAction : Node
{
    public override bool Execute()
    {
        Debug.Log("Currently relaxing.");
        return true;
    }
}
