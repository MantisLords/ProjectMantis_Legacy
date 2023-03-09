namespace Mantis.DocumentEngine;

public class LogAxis : AxisKernel
{
    public int Decades { get; }
    public int ZeroPower { get; }
    
    private LogAxis(string name,int decades,int zeroPower) : base(name)
    {
        Decades = decades;
        ZeroPower = zeroPower;
    }

    public static LogAxis Decade(string name, int decades, int zeroPower)
        => new LogAxis(name, decades, zeroPower);

    public override double UnitToMM(double value)
    {
        return (Math.Log10(value) - ZeroPower) / ((double)Decades) * Length;
    }

    public override double MMToUnit(double mm)
    {
        return Math.Pow(10, mm / Length * Decades + ZeroPower);
    }

    public override IEnumerable<GridLineInfo> GenerateGrid()
    {
        for (int decade = ZeroPower; decade < Decades + ZeroPower; decade++)
        {
            double floor = Math.Pow(10, decade);
            yield return new GridLineInfo() {Offset = UnitToMM(floor),Type = GridLineType.Large};

            for (int i = 2; i <= 9; i++)
            {
                yield return new GridLineInfo() { Offset = UnitToMM(floor * i),Type = GridLineType.Medium};
            }
        }
        double lastFloor = Math.Pow(10, ZeroPower + Decades);
        yield return new GridLineInfo() {Offset = UnitToMM(lastFloor),Type = GridLineType.Large};
    }

    public override IEnumerable<LabelPlotInfo> GenerateLabeling()
    {
        for (int decade = ZeroPower; decade < Decades + ZeroPower; decade++)
        {
            double floor = Math.Pow(10, decade);
            yield return new LabelPlotInfo() {Label = $"E{decade}",Offset = UnitToMM(floor)};

            for (int i = 2; i <= 9; i++)
            {
                yield return new LabelPlotInfo() {Label = i.ToString(), Offset = UnitToMM(floor * i)};
            }
        }
        
        double lastFloor = Math.Pow(10, ZeroPower + Decades);
        yield return new LabelPlotInfo() {Label = $"E{ZeroPower + Decades}",Offset = UnitToMM(lastFloor)};
    }
}