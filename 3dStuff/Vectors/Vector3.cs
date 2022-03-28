using System.Globalization;
//using System.Windows.Media.Media3D;

namespace _3dStuff.Vectors;
public struct Vector3
{
    public enum Axis
    {
        X,
        Y,
        Z
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector2 XY => new(X, Y);
    public Vector2 YX => new(Y, X);
    public Vector2 XZ => new(X, Z);
    public Vector2 ZX => new(Z, X);
    public Vector2 YZ => new(Y, Z);
    public Vector2 ZY => new(Z, Y);

    public Vector3() : this(0, 0, 0) { }
    public Vector3(double x, double y, double z) => (X, Y, Z) = (x, y, z);
    public Vector3(Vector3 other) => (X, Y, Z) = (other.X, other.Y, other.Z);

    public double GetAxis(Axis axis) => axis switch
    {
        Axis.X => X,
        Axis.Y => Y,
        Axis.Z => Z,
        _ => throw new InvalidOperationException("Unknown axis."),
    };

    public Vector3 Scale(Vector3 other) => new(X * other.X, Y * other.Y, Z * other.Z);
    public double SqrMagnitude() => X * X + Y * Y + Z * Z;
    public double Magnitude() => Math.Sqrt(SqrMagnitude());

    public void Normalize()
    {
        Vector3 normilized = Normalized();
        (X, Y, Z) = (normilized.X, normilized.Y, normilized.Z);
    }
    public Vector3 Normalized()
    {
        double sqrMag = SqrMagnitude();
        if(sqrMag > 0.0)
            return this / Math.Sqrt(sqrMag);
        else
            return Vector3._zero;
    }
    public double SqrDistance(Vector3 b) => (b - this).SqrMagnitude();
    public double Distance(Vector3 b) => (b - this).Magnitude();
    public Vector3 Reflect(Vector3 normal)
    {
        double factor = -2F * Vector3.Dot(normal, this);
        return normal * factor + this;
    }
    public Vector3 Project(Vector3 vector)
    {
        double sqrMag = vector.SqrMagnitude();
        if(sqrMag == 0)
            return Vector3._zero;
        else
            return vector * Vector3.Dot(this, vector) / sqrMag;
    }
    public Vector3 ProjectOnPlane(Vector3 planeNormal)
    {
        double sqrMag = planeNormal.SqrMagnitude();
        if(sqrMag == 0)
            return this;
        else
            return (this - planeNormal) * Vector3.Dot(this, planeNormal) / sqrMag;
    }
    public Vector3 ClampMagnitude(float maxLength)
    {
        double sqrmag = SqrMagnitude();
        if(sqrmag > maxLength * maxLength)
            return this / Math.Sqrt(sqrmag) * maxLength;
        else
            return this;
    }

    public bool Equals(Vector3 other) => this == other;
    public override bool Equals(object? obj) => obj is Vector3 other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    #region local operators
    public static bool operator ==(Vector3 a, Vector3 b) => (a.X, a.Y, a.Z) == (b.X, b.Y, b.Z);
    public static bool operator !=(Vector3 a, Vector3 b) => (a == b) == false;

