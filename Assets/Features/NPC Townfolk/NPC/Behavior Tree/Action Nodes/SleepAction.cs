using UnityEngine;

public class SleepAction : Node
{
    public override bool Execute()
    {
        Debug.Log("Currently sleeping.");
        return true;
    }
}
