using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_5_LensErrorChromatic
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] lens3Data = new double[,]
        {
            { 1225, 1246 },
            { 1222, 1240 },
            { 1227, 1242 }
        };
        double[,] lens5Data = new double[,]
        {
            { 829, 829 },
            { 829, 828 },
            { 829, 827 }
        };
        List<double> dataBlue3 = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(lens3Data, 0);
        List<double> dataRed3 = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(lens3Data, 1);
        List<double> dataRed5 = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(lens5Data, 0);
        List<double> dataBlue5 = Part_1_FocalLengthConvexLensAutokummulation.InitializePosition_x(lens5Data, 1);
        ErDouble mean3Blue = Mantis.Statistics.MeanWithError(dataBlue3);
        ErDouble mean3Red = Mantis.Statistics.MeanWithError(dataRed3);
        ErDouble mean5Blue = Mantis.Statistics.MeanWithError(dataBlue5);
        ErDouble mean5Red = Mantis.Statistics.MeanWithError(dataRed5);
        
        CurrentTableCreator.Print($"meanLinse3Rot {mean3Red} mm");
        CurrentTableCreator.Print($"meanLinse3Blau {mean3Blue} mm");
        CurrentTableCreator.Print($"meanLinse5Rot {mean5Red} mm");
        CurrentTableCreator.Print($"meanLinse5Blau {mean5Blue} mm");
        ErDouble fchrom3 = mean3Red - mean3Blue;
        ErDouble fchrom5 = mean5Red - mean5Blue;
        CurrentTableCreator.Print($"Deltaf3 {fchrom3} mm");
        CurrentTableCreator.Print($"Deltaf5 {fchrom5} mm");
    }
}