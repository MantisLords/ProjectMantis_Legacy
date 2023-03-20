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
        double[] mirrorPosition = new double[]
        {
            553, 552, 553
        };
        ErDouble sum = new ErDouble(0, 0);
        for (int i = 0; i < mirrorPosition.Length; i++)
        {
            sum += mirrorPosition[i];
        }

        ErDouble Mittelwert = sum / mirrorPosition.Length;
        ErDouble Gegenstand = Mittelwert - Main_Trial_20_Lenses.defaultObject1Position;
        CurrentTableCreator.Print($"f = {Gegenstand/2} mm");
    }
}