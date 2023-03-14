using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public static class Main_Trial_24_Pandulum
{
    public const double REACTION_ERROR = 0.2;// s
    public const double LASER_ERROR = 0.001;//s
    public const double ELECTRO_ERROR = 0.01;//s
    public const double G = 9.81;// m/s^2
    public const double ANGLE_READING_ERROR = 1;// in winkel
    public const double ANGLE_READING_ERROR_SWINGING = 3;
    public static MantisDocument CurrentDocument { get; private set; }
    public static TableCreator CurrentTableCreator { get; private set; }

    public static void Process()
    {
        CurrentDocument = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        CurrentTableCreator = new TableCreator(CurrentDocument);

        Part_1_Stoppwatch.GenerateBoth();
        Part_2_Objective_Measurement.Generate();
        Part_3_PhysicalPendulum.Generate();
        Part_4_Pendulum2.Generate();
        Part_4_Pendulum3.Generate();
        Part_5_GravitationalAcceleration.Generate();
        CurrentDocument.Save("KLP_Trail24_Pendulum_Printout.pdf");
    }

    public static ErDouble CalculateMean(IEnumerable<double> dataEn,out double standardDeviation)
    {
        List<double> data = dataEn.ToList();
        double mean = data.Sum(e => e) / data.Count;

        double deviation = Math.Sqrt(data.Sum(e => (e - mean) * (e - mean)) / data.Count);

        standardDeviation = Math.Sqrt((double)data.Count / (data.Count - 1)) * deviation;

        double standardError = standardDeviation / Math.Sqrt(data.Count);

        return new ErDouble(mean, standardError);
    }
}