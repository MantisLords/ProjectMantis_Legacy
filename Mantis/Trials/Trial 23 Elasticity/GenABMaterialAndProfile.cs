using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;
using MigraDoc.DocumentObjectModel;

namespace Mantis.Trials.Trial_23_Elasticity;

public static class GenABMaterialAndProfile
{
    private const double S_ERROR = 0.05;//mm
    private const double L_ERROR = 1.0;//mm
    private const double D_ERROR = 0.01;//mm

    public const char DELTA = ((char) 0x0394);
    
    private static MantisDocument currentDocument;
    private static TableCreator currentTableCreator;

    public static void GenerateABAll(MantisDocument document)
    {
        currentDocument = document;
        currentTableCreator = new TableCreator(document);
        
        GenAIronRound();
        GenABrassRound();
        GenACopperRound();
        GenBIronRectangular();
        GenBIronRoundWithHole();
    }

    public static void GenAIronRound()
    {
        double[,] ironRodData = new double[,]
        {
            {19.7777,22.53},
            {20.0664,21.45},
            {19.7206,20.38},
            {19.7826,19.29},
            {19.8744,18.19},
            {19.8662,17.03},
            {19.911,15.95},
            {20.0022,14.90},
            {19.9663,13.79},
            {19.7058,12.65},
        }; // m in g,s in mm
        
        ErDouble defaultLength = new ErDouble(23.51,S_ERROR); // mm

        ErDouble l = new ErDouble(875, L_ERROR); // mm
        ErDouble d = new ErDouble(3.92, D_ERROR); // mm

        ErDouble I = GetGeoMomentOfInertiaRound(d.Mul10E(-3)); // m^4
        
        Generate(ironRodData,I,defaultLength,l,"A.1 Stahl Rundprofil",
            "Tab 9: A.1 - Stahl Rundprofil");
    }

    public static void GenABrassRound()
    {
        double[,] rawData = new double[,]
        {
            {19.7777,23.56},
            {20.0664,22.73},
            {19.7206,21.91},
            {19.7826,21.11},
            {19.8744,20.18},
            {19.8662,19.46},
            {19.9110,18.65},
            {20.0022,17.75},
            {19.9663,16.95},
            {19.7058,16.13},
        }; // m in g,s in mm
        
        ErDouble defaultLength = new ErDouble(24.46,S_ERROR); // mm

        ErDouble l = new ErDouble(875, L_ERROR); // mm
        ErDouble d = new ErDouble(4.92, D_ERROR); // mm

        ErDouble I = GetGeoMomentOfInertiaRound(d.Mul10E(-3)); // m^4
        
        Generate(rawData,I,defaultLength,l,"A.2 Messing Rundprofil",
            "Tab 10: A.2 - Messing Rundprofil");
    }

    public static void GenACopperRound()
    {
        double[,] rawData = new double[,]
        {
            {19.7777,21.75},
            {20.0664,21.02},
            {19.7206,20.35},
            {19.7826,19.73},
            {19.8744,18.97},
            {19.8662,18.24},
            {19.9110,17.53},
            {20.0022,16.81},
            {19.9663,16.05},
            {19.7058,15.37},
        }; // m in g,s in mm
        
        ErDouble defaultLength = new ErDouble(22.57,S_ERROR); // mm

        ErDouble l = new ErDouble(875, L_ERROR); // mm
        ErDouble d = new ErDouble(4.89, D_ERROR); // mm

        ErDouble I = GetGeoMomentOfInertiaRound(d.Mul10E(-3)); // m^4
        
        Generate(rawData,I,defaultLength,l,"A.3 Kupfer Rundprofil",
            "Tab 11: A.3 - Kupfer Rundprofil");
    }

    public static void GenBIronRectangular()
    {
        double[,] rawData = new double[,]
        {
            {19.7777,22.15},
            {20.0664,21.41},
            {19.7206,20.73},
            {19.7826,19.97},
            {19.8744,19.20},
            {19.8662,18.38},
            {19.9110,17.64},
            {20.0022,16.85},
            {19.9663,16.11},
            {19.7058,15.42},
        }; // m in g,s in mm
        
        ErDouble defaultLength = new ErDouble(24.00,S_ERROR); // mm

        ErDouble l = new ErDouble(875, L_ERROR); // mm
        
        ErDouble b = new ErDouble(7.89, D_ERROR); // mm
        ErDouble h = new ErDouble(2.88, D_ERROR); // mm

        ErDouble I = GetGeoMomentOfInertiaRectangular(b.Mul10E(-3),h.Mul10E(-3)); // m^4
        
        Generate(rawData,I,defaultLength,l,"B.1 Stahl Rechteckprofil",
            "Tab 12: B.1 Stahl Rechteckprofil");
    }

