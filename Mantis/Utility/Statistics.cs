﻿using MathNet.Numerics.Statistics;

namespace Mantis;

public static class Statistics
{
    public static double StandardErrorMean(this IEnumerable<double> source)
    {
        return source.StandardDeviation() / Math.Sqrt(source.Count());
    }

    public static ErDouble MeanWithError(this IEnumerable<double> source)
    {
        return new ErDouble(source.Mean(), source.StandardErrorMean());
    }

    public static string StatisticalPropertiesToString(this IEnumerable<double> source)
    {
        return
            $"Mean:{source.Mean().ToString("G4")}\nStandard Deviation: {source.StandardDeviation().ToString("G4")}\n" +
            $"Error of the mean: {source.StandardErrorMean().ToString("G4")}";
    }
}