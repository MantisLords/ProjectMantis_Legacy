namespace Mantis.Utility;

public class PolynomialFunction : FitFunction
{
    public double Power;
    public double Factor;
    
    public override double Calculate(double x)
    {
        return Factor * Math.Pow(x, Power);
    }

    public override double CalculateInverse(double y)
    {
        return Math.Pow(y / Factor,1 / Power);
    }

    public override double GetSlope()
    {
        return Power;
    }

    public override double GetZeroValue()
    {
        return Factor;
    }
}