    public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3 operator -(Vector3 a) => new(-a.X, -a.Y, -a.Z);
    public static Vector3 operator *(double a, Vector3 b) => b * a;
    public static Vector3 operator *(Vector3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator /(Vector3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public static implicit operator Vector3((double x, double y, double z) tuple) => new(tuple.x, tuple.y, tuple.z);
    public static implicit operator (double x, double y, double z)(Vector3 vector) => (vector.X, vector.Y, vector.Z);
    public static implicit operator Vector3(Tuple<double, double, double> tuple) => new(tuple.Item1, tuple.Item2, tuple.Item3);
    public static implicit operator Tuple<double, double, double>(Vector3 vector) => Tuple.Create(vector.X, vector.Y, vector.Z);
    #endregion

    /* System.Windows.Media.Media3D
    #region System.Windows.Media.Media3D
    #region operators with Quaternion
    public static Vector3 operator *(Quaternion quaternion, Vector3 vector)
    {
        double num = quaternion.X * 2;
        double num2 = quaternion.Y * 2;
        double num3 = quaternion.Z * 2;
        double num4 = quaternion.X * num;
        double num5 = quaternion.Y * num2;
        double num6 = quaternion.Z * num3;
        double num7 = quaternion.X * num2;
        double num8 = quaternion.X * num3;
        double num9 = quaternion.Y * num3;
        double num10 = quaternion.W * num;
        double num11 = quaternion.W * num2;
        double num12 = quaternion.W * num3;
        Vector3 result = new();
        result.X = (1f - (num5 + num6)) * vector.X + (num7 - num12) * vector.Y + (num8 + num11) * vector.Z;
        result.Y = (num7 + num12) * vector.X + (1f - (num4 + num6)) * vector.Y + (num9 - num10) * vector.Z;
        result.Z = (num8 - num11) * vector.X + (num9 + num10) * vector.Y + (1f - (num4 + num5)) * vector.Z;
        return result;
    }
    #endregion

    #region operators with Vector3D
    public static bool operator ==(Vector3 a, Vector3D b) => a == (Vector3)b;
    public static bool operator ==(Vector3D a, Vector3 b) => (Vector3)a == b;
    public static bool operator !=(Vector3 a, Vector3D b) => a != (Vector3)b;
    public static bool operator !=(Vector3D a, Vector3 b) => (Vector3)a != b;
    public static Vector3 operator +(Vector3 a, Vector3D b) => a + (Vector3)b;
    public static Vector3 operator +(Vector3D a, Vector3 b) => (Vector3)a + b;
    public static Vector3 operator -(Vector3 a, Vector3D b) => a - (Vector3)b;
    public static Vector3 operator -(Vector3D a, Vector3 b) => (Vector3)a - b;
    public static implicit operator Vector3(Vector3D vector) => new(vector.X, vector.Y, vector.Z);
    public static implicit operator Vector3D(Vector3 vector) => new(vector.X, vector.Y, vector.Z);
    #endregion

    #region operators with Size3D
    public static bool operator ==(Vector3 a, Size3D b) => a == (Vector3)b;
    public static bool operator ==(Size3D a, Vector3 b) => (Vector3)a == b;
    public static bool operator !=(Vector3 a, Size3D b) => a != (Vector3)b;
    public static bool operator !=(Size3D a, Vector3 b) => (Vector3)a != b;
    public static Vector3 operator +(Vector3 a, Size3D b) => a + (Vector3)b;
    public static Vector3 operator +(Size3D a, Vector3 b) => (Vector3)a + b;
    public static Vector3 operator -(Vector3 a, Size3D b) => a - (Vector3)b;
    public static Vector3 operator -(Size3D a, Vector3 b) => (Vector3)a - b;
    public static implicit operator Vector3(Size3D vector) => new(vector.X, vector.Y, vector.Z);
    public static implicit operator Size3D(Vector3 vector) => new(vector.X, vector.Y, vector.Z);
    #endregion

    #region operators with Point3D
    public static bool operator ==(Vector3 a, Point3D b) => a == (Vector3)b;
    public static bool operator ==(Point3D a, Vector3 b) => (Vector3)a == b;
    public static bool operator !=(Vector3 a, Point3D b) => a != (Vector3)b;
    public static bool operator !=(Point3D a, Vector3 b) => (Vector3)a != b;
    public static Vector3 operator +(Vector3 a, Point3D b) => a + (Vector3)b;
    public static Vector3 operator +(Point3D a, Vector3 b) => (Vector3)a + b;
    public static Vector3 operator -(Vector3 a, Point3D b) => a - (Vector3)b;
    public static Vector3 operator -(Point3D a, Vector3 b) => (Vector3)a - b;
    public static implicit operator Vector3(Point3D vector) => new(vector.X, vector.Y, vector.Z);
    public static implicit operator Point3D(Vector3 vector) => new(vector.X, vector.Y, vector.Z);
    #endregion
    #endregion
    */

    public static Vector3 Lerp(Vector3 a, Vector3 b, double t) => LerpUnclamped(a, b, Math.Clamp(t, 0, 1));
    public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, double t) =>
        new(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
    public static Vector3 Min(Vector3 a, Vector3 b) =>
        new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
    public static Vector3 Max(Vector3 a, Vector3 b) =>
        new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
    public static (Vector3, Vector3) MinMax(Vector3 a, Vector3 b)
    {
        Vector3 min = new();
        Vector3 max = new();

        (min.X, max.X) = a.X < b.X ? (a.X, b.X) : (b.X, a.X);
        (min.Y, max.Y) = a.Y < b.Y ? (a.Y, b.Y) : (b.Y, a.Y);
        (min.Z, max.Z) = a.Z < b.Z ? (a.Z, b.Z) : (b.Z, a.Z);

        return (min, max);
    }
    
    public static Vector3 Cross(Vector3 a, Vector3 b) =>
        new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    public static double Dot(Vector3 a, Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    //returns angles in degrees
    public static double Angle(Vector3 from, Vector3 to)
    {
        double denominator = Math.Sqrt(from.SqrMagnitude() * to.SqrMagnitude());
        if(denominator == 0)
            return 0;

        double dot = Math.Clamp(Vector3.Dot(from, to) / denominator, -1, 1);
        return Math.Acos(dot) * 180 / Math.PI;
    }
    //returns angles in degrees
    public static double SignedAngle(Vector3 from, Vector3 to, Vector3 axis) =>
        Angle(from, to) * Math.Sign(Vector3.Dot(axis, Vector3.Cross(from, to)));

    public override string ToString() => ToString(null, null);
    public string ToString(string? format) => ToString(format, null);
    public string ToString(IFormatProvider? formatProvider) => ToString(null, formatProvider);
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if(string.IsNullOrEmpty(format))
            format = "F";
        if(formatProvider == null)
            formatProvider = CultureInfo.InvariantCulture.NumberFormat;
        return $"{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}";
    }

    private static readonly Vector3 _zero = new(0, 0, 0);
    private static readonly Vector3 _one = new(1, 1, 1);
    private static readonly Vector3 _right = new(1, 0, 0);
    private static readonly Vector3 _left = new(-1, 0, 0);
    private static readonly Vector3 _up = new(0, 1, 0);
    private static readonly Vector3 _down = new(0, -1, 0);
    private static readonly Vector3 _forward = new(0, 0, 1);
    private static readonly Vector3 _back = new(0, 0, -1);
    private static readonly Vector3 _positiveInfinity = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
    private static readonly Vector3 _negativeInfinity = new(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);

    public static Vector3 Zero => _zero;
    public static Vector3 One => _one;
    public static Vector3 Right => _right;
    public static Vector3 Left => _left;
    public static Vector3 Up => _up;
    public static Vector3 Down => _down;
    public static Vector3 Forward => _forward;
    public static Vector3 Back => _back;
    public static Vector3 PositiveInfinity => _positiveInfinity;
    public static Vector3 NegativeInfinity => _negativeInfinity;
}
