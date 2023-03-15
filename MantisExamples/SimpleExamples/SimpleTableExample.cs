using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

using System.Linq;

namespace Mantis.Examples;

public static class SimpleTableExample
{
    public static void RunBasicExample()
    {
        Console.WriteLine("Running Basic Table Example");
        
        //First create MantisDocument
        MantisDocument document = new MantisDocument();
        
        //Then add a TableCreator. You will use this class to add new tables
        TableCreator tableCreator = new TableCreator(document);
        
        
        //Generate table content. It has to be a list of string arrays. The length of the string arrays has to 
        //match the length of the headers array.
        List<string[]> content = new List<string[]>();
        for (int i = 0; i < 20; i++)
        {
            content.Add(new string[]{i.ToString(),Math.Sin(i).ToString()});
        }
        
        //Create the headers. The length has to match the length of the content arrays
        string[] headers = new string[] {"Index", "Sin"};
        
        //Finally add the table to the tableCreator
        tableCreator.AddTable("Tab1: Example Table",headers,content);
        
        //Lastly save the document
        document.Save("SimpleTableExample.pdf");
    }
}