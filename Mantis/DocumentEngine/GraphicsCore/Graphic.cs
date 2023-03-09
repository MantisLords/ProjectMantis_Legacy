namespace Mantis.DocumentEngine;

public abstract class Graphic
{
    public Matrix3x3 localPosition = Matrix3x3.identity;

    private Matrix3x3 globalPoistion;
    public Matrix3x3 GlobalPosition
    {
        get => globalPoistion;
    }

    internal virtual void UpdatePosition(Matrix3x3 parentPosition)
    {
        globalPoistion = parentPosition * localPosition;
    }

    public abstract void Draw(Canvas canvas);
    
    public void SetPosition(Vector2 position)
    {
        localPosition = Matrix3x3.Translate(position);
    }

    protected Vector2 MP(Vector2 localPoint)
    {
        return GlobalPosition.MultiplyPoint(localPoint);
    }
}