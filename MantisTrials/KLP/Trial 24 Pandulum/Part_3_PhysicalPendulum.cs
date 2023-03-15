using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_3_PhysicalPendulum
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_24_Pandulum.CurrentTableCreator;

    public static void Generate()
    {
        var Period = Part_2_Objective_Measurement.CalculateMeanLaserPeriod();
        var a = new ErDouble(863, 1.4); //mm
        var b = new ErDouble(4.5, 1.4); //mm

        CurrentTableCreator.Print(
            $"Reduzierte Pendellänge: {(Period.Pow(2) * Main_Trial_24_Pandulum.G / (4 * Math.PI * Math.PI)).Mul10E(3)} mm");

        var geometricLenght = 2.0 / 3.0 * (a.Pow(2) + a * b + b.Pow(2)) / (a - b);
        geometricLenght.Error = Math.Sqrt(
            (a * 2.0 * (a - 2 * b) / (3 * (a - b).Pow(2))).Pow(2).Value * Math.Pow(a.Error, 2) +
            (b * 2.0 * (b - 2 * a) / (3 * (b - a).Pow(2))).Pow(2).Value * Math.Pow(b.Error, 2));

        CurrentTableCreator.Print($"Geometrische Pendellänge: {geometricLenght} mm");

        CurrentTableCreator.MigraDoc.LastSection.AddPageBreak();
    }
}