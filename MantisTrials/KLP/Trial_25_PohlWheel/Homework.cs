using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;
using MathNet.Numerics;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public class Homework
{
 public static MantisDocument CurrentDocument = Main_Trial_25_PohlWheel.CurrentDocument;
 public static TableCreator CurrentTableCreator = Main_Trial_25_PohlWheel.CurrentTableCreator;
 private const double DeltaQuotient = 0.03;

 public static void Generate()
 {
     List<WheelData> data = CalculateTheoreticalResonanzCurve(DeltaQuotient);
     string[][] tableContent = new string[data.Count][];
     for (int i = 0; i < data.Count; i++)
     {
         WheelData e = data[i];
         tableContent[i] = new string[] {e.freqQuotient.ToString("G4"), e.AmplitudeQuotient.ToString("G4")};
     }
     CurrentTableCreator.AddTable("Hausaufgabe",
         new string[]{ "w/w0 ", "A/A0" },
         tableContent,
         GlobalStyles.StandardTable,
         1);
     CurrentTableCreator.MigraDoc.LastSection.AddParagraph();
     SketchBook sketchBook = new SketchBook("Hausaufgabe");
     var points = data.Select(e => new DataPoint(e.freqQuotient, e.AmplitudeQuotient)).ToList();
     sketchBook.Add(new DataSetSketch(points));
     GraphCreator graphcreator = new GraphCreator(CurrentDocument, sketchBook, LinearAxis.Auto("w/w0"),
         LinearAxis.Auto("A/A0"), GraphOrientation.Landscape);

 }

 public static List<WheelData> CalculateTheoreticalResonanzCurve(double deltaQuotient,double finePointDif = 0.02)
 {
     List<WheelData> data = new List<WheelData>();
     double sum = 0;
     while (sum <= 1.8)
     {
         data.Add(new WheelData()
             {
                 AmplitudeQuotient = 1/(Math.Sqrt(Math.Pow(1-Math.Pow(sum,2),2)+Math.Pow(2*sum*deltaQuotient,2))),
                 freqQuotient = sum
             }
         );
         if (sum >= 0.8 && sum < 1.1)
         {
             sum += finePointDif;
         }
         else
         {
            sum += 0.1; 
         }
         
     }

     return data;
 }
}