using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;

namespace Mantis.Utility;

public class LinearMinMaxFit : MinMaxFit<LinearFunction>
{

    public LinearMinMaxFit(List<DataPoint> dataPoints) : base(dataPoints)
    {
    }

    protected override LinearFunction FitOptimal(double[] x,double[] y)
    {
        (double zeroValue, double slope) = Fit.Line(x,y);
        return new LinearFunction(slope, zeroValue);
    }

    protected override LinearFunction GetFromPoints(PointPair points)
    {
        return new LinearFunction(points);
    }
}