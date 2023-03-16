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
            {20.0,1.862},
            {18.0,1.865},
            {16.0,1.868},
            {14.0,1.874},
            {12.0,1.880},
            {10.0,1.883},
            {8.0,1.887},
            {6.0,1.888},
            {4.0,1.891},
            {2.0,1.871}
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