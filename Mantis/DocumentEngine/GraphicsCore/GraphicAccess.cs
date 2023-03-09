namespace Mantis.DocumentEngine;

public class GraphicAccess
{
    private Transform root = new Transform();

    private PrinterCorrections Corrections { get; }

    public GraphicAccess(PrinterCorrections corrections)
    {
        Corrections = corrections;
    }

    public Transform Root
    {
        get => root;
    }

    public void Render(Canvas canvas)
    {
        root.localPosition = Matrix3x3.Translate(Corrections.Offset) * Matrix3x3.Scale(Corrections.Scale);
        root.UpdatePosition(Matrix3x3.identity);
        
        root.Draw(canvas);
    }
    
}