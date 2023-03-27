using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MantisTrials.KLP.Trial_23_Elasticity;
using MantisTrials.KLP.Trial_25_PohlWheel;

namespace MantisTrials.KLP.Trial_22_Spectroscope;

public class Part_1_GridConstant
{
    public static double wavelengthPart1 = 589*Math.Pow(10,-9);
    private static MantisDocument CurrentDocument => Main_Trial_22_Spectroscope.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_22_Spectroscope.CurrentTableCreator;

    public static void Generate()
    {
        double[,] DataForGrid = new double[,]
        {
            { 0, 307.8 },
            { 1, 288.0 },
            { 1, 327.3 },
            { 2, 264.6 },
            { 2, 349.5 }
        };//BEUGUNGSORDNUNG , WINKEL

        List<GridData> data = Initialize(DataForGrid);
        List<GridData> bereinigteDaten = BereinigeDaten(data);
        ErDouble[,] gridConstants = CalculateGridConstant(bereinigteDaten);
        CurrentTableCreator.AddTable("Part1",new string[]{"Ordnung","gitterconst"},gridConstants.ToString(),GlobalStyles.StandardTable);


    }

    public static ErDouble[,] CalculateGridConstant(List<GridData> listdata)
    {
        ErDouble[,] data = new ErDouble[listdata.Count,1];
        for (int i = 0; i < listdata.Count; i++)
        {
            data[i, 0] = listdata[i].Order;
            data[i, 1] = listdata[i].Order * wavelengthPart1 / Math.Sin(listdata[i].Angle.Value);
        }

        return data;
    }
    public static List<GridData> BereinigeDaten(List<GridData> dataList)
    {
        List<GridData> data = new List<GridData>();
        for (int i = 1; i < dataList.Count; i+=2)
        {
            GridData e = dataList[i];
            e.Angle = dataList[i].Angle + dataList[i + 1].Angle / 2;
            data[i] = e;
        }

        return data;
    }
    public static List<GridData> Initialize(double[,] rawData)
    {
        List<GridData> data = new List<GridData>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new GridData()
                {
                    Order = new ErDouble(rawData[i,0],0),
                    Angle = new ErDouble(rawData[i,1],0.1)
                }
            );
        }

        return data;
    }

    private struct GridData
    {
        public ErDouble Order;
        public ErDouble Angle;
    }
}