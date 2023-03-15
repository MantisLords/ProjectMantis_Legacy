using System.Data;
using System.Diagnostics;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
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
            { 200,, },
            { 300,, },
            { 400,, },
            { 500,, }
        };
        List<ResData> resdata = InitializeData(rawData);
        for (int i = 0; i < resdata.Count; i++)
        {
           ResData e = resdata[i];
           e.Delta = e.A0 / e.A1;

           resdata[i] = e;
        }

        CurrentTableCreator.AddTable("Abklingkoeff",
            new string[] { "Stromstaekre", "abklingconst" },
            resdata.Select(e => new string[] { e.Current.ToString(), e.Delta.ToString() }),
            GlobalStyles.StandardTable,
            1
            );
        SketchBook sketchBook = new SketchBook("Bestimmung der Abklingkonstante");
        var points = resdata.Select(e => new DataPoint(e.Current, e.Delta)).ToList();
        sketchBook.Add(new DataSetSketch("Bestimmung der Abklingkonstante",points));
        GraphCreator creator = new GraphCreator(CurrentDocument,sketchBook,LogAxis.Decade("x",1,2),LogAxis.Decade("y",2,-2),
            GraphOrientation.Portrait);
        
    }
    
    private static List<ResData> InitializeData(double[,] rawData)
    {
        List<ResData> data = new List<ResData>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new ResData()
                {
                    Current = new ErDouble(rawData[i,0],Main_Trial_25_PohlWheel.CURRENT_ERROR),
                    A0= new ErDouble(rawData[i,1],Main_Trial_25_PohlWheel.ERROR_AMPLITUE),
                    A1= new ErDouble(rawData[i,2], Main_Trial_25_PohlWheel.ERROR_AMPLITUE),
                }
            );
        }

        return data;
    }

    public struct ResData
    {
        public ErDouble Current;
        public ErDouble A0;
        public ErDouble A1;
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