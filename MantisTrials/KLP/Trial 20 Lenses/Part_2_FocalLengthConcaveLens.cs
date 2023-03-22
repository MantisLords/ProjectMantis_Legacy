using System.Security.Cryptography.X509Certificates;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_2_FocalLengthConcaveLens
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] lensData = new double[,]
        {
            { 627, 643 },
            { 629, 644 },
            { 625, 646 }
        };
        ErDouble f1 = new ErDouble(198.7, 0.4);
        List<double> dataPosition1 = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(lensData, 0);
        List<double> dataPosition2 = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(lensData, 1);
        ErDouble mean1 = Mantis.Statistics.MeanWithError(dataPosition1);
        ErDouble mean2 = Mantis.Statistics.MeanWithError(dataPosition2);
        mean1.Error = 4;
        mean2.Error = 4;
        ErDouble k = mean1 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble l = mean2 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble fgesamt = (k + l) / 2;
        ErDouble fLinse2 = 1/((1 / fgesamt) - (1 / f1));
        CurrentTableCreator.Print("Zerstreuungslinse: ");
        CurrentTableCreator.Print($"mean1 = {mean1} mm");
        CurrentTableCreator.Print($"mean2 = {mean2} mm");
        CurrentTableCreator.Print($"k = {k} mm");
        CurrentTableCreator.Print($"l = {l} mm");
        CurrentTableCreator.Print($"fgesamt = {fgesamt} mm");
        CurrentTableCreator.Print($"fLinse2 = {fLinse2}");
        

    }
    
    
}