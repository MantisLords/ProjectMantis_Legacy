using Mantis.DocumentEngine;

namespace Mantis.Utility;

public struct PointPair
{
    public Vector2 P1;
    public Vector2 P2;

    public override string ToString()
    {
        return $"Point1: {P1} Point2: {P2}";
    }
}