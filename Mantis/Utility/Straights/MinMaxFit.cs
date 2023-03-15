using Mantis.DocumentEngine;
using PdfSharp.Drawing;

namespace Mantis.Utility;

public abstract class MinMaxFit<T> : IMinMaxFit where T : FitFunction
{  public T Optimal { get; }
    public T Min { get; }
    public T Max { get; }

    protected MinMaxFit(List<DataPoint> dataPoints)
    {
        (double[] x, double[] y) = GetDoublePoints(dataPoints);
        Optimal = FitOptimal(x,y);
        (PointPair minPair, PointPair maxPair) = GetMinMaxPoints(dataPoints);

        Min = GetFromPoints(minPair);
        Max = GetFromPoints(maxPair);
    }

    protected abstract T FitOptimal(double[] x,double[] y);
    protected abstract T GetFromPoints(PointPair points);

    public (FitFunction, FitFunction, FitFunction) GetOptimalMinMax()
    {
        return (Optimal, Min, Max);
    }

    public ErDouble GetSlope()
    {
        ErDouble res = Optimal.GetSlope();
        res.Error = Math.Max(Math.Abs(Min.GetSlope() - Optimal.GetSlope()),
            Math.Abs(Max.GetSlope() - Optimal.GetSlope()));
        return res;
    }
    
    public ErDouble GetZeroValue()
    {
        ErDouble res = Optimal.GetZeroValue();
        res.Error = Math.Max(Math.Abs(Min.GetZeroValue() - Optimal.GetZeroValue()),
            Math.Abs(Max.GetZeroValue() - Optimal.GetZeroValue()));
        return res;
    }
    
    public void SetReading(double value1, bool isY1, double value2, bool isY2)
    {
        Optimal.SetReading(value1,isY1,value2,isY2);
        Min.SetReading(value1,isY1,value2,isY2);
        Max.SetReading(value1,isY1,value2,isY2);
    }

    public override string ToString()
    {
        string res = "---MinMaxFit---\n";
        if (Optimal.Reading != null) res += $"Optimal: {Optimal.Reading}\n";
        if (Min.Reading != null) res += $"Min: {Min.Reading}\n";
        if (Max.Reading != null) res += $"Max: {Max.Reading}\n\n";

        res += $"Slope: {GetSlope()}\n";
        res += $"ZeroValue: {GetZeroValue()}\n";
        return res;
    }

    private (PointPair min,PointPair max) GetMinMaxPoints(IEnumerable<DataPoint> data)
    {
        DataPoint smallestX = GetMinDataPoint(data);
        DataPoint biggestX = GetMaxDataPoint(data);

        PointPair min = new PointPair()
        {
            P1 = new Vector2(smallestX.X.Value, smallestX.Y.Max),
            P2 = new Vector2(biggestX.X.Value, biggestX.Y.Min)
        };

        PointPair max = new PointPair()
        {
            P1 = new Vector2(smallestX.X.Value, smallestX.Y.Min),
            P2 = new Vector2(biggestX.X.Value, biggestX.Y.Max)
        };
        return (min, max);
    }

    private DataPoint GetMinDataPoint(IEnumerable<DataPoint> source)
    {
        return source.Aggregate((a,b) => a.X.Value < b.X.Value ? a : b);
    }
    
    private DataPoint GetMaxDataPoint(IEnumerable<DataPoint> source)
    {
        return source.Aggregate((a,b) => a.X.Value > b.X.Value ? a : b);
    }
    
    private (double[],double[]) GetDoublePoints(List<DataPoint> source)
    {
        return (source.Select(e => e.X.Value).ToArray(),source.Select(e => e.Y.Value).ToArray());
    }
}

public interface IMinMaxFit
{
    public (FitFunction, FitFunction, FitFunction) GetOptimalMinMax();
}