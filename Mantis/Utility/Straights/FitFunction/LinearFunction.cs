using Mantis.DocumentEngine;
using MathNet.Numerics.LinearRegression;

namespace Mantis.Utility;

public class LinearFunction : FitFunction
{
    public double Slope { get; }
    public double ZeroValue { get; }

    public LinearFunction(double slope, double zeroValue)
    {
        Slope = slope;
        ZeroValue = zeroValue;
    }

    public LinearFunction(Vector2 point1, Vector2 point2)
    {
        Slope = (point1.y - point2.y) / (point1.x - point2.x);
        ZeroValue = point1.y - Slope * point1.x;
    }
    
    public LinearFunction(PointPair pair) : this(pair.P1,pair.P2){}
    
    public override double Calculate(double x)
    {
        return x * Slope + ZeroValue;
    }

    public override double CalculateInverse(double y)
    {
        return (y - ZeroValue) / Slope;
    }

    public override double GetSlope()
    {
        return Slope;
    }

    public override double GetZeroValue()
    {
        return ZeroValue;
    }
}