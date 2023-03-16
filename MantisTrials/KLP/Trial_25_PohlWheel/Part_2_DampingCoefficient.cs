using System.Data;
using System.Diagnostics;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public class Part_2_DampingCoefficient
{
    private static MantisDocument CurrentDocument => Main_Trial_25_PohlWheel.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_25_PohlWheel.CurrentTableCreator;

    public static void Generate()
    {
        double[,] rawData =
        {
            {200,0.0614,0.0003},
            {296,0.100,0.00053},
            {399,0.156,0.00082},
            {500,0.239,0.0017}
        };//Frequenz in Hz, Dämpfungskoeff , Fehler des Dämpfungskoeffizienten aus der Software
        List<ResData> resdata = InitializeData(rawData);
        
        CurrentTableCreator.AddTable("Abklingkoeff",
            new string[] { "Stromstärke / mA", "Abklingconst" },
            resdata.Select(e => new string[] { e.Current.ToString(), e.Delta.ToString() }),
            GlobalStyles.StandardTable,
            1
            );
        SketchBook sketchBook = new SketchBook("Bestimmung der Abklingkonstante");
        var points = resdata.Select(e => new DataPoint(e.Current, e.Delta)).ToList();
        sketchBook.Add(new DataSetSketch("Bestimmung der Abklingkonstante",points));
        PolynomialMinMaxFit fit = new PolynomialMinMaxFit(points);
        fit.SetReading(800,false,0.03,true);
        sketchBook.Add(new StraightSketch(fit));
        GraphCreator creator = new GraphCreator(CurrentDocument,sketchBook,LogAxis.Decade("x",1,2),LogAxis.Decade("y",2,-2),
            GraphOrientation.Portrait);
        CurrentTableCreator.Print(fit.ToString());
        CurrentTableCreator.AddPageBreak();
        
        
    }
    
    private static List<ResData> InitializeData(double[,] rawData)
    {
        List<ResData> data = new List<ResData>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new ResData()
                {
                    Current = new ErDouble(rawData[i,0],Main_Trial_25_PohlWheel.CURRENT_ERROR),
                    Delta = new ErDouble(rawData[i,1],rawData[i,1]*0.05)
                }
            );
        }

        return data;
    }

    public struct ResData
    {
        public ErDouble Current;
        public ErDouble Delta;
    }

    
    public enum Stromstaerke
    {
        I200,
        I300,
        I400,
        I500
}
}