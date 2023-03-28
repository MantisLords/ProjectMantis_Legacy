using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;
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
            { 0, 307.8 ,307.8},
            { 1, 288.0 ,327.3},
            { 2, 264.6 ,349.5},
        };//BEUGUNGSORDNUNG , WINKEL

        List<GridData> data = Initialize(DataForGrid);
        List<GridData> bereinigteDaten = BereinigeDaten(data);
        List<GridData> gridConstant = CalculateGridConstant(bereinigteDaten);
        CurrentTableCreator.AddTable("Part1",new string[]{"Ordnung","mean","SinAngle"},
            gridConstant.Select(e=>new string[]{e.Order.ToString(),e.meanAngle.ToString(),e.SinAngle.ToString()}),
            GlobalStyles.StandardTable);
        
        SketchBook sketchBook = new SketchBook("Graph1");
        List<DataPoint> points = gridConstant.Select(e => new DataPoint(e.Order, e.SinAngle)).ToList();
        
        sketchBook.Add(new DataSetSketch("winkeldaten",points));
        LinearMinMaxFit fit = new LinearMinMaxFit(points);
        fit.SetReading(0.25,false,2,false);
        sketchBook.Add(new StraightSketch(fit));
        CurrentTableCreator.Print(fit.ToString());
        CurrentTableCreator.Print($"Gitterconst: {(1/fit.GetSlope()) * wavelengthPart1}");

        
        GraphCreator creator = new GraphCreator(document: CurrentDocument, sketchBook: sketchBook,
            xAxis: LinearAxis.Auto("Order"),
            yAxis: LinearAxis.Auto($"Sinus"));


    }

    public static List<GridData> CalculateGridConstant(List<GridData> listdata)
    {
        List<GridData> data = new List<GridData>();
        for (int i = 0; i < listdata.Count; i++)
        {
            data.Add(new GridData()
                {
                    Order = listdata[i].Order,
                    meanAngle = listdata[i].meanAngle,
                    SinAngle = ErDouble.Sin(listdata[i].meanAngle*Math.PI/180)
                }
            );
        }

        return data;
    }
    public static List<GridData> BereinigeDaten(List<GridData> dataList)
    {
        List<GridData> data = new List<GridData>();
        for (int i = 0; i < dataList.Count; i++)
        {
            ErDouble zeroAngle = dataList[0].Angle1;
            ErDouble tempMeanAngle = ((zeroAngle - dataList[i].Angle1) + (dataList[i].Angle2 - zeroAngle)) / 2;
            
            data.Add(new GridData()
                {
                    Order = dataList[i].Order,
                    Angle1 = dataList[i].Angle1,
                    Angle2 = dataList[i].Angle2,
                    meanAngle = tempMeanAngle
                }
            );
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
                    Angle1 = new ErDouble(rawData[i,1],0.1),
                    Angle2 = new ErDouble(rawData[i,2],0.1)
                }
            );
        }

        return data;
    }

    public struct GridData
    {
        public ErDouble Order;
        public ErDouble Angle1;
        public ErDouble Angle2;
        public ErDouble meanAngle;
        public ErDouble SinAngle;
    }
}