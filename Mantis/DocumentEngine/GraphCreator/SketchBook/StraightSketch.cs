using Mantis.Utility;

namespace Mantis.DocumentEngine;

public class StraightSketch<T> : SketchCommand where T : FitFunction
{
    public StraightSketch(MinMaxFit<T> fit)
    {
        Fit = fit;
    }

    private MinMaxFit<T> Fit { get; }

    public override void Plot(LayoutManager layoutManager, Transform sketchRoot,SketchBookStyle style)
    {
        if (Fit is LinearMinMaxFit linearStraight)
        {
            if (!(layoutManager.XAxis is LinearAxis && layoutManager.YAxis is LinearAxis))
                throw new ArgumentException("It is not allowed to draw a linear MinMaxFit on a nonlinear plot!");
        }else if (Fit is PolynomialMinMaxFit)
        {
            if (!(layoutManager.XAxis is LogAxis && layoutManager.YAxis is LogAxis))
                throw new ArgumentException("You must draw a PolynomialMinMaxFit on a Log-Log-Graph!");
        }

        Transform straightTransform = new Transform(sketchRoot);
        
        PlotStraight(Fit.Optimal,layoutManager,sketchRoot,style);
        PlotStraight(Fit.Min,layoutManager,sketchRoot,style);
        PlotStraight(Fit.Max,layoutManager,sketchRoot,style);
        
        PlotReading(Fit.Optimal,layoutManager,sketchRoot,style);
        PlotReading(Fit.Min,layoutManager,sketchRoot,style);
        PlotReading(Fit.Max,layoutManager,sketchRoot,style);
    }

    private void PlotReading(FitFunction function, LayoutManager layoutManager, Transform root, SketchBookStyle style)
    {
        if (function.Reading == null)
            return;
        
        root.Add(new DataMarkGraphic()
        {
            localPosition = Matrix3x3.Translate(layoutManager.UnitToMM(function.Reading.Value.P1)),
            Size = 0.5,
            Style = style.DataMark,
            Type = DataMarkGraphic.MarkType.Circle
        });
        
        root.Add(new DataMarkGraphic()
        {
            localPosition = Matrix3x3.Translate(layoutManager.UnitToMM(function.Reading.Value.P2)),
            Size = 0.5,
            Style = style.DataMark,
            Type = DataMarkGraphic.MarkType.Circle
        });
    }

    private void PlotStraight(FitFunction function, LayoutManager layoutManager, Transform root, SketchBookStyle style)
    {
        Vector2? point1 = null;
        Vector2? point2 = null;

        FindPointY(layoutManager.XAxis.MinUnit,ref point1,ref point2, function, layoutManager);
        FindPointY(layoutManager.XAxis.MaxUnit,ref point1,ref point2, function, layoutManager);
        FindPointX(layoutManager.YAxis.MinUnit,ref point1,ref point2, function, layoutManager);
        FindPointX(layoutManager.YAxis.MaxUnit,ref point1,ref point2, function, layoutManager);

        if (point1 == null || point2 == null)
        {
            Console.WriteLine("Warning: Fit is outside of bounds of the plot");
            return;
        }
        root.Add(new LineGraphic()
        {
            Start = layoutManager.UnitToMM(point1.Value),
            End = layoutManager.UnitToMM(point2.Value),
            Style = style.Straight
        });
    }

    private void FindPointY(double x, ref Vector2? point1, ref Vector2? point2, FitFunction function,
        LayoutManager layoutManager)
    {
        if (IsInsideY(x, out Vector2 newPoint, function, layoutManager))
        {
            if (point1 == null)
                point1 = newPoint;
            else
                point2 = newPoint;
        }
    }
    
    private void FindPointX(double y, ref Vector2? point1, ref Vector2? point2, FitFunction function,
        LayoutManager layoutManager)
    {
        if (IsInsideX(y, out Vector2 newPoint, function, layoutManager))
        {
            if (point1 == null)
                point1 = newPoint;
            else
                point2 = newPoint;
        }
    }

    private bool IsInsideY(double x, out Vector2 point,FitFunction function,LayoutManager layoutManager)
    {
        double y = function.Calculate(x);
        point = new Vector2(x, y);
        return layoutManager.YAxis.MinUnit <= y && layoutManager.YAxis.MaxUnit >= y;
    }

    private bool IsInsideX(double y, out Vector2 point, FitFunction function, LayoutManager layoutManager)
    {
        double x = function.CalculateInverse(y);
        point = new Vector2(x, y);
        return layoutManager.XAxis.MinUnit <= x && layoutManager.XAxis.MaxUnit >= x;
    }
}