using System.Security.Cryptography.X509Certificates;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_1_FocalLengthConvexLensAutokummulation
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] LensData = new double[,]
        {
            {434,435},
            {435,435},
            {433,436},
        };//mm
        List<LensData> data = Initialize(LensData);
        ErDouble mean1 = GetMean1(data);
        ErDouble mean2 = GetMean2(data);
        CurrentTableCreator.Print("Autokumullation: ");
        ErDouble k = mean1 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble l = mean2 - Main_Trial_20_Lenses.defaultObject1Position;
        //CurrentTableCreator.Print($"{mean1}, mean2{mean2} mm");
        CurrentTableCreator.Print($"k = {k} mm");
        CurrentTableCreator.Print($"l = {l} mm");
        CurrentTableCreator.Print($"f = {(k+l)/2} mm");

    }
    public static ErDouble GetMean1(List<LensData> data)
    {
        ErDouble sum = new ErDouble(0, 0);
        foreach (var e in data)
        {
            sum += e.Position1;
        }

        return sum / data.Count;
    }
    public static ErDouble GetMean2(List<LensData> data)
    {
        ErDouble sum = new ErDouble(0, 0);
        foreach (var e in data)
        {
            sum += e.Position2;
        }

        return sum / data.Count;
    }
    public static List<LensData> Initialize(double[,] rawData)
    {
        List<LensData> data = new List<LensData>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new LensData()
                {
                    Position1 = new ErDouble(rawData[i,0],2),
                    Position2 = new ErDouble(rawData[i,1],2)
                }
            );
        }

        return data;
    }

    public struct LensData
    {
        public ErDouble Position1;
        public ErDouble Position2;
    }
}
