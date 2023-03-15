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
            { }
        };//in V und in s
        
        List<PeriodenDaten> daten = InitializeData(datenfuerEigenfrequenz);
        
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