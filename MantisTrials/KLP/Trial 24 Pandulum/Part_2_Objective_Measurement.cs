using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MantisTrials.KLP.Trial_23_Elasticity;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_2_Objective_Measurement
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_24_Pandulum.CurrentTableCreator;

    public static void Generate()
    {
        double[] laser = new double[]
        {
        1.518,1.519,1.519,1.519,1.519,1.518,1.519,1.519,1.519,1.519
        };

        double[] electronic = new double[]
        {
            1.516,1.518,1.519,1.519,1.519,1.513,1.519,1.519,1.519,1.511
        };

        List<ObjectiveData> laserData = InitializeDatawithError(laser, Device.Laser);
        List<ObjectiveData> electroData = InitializeDatawithError(electronic, Device.Electro);
        ErDouble meanLaser = CalculateMean(laserData, Device.Laser);
        ErDouble meanElectro = CalculateMean(electroData, Device.Electro);
        CurrentTableCreator.Print($"meanLaser: {meanLaser} s");
        CurrentTableCreator.Print($"Standard deviation {meanLaser.Error * Math.Sqrt(laser.Length)} s");
        CurrentTableCreator.Print($"meanElectro {meanElectro} s");
        CurrentTableCreator.Print($"Standard deviation {meanElectro.Error * Math.Sqrt(electronic.Length)} s");

    }

    
    private static List<ObjectiveData> InitializeDatawithError(double[] rawData, Device device)
    {
        List <ObjectiveData> data = new List<ObjectiveData>();
        for (int i = 0; i < rawData.Length; i++)
        {
            if (device == Device.Laser)
            {
                data.Add(new ObjectiveData()
                    {
                        ObjLaser = new ErDouble(rawData[i],Main_Trial_24_Pandulum.LASER_ERROR)
                    }
                );
            }
            if (device == Device.Electro)
            {
                data.Add(new ObjectiveData()
                    {
                        ObjElec = new ErDouble(rawData[i],Main_Trial_24_Pandulum.ELECTRO_ERROR)
                    }
                );
            }
            
        }

        return data;
    }

    private static ErDouble CalculateMean(List<ObjectiveData> data,Device device)
    {
        ErDouble sum = new ErDouble(0, 0);
        foreach (ObjectiveData e in data)
        {
            switch (device)
            {
                case Device.Laser: sum = sum + e.ObjLaser;
                    break;
                case Device.Electro: sum = sum + e.ObjElec;
                    break;
            }
            
        }

        return sum / 10;
    }
    private struct ObjectiveData
    {
        public ErDouble ObjLaser;
        public ErDouble ObjElec;
    }
    public enum Device
    {
        Laser,
        Electro
    }
}