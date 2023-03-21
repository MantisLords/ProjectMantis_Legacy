using Mantis.DocumentEngine;

namespace Mantis;

/// <summary>
/// A datatype representing a value with error. It supports basic arithmetic +-*/Pow . It !only! calculates the correct
/// error if in your formula the variable occurs once!
/// </summary>
public struct ErDouble
{
    public double Value;

    private double _error;
    
    public double Error
    {
        get => _error;
        set => _error = Math.Abs(value);
    }

    public ErDouble(double value, double error=0)
    {
        Value = value;
        _error = Math.Abs(error);
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
        => new ErDouble(a.Value * b.Value, Math.Abs(a.Value * b.Value) * Math.Sqrt(a.RelErSq + b.RelErSq));

    public static ErDouble operator /(ErDouble a, ErDouble b)
        => new ErDouble(a.Value / b.Value, Math.Abs(a.Value / b.Value) * Math.Sqrt(a.RelErSq + b.RelErSq));

    /// <summary>
    /// Raises the ErDouble to the power of "power"
    /// </summary>
    public ErDouble Pow(double power)
        => new ErDouble(Math.Pow(Value, power), Math.Abs(Math.Pow(Value, power) * RelEr * power));

    /// <summary>
    /// Multiplies the ErDouble with 10^power
    /// </summary>
    public ErDouble Mul10E(int power)
        => this * Math.Pow(10, power);


    /// <summary>
    /// Calculates e^exponent
    /// </summary>
    public static ErDouble Exp(ErDouble exponent)
    {
        ErDouble res = Math.Exp(exponent.Value);
        res.Error = res.Value * exponent.Error;
        return res;
    }

    /// <summary>
    /// Returns the natural log of the "argument"
    /// </summary>
    public static ErDouble Log(ErDouble argument)
    {
        ErDouble res = Math.Log(argument.Value);
        res.Error = argument.RelEr;
        return res;
    }
    
    public override string ToString()
    {
        if (Math.Abs(Error) <= double.Epsilon)
            return $"{Value.ToString("G5")}";

        if (Math.Abs(Value) <= double.Epsilon)
        {
            return $"0 {(char)0x00B1} {Error.ToString("G2")}";
        }
        
        double tmpValue = Value > 0 ? Value : -Value;
        int powerValue = (int) Math.Floor(Math.Log10(tmpValue));

        if (powerValue is <= 2 and >= -2)
            powerValue = 0;

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