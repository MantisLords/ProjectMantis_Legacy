using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;

namespace MantisTrials.KLP.Trial_23_Elasticity;

public static class Part_A_ElasticityOfMaterial
{
    private static MantisDocument CurrentDocument => Main_Trail_23_Elasticity.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trail_23_Elasticity.CurrentTableCreator;

    public static void GenerateAll()
    {
        GenAIronRound();
        GenABrassRound();
        GenACopperRound();
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
        
        ErDouble defaultLength = new ErDouble(23.51,Main_Trail_23_Elasticity.L_ERROR); // mm

        ErDouble l = new ErDouble(875, Main_Trail_23_Elasticity.L_ERROR); // mm
        ErDouble d = new ErDouble(3.92, Main_Trail_23_Elasticity.D_ERROR); // mm

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
        
        ErDouble defaultLength = new ErDouble(24.46,Main_Trail_23_Elasticity.S_ERROR); // mm

        ErDouble l = new ErDouble(875, Main_Trail_23_Elasticity.L_ERROR); // mm
        ErDouble d = new ErDouble(4.92, Main_Trail_23_Elasticity.D_ERROR); // mm

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
        
        ErDouble defaultLength = new ErDouble(22.57,Main_Trail_23_Elasticity.S_ERROR); // mm

        ErDouble l = new ErDouble(875, Main_Trail_23_Elasticity.L_ERROR); // mm
        ErDouble d = new ErDouble(4.89, Main_Trail_23_Elasticity.D_ERROR); // mm

        ErDouble I = GetGeoMomentOfInertiaRound(d.Mul10E(-3)); // m^4
        
        Generate(rawData,I,defaultLength,l,"A.3 Kupfer Rundprofil",
            "Tab 11: A.3 - Kupfer Rundprofil");
    }

    public static ErDouble GetGeoMomentOfInertiaRound(ErDouble diameter)
    {
        return diameter.Pow(4) * Math.PI / 64;
    }

    public static ErDouble GetGeoMomentOfInertiaRectangular(ErDouble width, ErDouble height)
    {
        return width * height.Pow(3) / 12;
    }

    public static ErDouble GetGeoMomentOfInertiaRodWithHole(ErDouble innerDiameter, ErDouble outerDiameter)
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
        GraphCreator creator = new GraphCreator(document: CurrentDocument, sketchBook: sketchBook,
            xAxis: LinearAxis.Auto("F / N"),
            yAxis: LinearAxis.Auto($"{((char)0x0394)}s / mm"));

        
        //Fourth: Print Tables
        CurrentTableCreator.AddTable(tablename:tableName,
            headers:new string[]{"F / N",$"{((char)0x0394)}s / mm"},
            content:data.Select(e => new string[]{e.F.ToString(),e.deltaS.ToString()}),
            style:GlobalStyles.StandardTable,
            times:2);
        
        //Prints the Read values and the slope of the fit
        CurrentTableCreator.Print(fit.ToString());
        
        //Calculate the Elasticity
        ErDouble elasticity = rodLength.Mul10E(-3).Pow(3) / fit.GetSlope().Mul10E(-3) / 48.0 / geoMomentOfInertia;
        CurrentTableCreator.Print($"\nElasticity: {elasticity.Mul10E(-6)} MPa");
        CurrentTableCreator.MigraDoc.LastSection.AddPageBreak();
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
        data.s.Error = Main_Trail_23_Elasticity.S_ERROR; // One 0.1 mm error

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