using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_4_FocalLengthConcaveMirror
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] mirrorPosition = new double[,]
        {
            {553,0},
            {552,0},
            {553,0}
        };
        List<double> data = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(mirrorPosition, 0);
        ErDouble mean = Mantis.Statistics.MeanWithError(data);
        
        ErDouble gegenstand = mean - Main_Trial_20_Lenses.defaultObject1Position;
        CurrentTableCreator.Print($"f = {gegenstand/2} mm");
    }
}