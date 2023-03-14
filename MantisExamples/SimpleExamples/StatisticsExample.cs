using MathNet.Numerics.Statistics;

namespace Mantis.Examples;

public static class StatisticsExample
{
    public static void CalculateBasicStatisticProperties()
    {
        Console.WriteLine("Running Statistic Example");
        
        //Generate random test data
        List<double> data = new List<double>();
        for (int i = 0; i < 20; i++)
        {
            data.Add(i + Math.Sin((double)i /10));
        }
        
        // Calculate basic statistical properties and outputting them to the console
        double mean = data.Mean();
        double standardDeviation = data.StandardDeviation();
        double meanError = data.StandardErrorMean();

        ErDouble meanWithError = data.MeanWithError();
        
        Console.WriteLine($"Properties of data:\n Mean:{mean}\n Standard Deviation: {standardDeviation}\n" +
                          $"Error of the mean: {meanError}\n Mean with error: {meanWithError}");
        
        //And here is a handy shortcut for getting this string output
        Console.WriteLine(data.StatisticalPropertiesToString());
    }
}