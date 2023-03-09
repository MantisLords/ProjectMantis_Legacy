using System.Collections;
using Mantis.DocumentEngine;

using System.Linq;

namespace Mantis.Utility;

public abstract class FitFunction
{
    public PointPair? Reading { get; private set; }
    
    public abstract double Calculate(double x);

    public abstract double CalculateInverse(double y);

    public abstract double GetSlope();
    public abstract double GetZeroValue();

    public void SetReading(double value1, bool isY1, double value2, bool isY2)
    {
        Reading = new PointPair()
        {
            P1 = isY1 ? new Vector2(CalculateInverse(value1), value1) : new Vector2(value1, Calculate(value1)),
            P2 = isY2 ? new Vector2(CalculateInverse(value2), value2) : new Vector2(value2, Calculate(value2))
        };
    }



}