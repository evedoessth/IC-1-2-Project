using UnityEngine;

public class EatAction : Node
{
    public override bool Execute()
    {
        Debug.Log("Currently eating.");
        return true;
    }
}
