using System.Collections;

namespace Mantis.DocumentEngine;

public abstract class AxisKernel
{
    private double _length;
    public double Length => _length;

    public double MinUnit => MMToUnit(0);
    public double MaxUnit => MMToUnit(Length);
    
    public string AxisName { get; }

    public AxisKernel(string name)
    {
        AxisName = name;
    }

    public abstract double UnitToMM(double value);
    public abstract double MMToUnit(double mm);

    public abstract IEnumerable<GridLineInfo> GenerateGrid();

    public abstract IEnumerable<LabelPlotInfo> GenerateLabeling();
    
    public virtual void Initialize(SketchBook sketchBook,bool isYAxis){}

    public void SetLength(double length) => _length = length;


}