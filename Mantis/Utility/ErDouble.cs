using Mantis.DocumentEngine;

namespace Mantis;

/// <summary>
/// A datatype representing a value with error. It supports basic arithmetic +-*/Pow . It !only! calculates the correct
/// error if in your formula the n
/// </summary>
public struct ErDouble
{
    public double Value;
    public double Error;

    public ErDouble(double value, double error=0)
    {
        Value = value;
        Error = error;
    }

    public static implicit operator ErDouble(double value) => new ErDouble(value);
    public static explicit operator double(ErDouble erDouble) => erDouble.Value;

    public double Max => Value + Error;
    public double Min => Value - Error;

    /// <summary>
    /// Realative Error
    /// </summary>
    public double RelEr
    {
        get => Error / Value;
        set => Error = value * Value;
    }

    /// <summary>
    /// Relative Error Squared
    /// </summary>
    public double RelErSq => RelEr * RelEr;

    public static ErDouble operator +(ErDouble a, ErDouble b)
        => new ErDouble(a.Value + b.Value, Math.Sqrt(a.Error * a.Error + b.Error * b.Error));

    public static ErDouble operator -(ErDouble a)
        => new ErDouble(-a.Value, a.Error);

    public static ErDouble operator -(ErDouble a, ErDouble b) => -b + a;

    public static ErDouble operator *(ErDouble a, ErDouble b)
        => new ErDouble(a.Value * b.Value, a.Value * b.Value * Math.Sqrt(a.RelErSq + b.RelErSq));

    public static ErDouble operator /(ErDouble a, ErDouble b)
        => new ErDouble(a.Value / b.Value, a.Value / b.Value * Math.Sqrt(a.RelErSq + b.RelErSq));

    public ErDouble Pow(double power)
        => new ErDouble(Math.Pow(Value, power), Math.Pow(Value, power) * RelEr * power);

    public ErDouble Mul10E(int power)
        => this * Math.Pow(10, power);
    
    public override string ToString()
    {
        double tmpValue = Value > 0 ? Value : -Value;
        int powerValue = (int) Math.Floor(Math.Log10(tmpValue));

        double formattedValue = tmpValue * Math.Pow(10, -powerValue);
        double formattedError = Error * Math.Pow(10, -powerValue);

        int powerFormattedError = (int) Math.Floor(Math.Log10(formattedError));
        int digits = -powerFormattedError + 1;

        string format = $"F{digits}";

        string appendix = "";
        if (powerValue < 0)
            appendix = $" E{powerValue}";
        else if (powerValue > 0)
            appendix = $" E+{powerValue}";

        string sign = Value < 0 ? "-" : "";

        return $"({sign}{formattedValue.ToString(format)} {(char)0x00B1} {formattedError.ToString(format)})"+appendix;
    }
}