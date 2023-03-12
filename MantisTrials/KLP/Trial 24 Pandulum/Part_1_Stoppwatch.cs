using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MantisTrials.KLP.Trial_23_Elasticity;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public static class Part_1_Stoppwatch
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trail_23_Elasticity.CurrentTableCreator;

    public static void GenerateBoth()
    {
        GenSalah();
        GenThomas();
    }
    public static void GenSalah()
    {
        double[] BrassOneSalah = new double[]
        {
            1.45,1.33,1.48,1.52,1.58,1.52,1.43,1.53,1.56,1.55
        };// in s
        double[] BrassTenSalah = new double[]
        {
            15.4,15.47,15.17,15.50,15.26,15.10,15.32,15.33,15.23,15.24
        };// in s
        
        List<PendulumData> dataSalah = InitializeDataWithError(BrassOneSalah, BrassTenSalah, Name.Salah);
       ErDouble meanOneSalah = CalculateMean(dataSalah, Name.Salah,1);
       ErDouble meanTenSalah = CalculateMean(dataSalah, Name.Salah, 10);
       string[][] tableContentSalah = new string[dataSalah.Count][];
       for (int i = 0; i < dataSalah.Count; i++)
       {
           PendulumData e = dataSalah[i];
           tableContentSalah[i] = new string[] { e.OneSalah.ToString(), e.TenSalah.ToString() };
       }
       CurrentTableCreator.Print($"Salahmittelwert {meanOneSalah} s");
       CurrentTableCreator.Print($"Salahmittelwert10 {meanTenSalah} s");
       CurrentTableCreator.AddTable("Messdaten Salah",
           new string[]{ "Zeiten" },
           tableContentSalah,
           GlobalStyles.StandardTable,
           1);
    }

    public static void GenThomas()
    {
        double[] BrassOneThomas = new double[]
        {
            1.23,1.93,1.48,1.52,1.68,1.52,1.22,1.53,1.66,1.15
        };// in s
        double[] BrassTenThomas = new double[]
        {
            15.60,15.74,15.17,15.45,15.26,15.89,15.23,15.45,15.12,15.24
        };// in s

        List<PendulumData> dataThomas = InitializeDataWithError(BrassOneThomas, BrassTenThomas, Name.Thomas);
        ErDouble meanOneThomas = CalculateMean(dataThomas, Name.Thomas, 1);
        ErDouble meanTenThomas = CalculateMean(dataThomas, Name.Thomas, 10);
        string[][] tableContentThomas = new string[dataThomas.Count][];
        for (int i = 0; i < dataThomas.Count; i++)
        {
            PendulumData e = dataThomas[i];
            tableContentThomas[i] = new string[] { e.OneThomas.ToString(), e.TenThomas.ToString() };
        }

        // tableContentThomas[dataThomas.Count ] = new string[] { meanOneThomas.ToString() };
        // tableContentThomas[dataThomas.Count + 1] = new string[] { meanTenThomas.ToString() };
        CurrentTableCreator.AddTable("Messdaten Thomas",
            new string[]{"Zeiten"},
            tableContentThomas,
            GlobalStyles.StandardTable,
            1);
        CurrentTableCreator.Print($"Thomasmittelwert {meanOneThomas} s");
        CurrentTableCreator.Print($"Thomasmittelwert10 {meanTenThomas} s");
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

    private static ErDouble CalculateMean(List<PendulumData> listdata, Name name, int numberOfOszilations)
    {
        ErDouble sum = new ErDouble(0, 0);
        foreach (PendulumData e in listdata)
        {
            if (numberOfOszilations == 1)
            {
                switch (name)
                {
                    case Name.Salah: sum = sum + e.OneSalah;
                        break;
                    case Name.Thomas: sum = sum + e.OneThomas;
                        break;
                }
            }
            else
            {
                switch (name)
                {
                    case Name.Salah: sum = sum + e.TenSalah;
                        break;
                    case Name.Thomas: sum = sum + e.TenThomas;
                        break;
                } 
            }
        }

        return sum / 10;
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