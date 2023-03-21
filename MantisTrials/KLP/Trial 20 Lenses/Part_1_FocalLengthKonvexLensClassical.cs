using System.Data.Common;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_1_FocalLengthKonvexLensClassical
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] lensPositions = new double[,]
        {
            { 492, 1145 },
            { 491, 1147 },
            { 492, 1146 },
            { 493, 1148 },
            { 490, 1147 },
            { 490, 1147 },
        };

        List<double> data = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(lensPositions, 1);
        ErDouble mean = Mantis.Statistics.MeanWithError(data);
        ErDouble g = mean - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble b = Main_Trial_20_Lenses.defaultSchirmPostition - mean;
        CurrentTableCreator.Print($"mean={mean} mm");
        CurrentTableCreator.Print($"g={g} mm");
        CurrentTableCreator.Print($"b={b} mm");
        CurrentTableCreator.Print($"f = {CalculateFrequeny(g,b)} mm");


    }

    public static ErDouble CalculateFrequeny(ErDouble g, ErDouble b)
    {
        ErDouble f = 1 / g + 1 / b;
        return 1 / f;
    }

    public static ErDouble GetMean(List<LensData> data)
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
                    Position2 = new ErDouble(rawData[i,1],4)//Es wird Position 2 genutzt da diese eine geringere Schärfentiefe aufweist
                }
            );
        }

        return data;
    }
    public struct LensData
    {
        public ErDouble Position2;
    }
}