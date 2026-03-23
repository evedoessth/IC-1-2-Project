

using System.Collections.Generic;

public class SequenceNode : Node 
{
    private List<Node> nodes = new List<Node>();

    public SequenceNode(List<Node> nodes)
    {
        this.nodes = nodes;
    }
    
    public override bool Execute()
    {
        foreach (var node in nodes)
        {
            if (!node.Execute())
            {
                return false;
            }
        }
        return true;
    }
}