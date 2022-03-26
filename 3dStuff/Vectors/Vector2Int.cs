using System.Globalization;
//using System.Windows;

namespace _3dStuff.Vectors;
public struct Vector2Int
{
    public enum Axis
    {
        X = Vector2.Axis.X,
        Y = Vector2.Axis.Y
    }

    public int X { get; set; }
    public int Y { get; set; }

    public Vector2 YX => new(Y, X);

    public Vector2Int() : this(0, 0) { }
    public Vector2Int(int x, int y) => (X, Y) = (x, y);
    public Vector2Int(Vector2Int other) => this = other;

    public double GetAxis(Axis axis) => axis switch
    {
        Axis.X => X,
        Axis.Y => Y,
        _ => throw new InvalidOperationException("Unknown axis."),
    };

    public Vector3Int To3D(Vector3Int.Axis axisToAdd, int axisValue = 0) => axisToAdd switch
    {
        Vector3Int.Axis.X => new(axisValue, X, Y),
        Vector3Int.Axis.Y => new(X, axisValue, Y),
        Vector3Int.Axis.Z => new(X, Y, axisValue),
        _ => throw new NotImplementedException()
    };

    public Vector2 Scale(Vector2 other) => ((Vector2)this).Scale(other);
    public Vector2Int ScaleInt(Vector2Int other) => new(X * other.X, Y * other.Y);
    public int SqrMagnitudeInt() => X * X + Y * Y;
    public double SqrMagnitude() => 1.0 * X * X + 1.0 * Y * Y;
    public double Magnitude() => Math.Sqrt(SqrMagnitude());

    public Vector2 Normalized()
    {
        double sqrMag = SqrMagnitude();
        if(sqrMag > 0.0)
            return this / Math.Sqrt(sqrMag);
        else
            return Vector2Int._zero;
    }
    public int SqrDistanceInt(Vector2Int b) => (b - this).SqrMagnitudeInt();
    public double SqrDistance(Vector2 b) => (b - this).SqrMagnitude();
    public double Distance(Vector2 b) => (b - this).Magnitude();
    public Vector2 Reflect(Vector2 normal) => ((Vector2)this).Reflect(normal);
    public Vector2 Project(Vector2 vector) => ((Vector2)this).Project(vector);
    public Vector2 ProjectOnPlane(Vector2 planeNormal) => ((Vector2)this).ProjectOnPlane(planeNormal);
    public Vector2 ClampMagnitude(float maxLength) => ((Vector2)this).ClampMagnitude(maxLength);

