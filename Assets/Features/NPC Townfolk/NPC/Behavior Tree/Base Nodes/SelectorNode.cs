using System.Collections.Generic;

public class SelectorNode : Node {
    
    private List<Node> nodes = new List<Node>();

    public SelectorNode(List<Node> nodes)
    {
        this.nodes = nodes;
    }
    
    
    public override bool Execute() 
    { 
        foreach (var node in nodes)
        {
            if (node.Execute())
            {
                return true;
            }
        }
        
        return false;
    }
}