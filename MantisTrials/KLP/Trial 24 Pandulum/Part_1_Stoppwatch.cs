using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MantisTrials.KLP.Trial_23_Elasticity;
using MathNet.Numerics.Statistics;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public static class Part_1_Stoppwatch
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_24_Pandulum.CurrentTableCreator;

    public static void GenerateBoth()
    {
        GenSalah();
        GenThomas();
    }
    public static void GenSalah()
    {
        double[] BrassOneSalah = new double[]
        {
            1.41,1.56,1.47,1.53,1.56,1.47,1.47,1.53,1.50,1.53
        };// in s
        double[] BrassTenSalah = new double[]
        {
            15.35,15.34,15.28,15.28,15.28,15.40,15.37,15.34,15.34,15.38
        };// in s
        
        List<PendulumData> dataSalah = InitializeDataWithError(BrassOneSalah, BrassTenSalah, Name.Salah);
        string statisticsOutSalahOne = dataSalah.Select(e => e.OneSalah.Value).StatisticalPropertiesToString();
       string statisticsOutSalahTen = dataSalah.Select(e => e.TenSalah.Value).StatisticalPropertiesToString();
       
       string[][] tableContentSalah = new string[dataSalah.Count][];
       for (int i = 0; i < dataSalah.Count; i++)
       {
           PendulumData e = dataSalah[i];
           tableContentSalah[i] = new string[] { e.OneSalah.ToString(), e.TenSalah.ToString() };
       }
       CurrentTableCreator.Print($"Salah in s: {statisticsOutSalahOne }");
       CurrentTableCreator.Print($"Salah10 in s {statisticsOutSalahTen}");
       CurrentTableCreator.AddTable("Messdaten Salah",
           new string[]{ "Eine Periode / s", "Zehn Perioden / s" },
           tableContentSalah,
           GlobalStyles.StandardTable,
           1);
       CurrentTableCreator.MigraDoc.LastSection.AddParagraph();
    }

    public static void GenThomas()
    {
        double[] BrassOneThomas = new double[]
        {
            1.43,1.59,1.53,1.46,1.59,1.53,1.62,1.62,1.43,1.53
        };// in s
        double[] BrassTenThomas = new double[]
        {
            15.22,15.25,15.22,15.34,15.22,15.28,15.53,15.34,15.31,15.25
        };// in s

        List<PendulumData> dataThomas = InitializeDataWithError(BrassOneThomas, BrassTenThomas, Name.Thomas);

        string statisticsOutOneThomas = dataThomas.Select(e => e.OneThomas.Value).StatisticalPropertiesToString();
        string statisticsOutTenThomas = dataThomas.Select(e => e.TenThomas.Value).StatisticalPropertiesToString();
        
        string[][] tableContentThomas = new string[dataThomas.Count][];
        for (int i = 0; i < dataThomas.Count; i++)
        {
            PendulumData e = dataThomas[i];
            tableContentThomas[i] = new string[] { e.OneThomas.ToString(), e.TenThomas.ToString() };
        }

        // tableContentThomas[dataThomas.Count ] = new string[] { meanOneThomas.ToString() };
        // tableContentThomas[dataThomas.Count + 1] = new string[] { meanTenThomas.ToString() };
        CurrentTableCreator.Print($"Thomas in s: {statisticsOutOneThomas}");
        CurrentTableCreator.Print($"Thomas10 in s: {statisticsOutTenThomas}");
        CurrentTableCreator.AddTable("Messdaten Thomas",
            new string[]{"Eine Periode / s", "Zehn Perioden / s"},
            tableContentThomas,
            GlobalStyles.StandardTable,
            1);
        CurrentTableCreator.MigraDoc.LastSection.AddParagraph();

    }

    private static List<PendulumData> InitializeDataWithError(double[] rawDataOne, double[] rawDataTen, Name name)
    {
        List<PendulumData> data = new List<PendulumData>();
        for (int i = 0; i < rawDataOne.Length; i++)
        {
            if (name == Name.Salah)
            {
               data.Add(new PendulumData()
                   {
                       OneSalah = new ErDouble(rawDataOne[i],Main_Trial_24_Pandulum.REACTION_ERROR),
                       TenSalah = new ErDouble(rawDataTen[i],Main_Trial_24_Pandulum.REACTION_ERROR)
                   }
               ); 
            }
            if (name == Name.Thomas)
            {
                data.Add(new PendulumData()
                    {
                        OneThomas = new ErDouble(rawDataOne[i],Main_Trial_24_Pandulum.REACTION_ERROR),
                        TenThomas = new ErDouble(rawDataTen[i],Main_Trial_24_Pandulum.REACTION_ERROR)
                    }
                ); 
            }
            
        }

        return data;
    }


    private struct PendulumData
    {
        public ErDouble OneThomas;
        public ErDouble TenThomas;
        public ErDouble OneSalah;
        public ErDouble TenSalah;
    }

    public enum Name
    {
        Thomas,
        Salah
    }
    
}