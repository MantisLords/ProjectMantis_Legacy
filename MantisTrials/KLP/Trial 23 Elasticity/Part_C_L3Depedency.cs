using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;

namespace MantisTrials.KLP.Trial_23_Elasticity;

public static class Part_C_L3Depedency
{
    private static MantisDocument CurrentDocument => Main_Trail_23_Elasticity.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trail_23_Elasticity.CurrentTableCreator;
    
    public static void Generate()
    {

        double[,] rawData = new double[,]
        {
            {850, 13.40, 7.67},
            {800, 13.54, 8.79},
            {750, 13.76, 9.84},
            {700, 13.91, 10.73},
            {650, 14.06, 11.51},
            {600, 14.15, 12.13},
            {550, 14.22, 12.77},
            {500, 14.33, 13.14}
        }; //l in mm,s0 in mm,s1 in mm

        ErDouble m = new ErDouble(315.1264, 0.0003); // g
        
        //Conver Raw Data
        List<L3Data> data = ConvertRawData(rawData);
        
        //Do calculations
        data = data.Select(e => CalculateDeltaSAndF(e)).ToList();
        
        //Draw Graph
        SketchBook sketchBook = new SketchBook("C L3 Abhängigkeit");

        List<DataPoint> points = data.Select(e => new DataPoint(e.l, e.deltaS)).ToList();
        sketchBook.Add(new DataSetSketch(points));
        
        //Do Fit
        PolynomialMinMaxFit fit = new PolynomialMinMaxFit(points);
        fit.SetReading(1,true,8,true);
        sketchBook.Add(new StraightSketch(fit));
        
        //Do Layout
        GraphCreator creator = new GraphCreator(document: CurrentDocument,
            sketchBook: sketchBook,
            xAxis: LogAxis.Decade("l / mm", 1, 2),
            yAxis: LogAxis.Decade($"{GreekAlphabet.DELTA}s / mm", 1, 0),
            offset: new Vector2(10,52),
            size:new Vector2(190,190));
        
        //Do Table
        CurrentTableCreator.AddTable(tableName:"Tab14: C l3 Abhängigkeit eines Stahlstabs mit Rohrprofil",
            headers:new string[]{"l / mm",$"{GreekAlphabet.DELTA}s / mm"},
            content:data.Select(e => new string[]{e.l.ToString(),e.deltaS.ToString()}),
            style:GlobalStyles.StandardTable,
            times:2);
        
        CurrentTableCreator.Print(fit.ToString());

    }

    private static List<L3Data> ConvertRawData(double[,] rawData)
    {
        List<L3Data> data = new List<L3Data>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new L3Data()
            {
                l = new ErDouble(rawData[i,0],Main_Trail_23_Elasticity.L_ERROR),
                s0 = new ErDouble(rawData[i,1],Main_Trail_23_Elasticity.S_ERROR),
                s1 = new ErDouble(rawData[i,2],Main_Trail_23_Elasticity.S_ERROR)
            });
        }

        return data;
    }

    private static L3Data CalculateDeltaSAndF(L3Data dataPoint)
    {
        dataPoint.deltaS = dataPoint.s0 - dataPoint.s1;
        return dataPoint;
    }

    public struct L3Data
    {
        public ErDouble l;
        public ErDouble s0;
        public ErDouble s1;

        public ErDouble deltaS;
    }
    
}