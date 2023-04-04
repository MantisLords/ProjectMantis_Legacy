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
    private static DataPoint Opt1 = new DataPoint(400, 62.6);
    private static DataPoint Opt2 = new DataPoint(680,57.9);
    private static DataPoint Min1 = new DataPoint(400,62.4);
    private static DataPoint Min2 = new DataPoint(680, 58.0);
    private static DataPoint Max1 = new DataPoint(400, 62.8);
    private static DataPoint Max2 = new DataPoint(680, 57.7);

    public static void Generate()
    {
        CurrentTableCreator.Print(CalculateIncline().ToString());
        CurrentTableCreator.Print((CalculateIncline()*Math.PI/180).ToString());
        CurrentTableCreator.Print((lamda2/(lamda1-lamda2)).ToString());
        ErDouble d = new ErDouble(1.0, 0.014);
        ErDouble A = d.Mul10E(6) * CalculateIncline() * Math.PI / 180;
        CurrentTableCreator.Print(A.ToString());

        double[] winkelmessungen = new double[]
        {
            263.7, 263.0, 262.5, 262.3,261.2, 262.0, 261.0, 260.9, 260.6, 259.9, 259.5, 258.0
        };

        for (int i = 0; i < winkelmessungen.Length; i++)
        {
            CurrentTableCreator.Print((322.3-winkelmessungen[i]).ToString("G4"));
        }
        Incline();
    }

    public static void Incline()
    {
        ErDouble opt = (Opt2.Y - Opt1.Y )/ (Opt2.X - Opt1.X);
        ErDouble min = (Min2.Y - Min1.Y )/ (Min2.X - Min1.X);
        ErDouble max = (Max2.Y - Max1.Y )/ (Max2.X - Max1.X);
        opt.Error = Math.Max(Math.Abs(opt.Value - min.Value), Math.Abs(max.Value - opt.Value));
        
        CurrentTableCreator.Print("Steigung per Gröstfhler in Grad" + opt.ToString() + "Steigung in Radians" + (opt*Math.PI/180).ToString());
    }
    public static ErDouble CalculateIncline()
    {
        return (ablesePunkt2.Y - ablesePunkt1.Y )/ (ablesePunkt2.X - ablesePunkt1.X);
    }
}