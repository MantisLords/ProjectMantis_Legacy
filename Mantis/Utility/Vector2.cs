using PdfSharp.Drawing;

namespace Mantis;

public struct Vector2 : IEquatable<Vector2>
{
    public double x;
    public double y;


    public Vector2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public bool Equals(Vector2 other)
    {
        return x.Equals(other.x) && y.Equals(other.y);
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2 other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }
    
    // Adds two vectors.
    public static Vector2 operator+(Vector2 a, Vector2 b) { return new Vector2(a.x + b.x, a.y + b.y); }
        // Subtracts one vector from another.
        public static Vector2 operator-(Vector2 a, Vector2 b) { return new Vector2(a.x - b.x, a.y - b.y); }
        // Multiplies one vector by another.
        public static Vector2 operator*(Vector2 a, Vector2 b) { return new Vector2(a.x * b.x, a.y * b.y); }
        // Divides one vector over another.
        public static Vector2 operator/(Vector2 a, Vector2 b) { return new Vector2(a.x / b.x, a.y / b.y); }
        // Negates a vector.
        public static Vector2 operator-(Vector2 a) { return new Vector2(-a.x, -a.y); }
        // Multiplies a vector by a number.
        public static Vector2 operator*(Vector2 a, double d) { return new Vector2(a.x * d, a.y * d); }
        // Multiplies a vector by a number.
        public static Vector2 operator*(double d, Vector2 a) { return new Vector2(a.x * d, a.y * d); }
        // Divides a vector by a number.

        public static Vector2 operator/(Vector2 a, double d) { return new Vector2(a.x / d, a.y / d); }
        // Returns true if the vectors are equal.

        public static bool operator==(Vector2 lhs, Vector2 rhs)
        {
            // Returns false in the presence of NaN values.
            double diff_x = lhs.x - rhs.x;
            double diff_y = lhs.y - rhs.y;
            return (diff_x * diff_x + diff_y * diff_y) < 0.000001 * 0.0000001;
        }

        // Returns true if vectors are different.

        public static bool operator!=(Vector2 lhs, Vector2 rhs)
        {
            // Returns true in the presence of NaN values.
            return !(lhs == rhs);
        }

        public double Length() => Math.Sqrt(x * x + y * y);
        public Vector2 normalized => this / this.Length();
        
        public static Vector2 right { get => new Vector2(1, 0); }
        public static Vector2 up { get => new Vector2(0, 1); }
        public static Vector2 left { get => new Vector2(-1, 0); }
        public static Vector2 down { get => new Vector2(0, -1); }

        public static Vector2 zero => new Vector2(0, 0);

        public static Vector2 one => new Vector2(1, 1);

        public override string ToString()
        {
            return $"( {x.ToString("G5")} | {y.ToString("G5")} )";
        }
}