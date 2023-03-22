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
        double[,] lensData = new double[,]
        {
            {434,435},
            {435,435},
            {433,436},
        };//mm
        List<double> dataPosition1 = InitializePosition_x(lensData,0);
        List<double> dataPosition2 = InitializePosition_x(lensData, 1);
        ErDouble mean1 = Mantis.Statistics.MeanWithError(dataPosition1);
        ErDouble mean2 = Mantis.Statistics.MeanWithError(dataPosition2);
        mean1.Error = 2;
        mean2.Error = 2;
        CurrentTableCreator.Print("Autokumullation: ");
        ErDouble k = mean1 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble l = mean2 - Main_Trial_20_Lenses.defaultObject1Position;
        //CurrentTableCreator.Print($"{mean1}, mean2{mean2} mm");
        CurrentTableCreator.Print($"mean1 = {mean1} mm");
        CurrentTableCreator.Print($"mean2 = {mean2} mm");
        CurrentTableCreator.Print($"k = {k} mm");
        CurrentTableCreator.Print($"l = {l} mm");
        CurrentTableCreator.Print($"f = {(k+l)/2} mm");

    }
    public static ErDouble GetMean1(List<LensData> data)
    {
        return data.Select(e => e.Position1.Value).MeanWithError();
    }
    public static ErDouble GetMean2(List<LensData> data)
    {
        return data.Select(e => e.Position2.Value).MeanWithError();
    }
    public static List<double> InitializePosition_x(double[,] rawData, int position)
    {
        List<double> data = new List<double>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(rawData[i,position]);
        }

        return data;
    }

    public struct LensData
    {
        public ErDouble Position1;
        public ErDouble Position2;
    }
}
