﻿using System.Collections;

namespace Mantis.DocumentEngine;

public class DataSetSketch : SketchCommand
{
    public IEnumerable<DataPoint> Data { get; }

    public string Name { get; }

    public DataSetSketch(List<DataPoint> dataPoints) : this("Data Points", dataPoints){}

    public DataSetSketch(string name, IEnumerable<DataPoint> dataPoints)
    {
        Name = name;
        Data = dataPoints;
    }
    
    public override void Plot(LayoutManager layoutManager, Transform sketchRoot,SketchBookStyle style)
    {
        Transform dataSetTransform = new Transform(sketchRoot);
        foreach (DataPoint dataPoint in Data)
        {
            if(dataPoint.X.Value < 0 || dataPoint.X.Value > layoutManager.XAxis.MaxUnit ||
               dataPoint.Y.Value < 0 || dataPoint.Y.Value > layoutManager.YAxis.MaxUnit)
                Console.WriteLine($"Warning: Datapoint {dataPoint} is outside of graph bounds. You should rescale the graph");
            
            Vector2 pointInUnit = new Vector2(dataPoint.X.Value, dataPoint.Y.Value);
            Vector2 pos = layoutManager.UnitToMM(pointInUnit);
            
            dataSetTransform.Add(new DataMarkGraphic()
            {
                localPosition = Matrix3x3.Translate(pos),
                Style = style.DataMark
            });
            
            dataSetTransform.Add(new DataMarkErrorGraphic()
            {
                Position = pos,
                Style = style.DataMarkErrorBounds,
                Up = layoutManager.YAxis.UnitToMM(dataPoint.Y.Max),
                Down = layoutManager.YAxis.UnitToMM(dataPoint.Y.Min),
                Right = layoutManager.XAxis.UnitToMM(dataPoint.X.Max),
                Left = layoutManager.XAxis.UnitToMM(dataPoint.X.Min),
            });
        }
    }
}