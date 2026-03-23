using UnityEngine;
using Random = UnityEngine.Random;

public class VectorCalculation
{
    public Vector3 GetRandomVectorInRange(int range)
    {
        return new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
    }

    public Vector3 GetStartPosAtEdge(cardinalDirection direction, int minRange, int maxRange, int constantMaxDistance)
    {
        Vector3 pos = new Vector3();
        switch (direction)
        {
            case cardinalDirection.North:
            pos = new Vector3(Random.Range(-minRange, maxRange), 0, constantMaxDistance);
            return pos;

            case cardinalDirection.East:
            pos = new Vector3(constantMaxDistance, 0, Random.Range(-minRange, maxRange));
            return pos;

            case cardinalDirection.South:
            pos = new Vector3(Random.Range(-minRange, maxRange), 0, -constantMaxDistance);
            return pos;
            
            case cardinalDirection.West:
            pos = new Vector3(-constantMaxDistance, 0, Random.Range(-minRange, maxRange));
            return pos;
        }   
        return pos;
    }
    
    public Vector3 CalculatePointPerpendicularToLine(Vector3 startPos, Vector3 goalPos, Vector3 linePoint, int distance, bool isOnRightSide)
    {
        Vector2 rNormal;
        
        var rx = goalPos.x - startPos.x;
        var rz = goalPos.z - startPos.z;
        Vector2 rVector = new Vector2(rx, rz);
        if (isOnRightSide)
        {
            rNormal = 1 / Vector2.SqrMagnitude(rVector) * new Vector2(rz, -rx);
        }
        else
        {
            rNormal = 1/Vector2.SqrMagnitude(rVector) * new Vector2(-rz,rx);
        }
        Vector3 objectPoint = new Vector3(rNormal.x, 0, rNormal.y);
        return (objectPoint * distance) + linePoint;
    }

    public Vector3 CalculatePointAtEndOfLine(Vector3 startPos, Vector3 goalPos, float distance)
    {
        
        Vector3 heading = goalPos - startPos;
        Vector3 objectPoint = goalPos + (heading / heading.magnitude);
        return objectPoint * distance;
    }
}   