    public static void GenBIronRoundWithHole()
    {
        double[,] rawData = new double[,]
        {
            {19.7777,22.45},
            {20.0664,22.09},
            {19.7206,21.64},
            {19.7826,21.26},
            {19.8744,20.88},
            {19.8662,20.46},
            {19.9110,20.10},
            {20.0022,19.64},
            {19.9663,19.24},
            {19.7058,18.88},
        }; // m in g,s in mm
        
        ErDouble defaultLength = new ErDouble(22.88,S_ERROR); // mm

        ErDouble l = new ErDouble(875, L_ERROR); // mm
        
        ErDouble d = new ErDouble(5.08, D_ERROR); // mm
        ErDouble D = new ErDouble(5.92, D_ERROR); // mm

        ErDouble I = GetGeoMomentOfInertiaRodWithHole(d.Mul10E(-3),D.Mul10E(-3)); // m^4
        
        Generate(rawData,I,defaultLength,l,"B.2 Stahl Rohrprofil",
            "Tab 13: B.2 Stahl Rohrprofil");
    }

    private static ErDouble GetGeoMomentOfInertiaRound(ErDouble diameter)
    {
        return diameter.Pow(4) * Math.PI / 64;
    }

    private static ErDouble GetGeoMomentOfInertiaRectangular(ErDouble width, ErDouble height)
    {
        return width * height.Pow(3) / 12;
    }

    private static ErDouble GetGeoMomentOfInertiaRodWithHole(ErDouble innerDiameter, ErDouble outerDiameter)
    {
        return (outerDiameter.Pow(4) - innerDiameter.Pow(4)) * Math.PI / 64;
    }

    public static void Generate(double[,] msData, ErDouble geoMomentOfInertia,ErDouble defaultS,ErDouble rodLength, string graphName, string tableName)
    {
        //First initialize the raw data. Meaning add the errors
        var data = InitializeDataSetFromRawData(msData);
        
        //Second: Do the calculations with it. In this case get the relative length deltaS and the Force F
        data = data.Select(e=>CalculateDeltaSAndF(e, defaultS));
        
        //Third: Draw the graphs
        //3.1: Initialize Sketchbook with name
        SketchBook sketchBook = new SketchBook(graphName);
        
        //3.2: Add Points to the graph
        var points = data.Select(e => new DataPoint(e.F, e.deltaS)).ToList();
        sketchBook.Add(new DataSetSketch("IronRodData",points));
        
        //3.3: Add Fit to the graph
        LinearMinMaxFit fit = new LinearMinMaxFit(points);
        fit.SetReading(0.25,false,2,false);
        sketchBook.Add(new StraightSketch<LinearFunction>(fit));


        //3.4: Add the plot to the document
        GraphCreator creator = new GraphCreator(document: currentDocument, sketchBook: sketchBook,
            xAxis: LinearAxis.Auto("F / N"),
            yAxis: LinearAxis.Auto($"{((char)0x0394)}s / mm"));

        
        // //Fourth: Print Tables
        // currentTableCreator.AddTable(tablename:tableName,
        //     headers:new string[]{"F / N",$"{((char)0x0394)}s / mm"},
        //     content:data.Select(e => new string[]{e.F.ToString(),e.deltaS.ToString()}),
        //     style:GlobalStyles.StandardTable,
        //     times:2);
        //
        // //Prints the Read values and the slope of the fit
        // currentTableCreator.Print(fit.ToString());
        //
        // //Calculate the Elasticity
        // ErDouble elasticity = rodLength.Mul10E(-3).Pow(3) / fit.GetSlope().Mul10E(-3) / 48 / geoMomentOfInertia;
        // currentTableCreator.Print($"\nElasticity: {elasticity.Mul10E(-6)} MPa");
        // currentTableCreator.MigraDoc.LastSection.AddPageBreak();
    }

    private static IEnumerable<ElaData> InitializeDataSetFromRawData(double[,] msData)
    {
        //First generate list
        List<ElaData> data = new List<ElaData>();
        for (int i = 0; i < msData.GetLength(0); i++)
        {
            double mass = msData[i, 0];
            if (i > 0)
                mass += data.Last().m.Value;
            data.Add(CalculateError(msData[i,1],mass));
        }

        return data;
    }

    private static ElaData CalculateError(double s, double mass)
    {
        ElaData data = new ElaData();
        data.s = s;
        data.s.Error = S_ERROR; // One 0.1 mm error

        data.m = mass;
        data.m.Error = data.m.Value * 0.0001+0.0001;
        return data;
    }
    
    private const double G = 9.81;

    private static ElaData CalculateDeltaSAndF(ElaData dataPoint,ErDouble defaultS)
    {
        dataPoint.F = dataPoint.m * G / 1000;
        dataPoint.deltaS = defaultS - dataPoint.s;
        return dataPoint;
    }
}

public struct ElaData
{
    public ErDouble s; // mm
    public ErDouble deltaS; // mm
    public ErDouble m; // g
    public ErDouble F; // N
    

    public override string ToString()
    {
        return $"s: {s} m: {m} F: {F} deltaS: {deltaS}";
    }
}