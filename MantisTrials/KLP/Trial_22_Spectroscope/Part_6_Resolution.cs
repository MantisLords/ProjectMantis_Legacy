using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_22_Spectroscope;

public class Part_6_Resolution
{
    private static MantisDocument CurrentDocument => Main_Trial_22_Spectroscope.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_22_Spectroscope.CurrentTableCreator;
    private static DataPoint ablesePunkt1 = new DataPoint(new ErDouble(400,1),new ErDouble(62.4,0.1));
    private static DataPoint ablesePunkt2 = new DataPoint(new ErDouble(680,1),new ErDouble(58.1,0.1));
    private static ErDouble lamda1 = new ErDouble(579.1);
    private static ErDouble lamda2 = new ErDouble(577);

    public static void Generate()
    {
        CurrentTableCreator.Print(CalculateIncline().ToString());
        CurrentTableCreator.Print((lamda2/(lamda1-lamda2)).ToString());
        ErDouble d = new ErDouble(2.2, 0.01);
        ErDouble A = d.Mul10E(6) * CalculateIncline() * Math.PI / 180;
        CurrentTableCreator.Print(A.ToString());
    }

    public static ErDouble CalculateIncline()
    {
        return (ablesePunkt2.Y - ablesePunkt1.Y )/ (ablesePunkt2.X - ablesePunkt1.X);
    }
}