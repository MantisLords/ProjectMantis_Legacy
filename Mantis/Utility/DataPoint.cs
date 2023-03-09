namespace Mantis;

public struct DataPoint
{
    public ErDouble X;
    public ErDouble Y;

    public DataPoint(ErDouble x, ErDouble y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"PX: {X} PY: {Y}";
    }
}