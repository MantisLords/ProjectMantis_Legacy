using System;
using System.Text.RegularExpressions;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public class Part_1_ResonantFrequency
{
    private static MantisDocument CurrentDocument => Main_Trial_25_PohlWheel.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_25_PohlWheel.CurrentTableCreator;

    public static void Generate()
    {
        double[,] datenfuerEigenfrequenz = new double[,]
        {
            {6.942,1.902},
            {6.467,1.905},
            {6.144,1.900},
            {3.764,1.900},
            {0.843,1.900},
            {0.313,1.950}
        };//in V und in s
        
        List<PeriodenDaten> daten = InitializeData(datenfuerEigenfrequenz);
        List<double> peridoden = daten.Select(e => e.periode.Value).ToList();
        CurrentTableCreator.Print(peridoden.StatisticalPropertiesToString());
    }

    public static List<PeriodenDaten> InitializeData(double[,] rawData)
    {
        List<PeriodenDaten> data = new List<PeriodenDaten>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new PeriodenDaten()
                {
                    amplitude = new ErDouble(rawData[i,0],Main_Trial_25_PohlWheel.ERROR_AMPLITUE),
                    periode = new ErDouble(rawData[i,1],Main_Trial_25_PohlWheel.ERROR_PERIODE)
                    
                }
            );
        }

        return data;
    }

    public struct PeriodenDaten
    {
        public ErDouble amplitude;
        public ErDouble periode;
    }
}