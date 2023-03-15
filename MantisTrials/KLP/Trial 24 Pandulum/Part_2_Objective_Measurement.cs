using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MathNet.Numerics.Statistics;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_2_Objective_Measurement
{
    public enum Device
    {
        Laser,
        Electro
    }

    private static readonly double[] laser =
    {
        1.524, 1.524, 1.525, 1.524, 1.524, 1.524, 1.524, 1.524, 1.524, 1.524
    };

    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_24_Pandulum.CurrentTableCreator;

    public static void Generate()
    {
        var meanLaser = CalculateMeanLaserPeriod();
        //ErDouble meanElectro = CalculateMean(electroData, Device.Electro);
        CurrentTableCreator.Print($"meanLaser: {meanLaser} s");
        var laserData = InitializeDatawithError(laser, Device.Laser);
        var stDLaser = laserData.Select(e => e.ObjLaser.Value).StandardDeviation();
        CurrentTableCreator.Print($"Standard deviation {stDLaser} s");
        //CurrentTableCreator.Print($"meanElectro {meanElectro} s");
        //CurrentTableCreator.Print($"Standard deviation {meanElectro.Error * Math.Sqrt(electronic.Length)} s");
    }

    public static ErDouble CalculateMeanLaserPeriod()
    {
        double[] electronic =
        {
            1.516, 1.518, 1.519, 1.519, 1.519, 1.513, 1.519, 1.519, 1.519, 1.511
        };

        var laserData = InitializeDatawithError(laser, Device.Laser);
        //List<ObjectiveData> electroData = InitializeDatawithError(electronic, Device.Electro);
        var meanLaser = laserData.Select(e => e.ObjLaser.Value).MeanWithError();
        return meanLaser;
    }


    private static List<ObjectiveData> InitializeDatawithError(double[] rawData, Device device)
    {
        var data = new List<ObjectiveData>();
        for (var i = 0; i < rawData.Length; i++)
        {
            if (device == Device.Laser)
                data.Add(new ObjectiveData
                    {
                        ObjLaser = new ErDouble(rawData[i], Main_Trial_24_Pandulum.LASER_ERROR)
                    }
                );
            if (device == Device.Electro)
                data.Add(new ObjectiveData
                    {
                        ObjElec = new ErDouble(rawData[i], Main_Trial_24_Pandulum.ELECTRO_ERROR)
                    }
                );
        }

        return data;
    }

    private struct ObjectiveData
    {
        public ErDouble ObjLaser;
        public ErDouble ObjElec;
    }
}