    public bool Equals(Vector2Int other) => this == other;
    public override bool Equals(object? obj) => obj is Vector2Int other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y);

    #region local operators
    public static bool operator ==(Vector2Int a, Vector2Int b) => (a.X, a.Y) == (b.X, b.Y);
    public static bool operator !=(Vector2Int a, Vector2Int b) => (a == b) == false;

    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2Int operator -(Vector2Int a) => new(-a.X, -a.Y);
    public static Vector2Int operator *(Vector2Int a, int b) => new(a.X * b, a.Y * b);
    public static Vector2Int operator *(int a, Vector2Int b) => b * a;

    public static Vector2 operator *(Vector2Int a, double b) => (Vector2)a * b;
    public static Vector2 operator *(double a, Vector2Int b) => a * (Vector2)b;
    public static Vector2 operator /(Vector2Int a, double b) => (Vector2)a / b;

    public static implicit operator Vector3Int(Vector2Int vector) => new(vector.X, vector.Y, 0);

    public static implicit operator Vector2Int((int x, int y) tuple) => new(tuple.x, tuple.y);
    public static implicit operator (int x, int y)(Vector2Int vector) => (vector.X, vector.Y);
    public static implicit operator Vector2Int(Tuple<int, int> tuple) => new(tuple.Item1, tuple.Item2);
    public static implicit operator Tuple<int, int>(Vector2Int vector) => Tuple.Create(vector.X, vector.Y);
    #endregion

    #region operators with Vector2
    public static bool operator ==(Vector2Int a, Vector2 b) => (Vector2)a == b;
    public static bool operator ==(Vector2 a, Vector2Int b) => a == (Vector2)b;
    public static bool operator !=(Vector2Int a, Vector2 b) => (Vector2)a != b;
    public static bool operator !=(Vector2 a, Vector2Int b) => a != (Vector2)b;
    public static Vector2 operator +(Vector2Int a, Vector2 b) => (Vector2)a + b;
    public static Vector2 operator +(Vector2 a, Vector2Int b) => a + (Vector2)b;
    public static Vector2 operator -(Vector2Int a, Vector2 b) => (Vector2)a - b;
    public static Vector2 operator -(Vector2 a, Vector2Int b) => a - (Vector2)b;
    public static explicit operator Vector2Int(Vector2 vector) => new((int)vector.X, (int)vector.Y);
    public static implicit operator Vector2(Vector2Int vector) => new(vector.X, vector.Y);
    #endregion

    /* System.Windows
    #region System.Windows
    #region operators with Vector2D
    public static bool operator ==(Vector2Int a, Vector b) => a == (Vector2)b;
    public static bool operator ==(Vector a, Vector2Int b) => (Vector2)a == b;
    public static bool operator !=(Vector2Int a, Vector b) => a != (Vector2)b;
    public static bool operator !=(Vector a, Vector2Int b) => (Vector2)a != b;
    public static Vector2 operator +(Vector2Int a, Vector b) => a + (Vector2)b;
    public static Vector2 operator +(Vector a, Vector2Int b) => (Vector2)a + b;
    public static Vector2 operator -(Vector2Int a, Vector b) => a - (Vector2)b;
    public static Vector2 operator -(Vector a, Vector2Int b) => (Vector2)a - b;
    public static explicit operator Vector2Int(Vector vector) => new((int)vector.X, (int)vector.Y);
    public static implicit operator Vector(Vector2Int vector) => new(vector.X, vector.Y);
    #endregion

    #region operators with Size2D
    public static bool operator ==(Vector2Int a, Size b) => a == (Vector2)b;
    public static bool operator ==(Size a, Vector2Int b) => (Vector2)a == b;
    public static bool operator !=(Vector2Int a, Size b) => a != (Vector2)b;
    public static bool operator !=(Size a, Vector2Int b) => (Vector2)a != b;
    public static Vector2 operator +(Vector2Int a, Size b) => a + (Vector2)b;
    public static Vector2 operator +(Size a, Vector2Int b) => (Vector2)a + b;
    public static Vector2 operator -(Vector2Int a, Size b) => a - (Vector2)b;
    public static Vector2 operator -(Size a, Vector2Int b) => (Vector2)a - b;
    public static explicit operator Vector2Int(Size vector) => new((int)vector.Width, (int)vector.Height);
    public static implicit operator Size(Vector2Int vector) => new(vector.X, vector.Y);
    #endregion

    #region operators with Point2D
    public static bool operator ==(Vector2Int a, Point b) => a == (Vector2)b;
    public static bool operator ==(Point a, Vector2Int b) => (Vector2)a == b;
    public static bool operator !=(Vector2Int a, Point b) => a != (Vector2)b;
    public static bool operator !=(Point a, Vector2Int b) => (Vector2)a != b;
    public static Vector2 operator +(Vector2Int a, Point b) => a + (Vector2)b;
    public static Vector2 operator +(Point a, Vector2Int b) => (Vector2)a + b;
    public static Vector2 operator -(Vector2Int a, Point b) => a - (Vector2)b;
    public static Vector2 operator -(Point a, Vector2Int b) => (Vector2)a - b;
    public static explicit operator Vector2Int(Point vector) => new((int)vector.X, (int)vector.Y);
    public static implicit operator Point(Vector2Int vector) => new(vector.X, vector.Y);
    #endregion
    #endregion
    */

    public static Vector2Int Min(Vector2Int a, Vector2Int b) => new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
    public static Vector2Int Max(Vector2Int a, Vector2Int b) => new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
    public static (Vector2Int, Vector2Int) MinMax(Vector2Int a, Vector2Int b)
    {
        Vector2Int min = new();
        Vector2Int max = new();

        (min.X, max.X) = a.X < b.X ? (a.X, b.X) : (b.X, a.X);
        (min.Y, max.Y) = a.Y < b.Y ? (a.Y, b.Y) : (b.Y, a.Y);

        return (min, max);
    }

    public static Vector3Int Cross(Vector2Int a, Vector2Int b) => new(0, 0, a.X * b.Y - a.Y * b.X);
    public static int Dot(Vector2Int a, Vector2Int b) => a.X * b.X + a.Y * b.Y;

    public override string ToString() => ToString(null, null);
    public string ToString(string? format) => ToString(format, null);
    public string ToString(IFormatProvider? formatProvider) => ToString(null, formatProvider);
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if(string.IsNullOrEmpty(format))
            format = "F";
        if(formatProvider == null)
            formatProvider = CultureInfo.InvariantCulture.NumberFormat;
        return $"{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}";
    }

    private static readonly Vector2Int _zero = new(0, 0);
    private static readonly Vector2Int _one = new(1, 1);
    private static readonly Vector2Int _right = new(1, 0);
    private static readonly Vector2Int _left = new(-1, 0);
    private static readonly Vector2Int _up = new(0, 1);
    private static readonly Vector2Int _down = new(0, -1);

    public static Vector2Int Zero => _zero;
    public static Vector2Int One => _one;
    public static Vector2Int Right => _right;
    public static Vector2Int Left => _left;
    public static Vector2Int Up => _up;
    public static Vector2Int Down => _down;
}
