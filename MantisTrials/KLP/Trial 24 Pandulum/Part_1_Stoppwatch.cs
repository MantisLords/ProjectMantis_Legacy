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

    public static void GenSalah()
    {
        double[] BrassOneSalah = new double[]
        {
            
        };// in s
        double[] BrassTenSalah = new double[]
        {
            
        };// in s
        
        List<PendulumData> dataSalah = InitializeDataWithError(BrassOneSalah, BrassTenSalah, Name.Salah);
       ErDouble meanOneSalah = CalculateMean(dataSalah, Name.Salah,1);
       ErDouble meanTenSalah = CalculateMean(dataSalah, Name.Salah, 10);
    }

    public static void GenThomas()
    {
        double[] BrassOneThomas = new double[]
        {
            
        };// in s
        double[] BrassTenThomas = new double[]
        {
            
        };// in s

        List<PendulumData> dataThomas = InitializeDataWithError(BrassOneThomas, BrassTenThomas, Name.Thomas);
        ErDouble meanOneThomas = CalculateMean(dataThomas, Name.Thomas, 1);
        ErDouble meanTenThomas = CalculateMean(dataThomas, Name.Thomas, 10);
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