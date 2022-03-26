using System.Globalization;
//using System.Windows;

namespace _3dStuff.Vectors;
public struct Vector2
{
    public enum Axis
    {
        X = Vector3.Axis.X,
        Y = Vector3.Axis.Y
    }

    public double X { get; set; }
    public double Y { get; set; }

    public Vector2 YX => new(Y, X);

    public Vector2() : this(0, 0) { }
    public Vector2(double x, double y) => (X, Y) = (x, y);
    public Vector2(Vector2 other) => this = other;

    public double GetAxis(Axis axis) => axis switch
    {
        Axis.X => X,
        Axis.Y => Y,
        _ => throw new InvalidOperationException("Unknown axis."),
    };

    public Vector3 To3D(Vector3.Axis axisToAdd, double axisValue = 0) => axisToAdd switch
    {
        Vector3.Axis.X => new(axisValue, X, Y),
        Vector3.Axis.Y => new(X, axisValue, Y),
        Vector3.Axis.Z => new(X, Y, axisValue),
        _ => throw new NotImplementedException()
    };

    public Vector2 Scale(Vector2 other) => new(X * other.X, Y * other.Y);
    public double SqrMagnitude() => X * X + Y * Y;
    public double Magnitude() => Math.Sqrt(SqrMagnitude());

    public Vector2 Normalize() => this = Normalized();
    public Vector2 Normalized()
    {
        double sqrMag = SqrMagnitude();
        if(sqrMag > 0.0)
            return this / Math.Sqrt(sqrMag);
        else
            return Vector2._zero;
    }
    public double SqrDistance(Vector2 b) => (b - this).SqrMagnitude();
    public double Distance(Vector2 b) => (b - this).Magnitude();
    public Vector2 Reflect(Vector2 normal)
    {
        double factor = -2F * Vector2.Dot(normal, this);
        return normal * factor + this;
    }
    public Vector2 Project(Vector2 vector)
    {
        double sqrMag = vector.SqrMagnitude();
        if(sqrMag == 0)
            return Vector2._zero;
        else
            return vector * Vector2.Dot(this, vector) / sqrMag;
    }
    public Vector2 ProjectOnPlane(Vector2 planeNormal)
    {
        double sqrMag = planeNormal.SqrMagnitude();
        if(sqrMag == 0)
            return this;
        else
            return (this - planeNormal) * Vector2.Dot(this, planeNormal) / sqrMag;
    }
    public Vector2 ClampMagnitude(float maxLength)
    {
        double sqrmag = SqrMagnitude();
        if(sqrmag > maxLength * maxLength)
            return this / Math.Sqrt(sqrmag) * maxLength;
        else
            return this;
    }

