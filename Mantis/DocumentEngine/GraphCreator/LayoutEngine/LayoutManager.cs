namespace Mantis.DocumentEngine;

public class LayoutManager
{
    public AxisKernel XAxis { get; }
    public AxisKernel YAxis { get; }
    
    public SketchBook SketchBook { get; }
    public SketchBookStyle SketchBookStyle => SketchBook.Style;
    
    
    private GraphOrientation GraphOrientation { get; }
    
    public LayoutManager(AxisKernel xAxis, AxisKernel yAxis,GraphOrientation orientation,SketchBook sketchBook,Vector2 size)
    {
        XAxis = xAxis;
        YAxis = yAxis;
        GraphOrientation = orientation;
        SketchBook = sketchBook;
        
        if (orientation == GraphOrientation.Portrait)
        {
            xAxis.SetLength(size.x); // X-Axis 18 cm
            yAxis.SetLength(size.y); // Y-Axis 28 cm
        }
        else
        {
            xAxis.SetLength(size.y); //Switched
            yAxis.SetLength(size.x);
        }
        
        xAxis.Initialize(sketchBook,false);
        yAxis.Initialize(sketchBook,true);
    }

    public Vector2 UnitToMM(Vector2 unit)
    {
        return new Vector2(XAxis.UnitToMM(unit.x), YAxis.UnitToMM(unit.y));
    }

    public Vector2 MMToUnit(Vector2 mm)
    {
        return new Vector2(XAxis.MMToUnit(mm.x), YAxis.MMToUnit(mm.y));
    }
}