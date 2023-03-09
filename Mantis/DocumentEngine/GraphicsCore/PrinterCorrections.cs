namespace Mantis.DocumentEngine;

public struct PrinterCorrections
{
    public Vector2 Scale = Vector2.one;
    public Vector2 Offset = Vector2.zero;

    public PrinterCorrections()
    {
    Scale = Vector2.one;
    Offset = Vector2.zero;
    }

    public PrinterCorrections(Vector2 scale, Vector2 offset)
    {
        Scale = scale;
        Offset = offset;
    }
}