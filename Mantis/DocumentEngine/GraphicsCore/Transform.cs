namespace Mantis.DocumentEngine;

public class Transform : Graphic
{
    public List<Graphic> children = new List<Graphic>();

    public Transform(){}

    public Transform(Transform parent)
    {
        parent.Add(this);
    }
    
    public void Add(Graphic child)
    {
        children.Add(child);
    }

    internal override void UpdatePosition(Matrix3x3 parentPosition)
    {
        base.UpdatePosition(parentPosition);
        foreach (Graphic child in children)
        {
            child.UpdatePosition(GlobalPosition);
        }
    }

    public override void Draw(Canvas canvas)
    {
        foreach (Graphic child in children)
        {
            child.Draw(canvas);
        }
    }
    
}