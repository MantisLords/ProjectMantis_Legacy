using MathNet.Numerics;

namespace Mantis.Utility;

public class PolynomialMinMaxFit : MinMaxFit<PolynomialFunction>
{
    public PolynomialMinMaxFit(List<DataPoint> dataPoints) : base(dataPoints)
    {
    }

    protected override PolynomialFunction FitOptimal(double[] x,double[] y)
    {
        (double factor,double power) = Fit.Power(x, y);
        return new PolynomialFunction()
        {
            Power = power,
            Factor = factor
        };
    }

    protected override PolynomialFunction GetFromPoints(PointPair points)
    {
        double power = (Math.Log2(points.P1.y) - Math.Log2(points.P2.y)) /
                       (Math.Log2(points.P1.x) - Math.Log2(points.P2.x));
        double factor = points.P1.y / Math.Pow(points.P1.x, power);
        return new PolynomialFunction()
        {
            Power = power,
            Factor = factor
        };
    }
}