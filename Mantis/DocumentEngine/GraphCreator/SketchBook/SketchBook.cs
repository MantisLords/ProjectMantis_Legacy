namespace Mantis.DocumentEngine;

public class SketchBook
{
    public readonly List<SketchCommand> Sketches = new List<SketchCommand>();

    public string Name { get; }
    public SketchBookStyle Style { get; }

    public SketchBook(string name,SketchBookStyle style)
    {
        Name = name;
        Style = style;
    }
    
    public SketchBook(string name) : this(name,GlobalStyles.StandardGraphStyle){}

    public void Add(SketchCommand sketch)
    {
        Sketches.Add(sketch);
    }

    internal void Plot(LayoutManager layoutManager, Transform graphTransform)
    {
        Transform sketchTransform = new Transform(graphTransform);

        foreach (SketchCommand sketch in Sketches)
        {
            sketch.Plot(layoutManager,sketchTransform,Style);
        }

        Transform titleTransform = new Transform(graphTransform);
        titleTransform.localPosition = Matrix3x3.Translate(new Vector2(layoutManager.XAxis.Length/2,layoutManager.YAxis.Length-5));
        titleTransform.Add(new TextGraphic()
        {
            Style = Style.GraphTitle,
            Text = Name
        });
    }
}