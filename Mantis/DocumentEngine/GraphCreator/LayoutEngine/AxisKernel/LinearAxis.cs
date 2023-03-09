using System.Collections;

namespace Mantis.DocumentEngine;

public class LinearAxis : AxisKernel
{
    private double _unitPerMM;
    private double _zeroUnit;

    private bool _autoSetScale = false;

    private LinearAxis(string name,double unitPerMm, double zeroUnit,bool autoSetScale) : base(name)
    {
        _unitPerMM = unitPerMm;
        _zeroUnit = zeroUnit;

        _autoSetScale = autoSetScale;
    }

    public static LinearAxis Manual(string name, double unitPerMM, double zeroUnit = 0) =>
        new LinearAxis(name, unitPerMM, zeroUnit, false);

    public static LinearAxis Auto(string name, double zeroUnit = 0) => new LinearAxis(name, 0, zeroUnit, true);

    public double UnitPerMM => _unitPerMM;
    public double ZeroUnit => _zeroUnit;

    public override double UnitToMM(double value)
    {
        return (value - ZeroUnit) / UnitPerMM;
    }

    public override double MMToUnit(double mm)
    {
        return mm * UnitPerMM + ZeroUnit;
    }

    public override void Initialize(SketchBook sketchBook,bool isYAxis)
    {
        base.Initialize(sketchBook,isYAxis);

        if (_autoSetScale)
        {
            double maxValue = double.NegativeInfinity;

            foreach (SketchCommand sketchCommand in sketchBook.Sketches)
            {
                if (sketchCommand is DataSetSketch dataSetSketch)
                {
                    foreach (var dataPoint in dataSetSketch.Data)
                    {
                        maxValue = Math.Max(maxValue, (isYAxis? dataPoint.Y.Max:dataPoint.X.Max));
                    }
                }
            }

            double range = maxValue - ZeroUnit;
            double minValuePerMM = range / Length;

            if (range <= 0)
                throw new ArgumentException(
                    $"Auto Layout failed. Maximum data value {maxValue} is smaller than zeroUnit {ZeroUnit}");

            _unitPerMM = GetBestValuePerMM(minValuePerMM);
        }
    }

    private double GetBestValuePerMM(double minValuePerMM)
    {
        int smallestPower = (int) Math.Floor(Math.Log10(minValuePerMM));

        double[] allowedScalings = {10,5,2.5,2,1.25};
        
        double smallestScale = Math.Pow(10, smallestPower);
        double bestScale = smallestScale * 10;
        foreach (double scaling in allowedScalings)
        {
            if (smallestScale * scaling < minValuePerMM)
                break;
            bestScale = scaling * smallestScale;
        }

        return bestScale;
    }

    public override IEnumerable<GridLineInfo> GenerateGrid()
    {
        for (int millimeters = 0; millimeters <= Length; millimeters++)
        {
            GridLineType gridLineType;
            
            if (millimeters % 10 == 0)
                gridLineType = GridLineType.Large;
            else if (millimeters % 5 == 0)
                gridLineType = GridLineType.Medium;
            else
                gridLineType = GridLineType.Small;

            yield return new GridLineInfo(){Offset = millimeters,Type = gridLineType};
        }
    }

    public override IEnumerable<LabelPlotInfo> GenerateLabeling()
    {
        for (int millimiters = 0; millimiters < Length; millimiters+= 10)
        {
            double value = MMToUnit(millimiters);
            string label = value.ToString();

            yield return new LabelPlotInfo() {Label = label, Offset = millimiters};
        }
    }
}