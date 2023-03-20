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
        double[,] LensData = new double[,]
        {
            { 627, 643 },
            { 629, 644 },
            { 625, 646 }
        };
        ErDouble f1 = new ErDouble(198.7, 1.2);
        List <Part_1_FocalLengthConvexLensAutokummulation.LensData> data = Part_1_FocalLengthConvexLensAutokummulation.Initialize(LensData);
        ErDouble mean1 = Part_1_FocalLengthConvexLensAutokummulation.GetMean1(data);
        ErDouble mean2 = Part_1_FocalLengthConvexLensAutokummulation.GetMean2(data);
        ErDouble k = mean1 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble l = mean2 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble fgesamt = (k + l) / 2;
        ErDouble fLinse2 = new ErDouble((f1 * fgesamt / (f1 - fgesamt)).Value, 0.012);//HIER FEHLER AUSGEDACHT - AUFPASSEN
        CurrentTableCreator.Print("Zerstreuungslinse: ");
        CurrentTableCreator.Print($"k = {k} mm");
        CurrentTableCreator.Print($"l = {l} mm");
        CurrentTableCreator.Print($"fgesamt = {fgesamt} mm");
        CurrentTableCreator.Print($"fLinse2 = {fLinse2}");
        

    }
    
    
}