    public bool Equals(Vector2 other) => this == other;
    public override bool Equals(object? obj) => obj is Vector2 other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y);

    #region local operators
    public static bool operator ==(Vector2 a, Vector2 b) => (a.X, a.Y) == (b.X, b.Y);
    public static bool operator !=(Vector2 a, Vector2 b) => (a == b) == false;

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator -(Vector2 a) => new(-a.X, -a.Y);
    public static Vector2 operator *(Vector2 a, double b) => new(a.X * b, a.Y * b);
    public static Vector2 operator *(double a, Vector2 b) => b * a;
    public static Vector2 operator /(Vector2 a, double b) => new(a.X / b, a.Y / b);

    public static implicit operator Vector3(Vector2 vector) => new(vector.X, vector.Y, 0);

    public static implicit operator Vector2((double x, double y) tuple) => new(tuple.x, tuple.y);
    public static implicit operator (double x, double y)(Vector2 vector) => (vector.X, vector.Y);
    public static implicit operator Vector2(Tuple<double, double> tuple) => new(tuple.Item1, tuple.Item2);
    public static implicit operator Tuple<double, double>(Vector2 vector) => Tuple.Create(vector.X, vector.Y);
    #endregion

    /* System.Windows
    #region System.Windows
    #region operators with Vector
    public static bool operator ==(Vector2 a, Vector b) => a == (Vector2)b;
    public static bool operator ==(Vector a, Vector2 b) => (Vector2)a == b;
    public static bool operator !=(Vector2 a, Vector b) => a != (Vector2)b;
    public static bool operator !=(Vector a, Vector2 b) => (Vector2)a != b;
    public static Vector2 operator +(Vector2 a, Vector b) => a + (Vector2)b;
    public static Vector2 operator +(Vector a, Vector2 b) => (Vector2)a + b;
    public static Vector2 operator -(Vector2 a, Vector b) => a - (Vector2)b;
    public static Vector2 operator -(Vector a, Vector2 b) => (Vector2)a - b;
    public static implicit operator Vector2(Vector vector) => new(vector.X, vector.Y);
    public static implicit operator Vector(Vector2 vector) => new(vector.X, vector.Y);
    #endregion

    #region operators with Size
    public static bool operator ==(Vector2 a, Size b) => a == (Vector2)b;
    public static bool operator ==(Size a, Vector2 b) => (Vector2)a == b;
    public static bool operator !=(Vector2 a, Size b) => a != (Vector2)b;
    public static bool operator !=(Size a, Vector2 b) => (Vector2)a != b;
    public static Vector2 operator +(Vector2 a, Size b) => a + (Vector2)b;
    public static Vector2 operator +(Size a, Vector2 b) => (Vector2)a + b;
    public static Vector2 operator -(Vector2 a, Size b) => a - (Vector2)b;
    public static Vector2 operator -(Size a, Vector2 b) => (Vector2)a - b;
    public static implicit operator Vector2(Size size) => new(size.Width, size.Height);
    public static implicit operator Size(Vector2 vector) => new(vector.X, vector.Y);
    #endregion

    #region operators with Point
    public static bool operator ==(Vector2 a, Point b) => a == (Vector2)b;
    public static bool operator ==(Point a, Vector2 b) => (Vector2)a == b;
    public static bool operator !=(Vector2 a, Point b) => a != (Vector2)b;
    public static bool operator !=(Point a, Vector2 b) => (Vector2)a != b;
    public static Vector2 operator +(Vector2 a, Point b) => a + (Vector2)b;
    public static Vector2 operator +(Point a, Vector2 b) => (Vector2)a + b;
    public static Vector2 operator -(Vector2 a, Point b) => a - (Vector2)b;
    public static Vector2 operator -(Point a, Vector2 b) => (Vector2)a - b;
    public static implicit operator Vector2(Point point) => new(point.X, point.Y);
    public static implicit operator Point(Vector2 vector) => new(vector.X, vector.Y);
    #endregion
    #endregion
    */

    public static Vector2 Lerp(Vector2 a, Vector2 b, double t) => LerpUnclamped(a, b, Math.Clamp(t, 0, 1));
    public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, double t) =>  new(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
    public static Vector2 Min(Vector2 a, Vector2 b) => new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
    public static Vector2 Max(Vector2 a, Vector2 b) => new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
    public static (Vector2, Vector2) MinMax(Vector2 a, Vector2 b)
    {
        Vector2 min = new();
        Vector2 max = new();

        (min.X, max.X) = a.X < b.X ? (a.X, b.X) : (b.X, a.X);
        (min.Y, max.Y) = a.Y < b.Y ? (a.Y, b.Y) : (b.Y, a.Y);

        return (min, max);
    }

    public static Vector3 Cross(Vector2 a, Vector2 b) => new(0, 0, a.X * b.Y - a.Y * b.X);
    public static double Dot(Vector2 a, Vector2 b) => a.X * b.X + a.Y * b.Y;
    //returns angles in degrees
    public static double Angle(Vector2 from, Vector2 to) => Vector3.Angle(from, to);
    //returns angles in degrees
    public static double SignedAngle(Vector2 from, Vector2 to, Vector2 axis) => Vector3.SignedAngle(from, to, axis);

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

    private static readonly Vector2 _zero = new(0, 0);
    private static readonly Vector2 _one = new(1, 1);
    private static readonly Vector2 _right = new(1, 0);
    private static readonly Vector2 _left = new(-1, 0);
    private static readonly Vector2 _up = new(0, 1);
    private static readonly Vector2 _down = new(0, -1);
    private static readonly Vector2 _positiveInfinity = new(double.PositiveInfinity, double.PositiveInfinity);
    private static readonly Vector2 _negativeInfinity = new(double.NegativeInfinity, double.NegativeInfinity);

    public static Vector2 Zero => _zero;
    public static Vector2 One => _one;
    public static Vector2 Right => _right;
    public static Vector2 Left => _left;
    public static Vector2 Up => _up;
    public static Vector2 Down => _down;
    public static Vector2 PositiveInfinity => _positiveInfinity;
    public static Vector2 NegativeInfinity => _negativeInfinity;
}
