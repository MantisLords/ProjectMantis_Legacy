using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_3_BesselProcedure
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] lensData = new double[,]
        {
            { 372, 633 },
            { 370, 630 },
            { 371, 629 }
        };//mm
        double[,] lensDataAbbildungsgleichung = new double[,]
        {
            { 729, 1170 },
            { 725, 1170 },
            { 727, 1170 }
        };

        Double d = 150;//mm
        ErDouble f1 = new ErDouble(198.7, 1.2);
        ErDouble f2 = new ErDouble( -397.13369073546653,0.012);
        List <Part_1_FocalLengthConvexLensAutokummulation.LensData> data = Initialize(lensData,0.3,0.5);
        List<Part_1_FocalLengthConvexLensAutokummulation.LensData> dataAbbilungsgleichung =
            Initialize(lensDataAbbildungsgleichung, 0.5, 0.2);
        ErDouble position1Linse = Part_1_FocalLengthConvexLensAutokummulation.GetMean1(dataAbbilungsgleichung);
        ErDouble position2Linse = Part_1_FocalLengthConvexLensAutokummulation.GetMean2(dataAbbilungsgleichung);
        ErDouble delta = position2Linse - position1Linse;
        ErDouble e = Main_Trial_20_Lenses.defaultSchirmPostition - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble mean1 = Part_1_FocalLengthConvexLensAutokummulation.GetMean1(data);
        ErDouble mean2 = Part_1_FocalLengthConvexLensAutokummulation.GetMean2(data);
        ErDouble k = mean1 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble l = mean2 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble s = k + l;
        double f = 0.5 * Math.Sqrt(Math.Pow((e.Value-s.Value),2)-Math.Pow(delta.Value,2));
        ErDouble h = s - 2*f;
        ErDouble fTheo = -f1 * f2 / (d - f1 - f2);
        ErDouble hTheo = d * d / (d - f1 - f2);
        CurrentTableCreator.Print($"s= {s.ToString()}");
        CurrentTableCreator.Print($"e= {e.ToString()}");
        CurrentTableCreator.Print($"Position1 = {position1Linse.ToString()}");
        CurrentTableCreator.Print($"Position2 = {position2Linse.ToString()}");
        CurrentTableCreator.Print($"delta= {delta.ToString()}");
        CurrentTableCreator.Print($"f = {f.ToString()}");
        CurrentTableCreator.Print($"h = {h.ToString()} mm");
        CurrentTableCreator.Print($"fTheo = {fTheo} mm");
        CurrentTableCreator.Print($"hTheo = {hTheo} mm");
        
        
    }

    public static List<Part_1_FocalLengthConvexLensAutokummulation.LensData> Initialize(double[,] rawData,double fehler1, double fehler2)
    {
        List<Part_1_FocalLengthConvexLensAutokummulation.LensData> data =
            new List<Part_1_FocalLengthConvexLensAutokummulation.LensData>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new Part_1_FocalLengthConvexLensAutokummulation.LensData()
                {
                    Position1 = new ErDouble(rawData[i,0],fehler1),
                    Position2 = new ErDouble(rawData[i,1],fehler2)
                }
            );
        }

        return data;
    }
}