using System.Globalization;
//using System.Windows.Media.Media3D;

namespace _3dStuff.Vectors;
public struct Vector3Int
{
    public enum Axis
    {
        X = Vector3.Axis.X,
        Y = Vector3.Axis.Y,
        Z = Vector3.Axis.Z
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Vector2Int XY => new(X, Y);
    public Vector2Int YX => new(Y, X);
    public Vector2Int XZ => new(X, Z);
    public Vector2Int ZX => new(Z, X);
    public Vector2Int YZ => new(Y, Z);
    public Vector2Int ZY => new(Z, Y);

    public Vector3Int() : this(0, 0, 0) { }
    public Vector3Int(int x, int y, int z) => (X, Y, Z) = (x, y, z);
    public Vector3Int(Vector3Int other) => this = other;

    public int GetAxis(Axis axis) => axis switch
    {
        Axis.X => X,
        Axis.Y => Y,
        Axis.Z => Z,
        _ => throw new InvalidOperationException("Unknown axis."),
    };

    public Vector3 Scale(Vector3 other) => ((Vector3)this).Scale(other);
    public Vector3Int ScaleInt(Vector3Int other) => new(X * other.X, Y * other.Y, Z * other.Z);
    public int SqrMagnitudeInt() => X * X + Y * Y + Z * Z;
    public double SqrMagnitude() => 1.0 * X * X + 1.0 * Y * Y + 1.0 * Z * Z;
    public double Magnitude() => Math.Sqrt(SqrMagnitude());

    public Vector3 Normalized()
    {
        double sqrMag = SqrMagnitude();
        if(sqrMag > 0.0)
            return this / Math.Sqrt(sqrMag);
        else
            return Vector3Int._zero;
    }
    public int SqrDistanceInt(Vector3Int b) => (b - this).SqrMagnitudeInt();
    public double SqrDistance(Vector3 b) => (b - this).SqrMagnitude();
    public double Distance(Vector3 b) => (b - this).Magnitude();
    public Vector3 Reflect(Vector3 normal) => ((Vector3)this).Reflect(normal);
    public Vector3 Project(Vector3 vector) => ((Vector3)this).Project(vector);
    public Vector3 ProjectOnPlane(Vector3 planeNormal) => ((Vector3)this).ProjectOnPlane(planeNormal);
    public Vector3 ClampMagnitude(float maxLength) => ((Vector3)this).ClampMagnitude(maxLength);

    public bool Equals(Vector3Int other) => this == other;
    public override bool Equals(object? obj) => obj is Vector3Int other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    #region local operators
    public static bool operator ==(Vector3Int a, Vector3Int b) => (a.X, a.Y, a.Z) == (b.X, b.Y, b.Z);
    public static bool operator !=(Vector3Int a, Vector3Int b) => (a == b) == false;

    public static Vector3Int operator +(Vector3Int a, Vector3Int b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3Int operator -(Vector3Int a, Vector3Int b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3Int operator -(Vector3Int a) => new(-a.X, -a.Y, -a.Z);
    public static Vector3Int operator *(Vector3Int a, int b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Vector3Int operator *(int a, Vector3Int b) => b * a;

    public static Vector3 operator *(Vector3Int a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator *(double a, Vector3Int b) => b * a;
    public static Vector3 operator /(Vector3Int a, double b) => new(a.X / b, a.Y / b, a.Z / b);

    public static implicit operator Vector3Int((int x, int y, int z) tuple) => new(tuple.x, tuple.y, tuple.z);
    public static implicit operator (int x, int y, int z)(Vector3Int vector) => (vector.X, vector.Y, vector.Z);
    public static implicit operator Vector3Int(Tuple<int, int, int> tuple) => new(tuple.Item1, tuple.Item2, tuple.Item3);
    public static implicit operator Tuple<int, int, int>(Vector3Int vector) => Tuple.Create(vector.X, vector.Y, vector.Z);
    #endregion

    #region operators with Vector3
    public static bool operator ==(Vector3Int a, Vector3 b) => (Vector3)a == b;
    public static bool operator ==(Vector3 a, Vector3Int b) => a == (Vector3)b;
    public static bool operator !=(Vector3Int a, Vector3 b) => (Vector3)a != b;
    public static bool operator !=(Vector3 a, Vector3Int b) => a != (Vector3)b;
    public static Vector3 operator +(Vector3Int a, Vector3 b) => (Vector3)a + b;
    public static Vector3 operator +(Vector3 a, Vector3Int b) => a + (Vector3)b;
    public static Vector3 operator -(Vector3Int a, Vector3 b) => (Vector3)a - b;
    public static Vector3 operator -(Vector3 a, Vector3Int b) => a - (Vector3)b;
    public static explicit operator Vector3Int(Vector3 vector) => new((int)vector.X, (int)vector.Y, (int)vector.Z);
    public static implicit operator Vector3(Vector3Int vector) => new(vector.X, vector.Y, vector.Z);
    #endregion

    /* System.Windows.Media.Media3D
    #region System.Windows.Media.Media3D
    #region operators with Quaternion
    public static Vector3 operator *(Quaternion quaternion, Vector3Int vector) => quaternion * (Vector3)vector;
    #endregion

    #region operators with Vector2D
    public static bool operator ==(Vector3Int a, Vector3D b) => a == (Vector3)b;
    public static bool operator ==(Vector3D a, Vector3Int b) => (Vector3)a == b;
    public static bool operator !=(Vector3Int a, Vector3D b) => a != (Vector3)b;
    public static bool operator !=(Vector3D a, Vector3Int b) => (Vector3)a != b;
    public static Vector3 operator +(Vector3Int a, Vector3D b) => a + (Vector3)b;
    public static Vector3 operator +(Vector3D a, Vector3Int b) => (Vector3)a + b;
    public static Vector3 operator -(Vector3Int a, Vector3D b) => a - (Vector3)b;
    public static Vector3 operator -(Vector3D a, Vector3Int b) => (Vector3)a - b;
    public static explicit operator Vector3Int(Vector3D vector) => new((int)vector.X, (int)vector.Y, (int)vector.Z);
    public static implicit operator Vector3D(Vector3Int vector) => new(vector.X, vector.Y, vector.Z);
    #endregion

    #region operators with Size2D
    public static bool operator ==(Vector3Int a, Size3D b) => a == (Vector3)b;
    public static bool operator ==(Size3D a, Vector3Int b) => (Vector3)a == b;
    public static bool operator !=(Vector3Int a, Size3D b) => a != (Vector3)b;
    public static bool operator !=(Size3D a, Vector3Int b) => (Vector3)a != b;
    public static Vector3 operator +(Vector3Int a, Size3D b) => a + (Vector3)b;
    public static Vector3 operator +(Size3D a, Vector3Int b) => (Vector3)a + b;
    public static Vector3 operator -(Vector3Int a, Size3D b) => a - (Vector3)b;
    public static Vector3 operator -(Size3D a, Vector3Int b) => (Vector3)a - b;
    public static explicit operator Vector3Int(Size3D vector) => new((int)vector.X, (int)vector.Y, (int)vector.Z);
    public static implicit operator Size3D(Vector3Int vector) => new(vector.X, vector.Y, vector.Z);
    #endregion

    #region operators with Point3D
    public static bool operator ==(Vector3Int a, Point3D b) => a == (Vector3)b;
    public static bool operator ==(Point3D a, Vector3Int b) => (Vector3)a == b;
    public static bool operator !=(Vector3Int a, Point3D b) => a != (Vector3)b;
    public static bool operator !=(Point3D a, Vector3Int b) => (Vector3)a != b;
    public static Vector3 operator +(Vector3Int a, Point3D b) => a + (Vector3)b;
    public static Vector3 operator +(Point3D a, Vector3Int b) => (Vector3)a + b;
    public static Vector3 operator -(Vector3Int a, Point3D b) => a - (Vector3)b;
    public static Vector3 operator -(Point3D a, Vector3Int b) => (Vector3)a - b;
    public static explicit operator Vector3Int(Point3D vector) => new((int)vector.X, (int)vector.Y, (int)vector.Z);
    public static implicit operator Point3D(Vector3Int vector) => new(vector.X, vector.Y, vector.Z);
    #endregion
    #endregion
    */

    public static Vector3Int Min(Vector3Int a, Vector3Int b) =>
        new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
    public static Vector3Int Max(Vector3Int a, Vector3Int b) =>
        new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
    public static (Vector3Int, Vector3Int) MinMax(Vector3Int a, Vector3Int b)
    {
        Vector3Int min = new();
        Vector3Int max = new();

        (min.X, max.X) = a.X < b.X ? (a.X, b.X) : (b.X, a.X);
        (min.Y, max.Y) = a.Y < b.Y ? (a.Y, b.Y) : (b.Y, a.Y);
        (min.Z, max.Z) = a.Z < b.Z ? (a.Z, b.Z) : (b.Z, a.Z);

        return (min, max);
    }

    public static Vector3Int Cross(Vector3Int a, Vector3Int b) =>
        new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    public static int Dot(Vector3Int a, Vector3Int b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    public override string ToString() => ToString(null, null);
    public string ToString(string? format) => ToString(format, null);
    public string ToString(IFormatProvider? formatProvider) => ToString(null, formatProvider);
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if(string.IsNullOrEmpty(format))
            format = "F";
        if(formatProvider == null)
            formatProvider = CultureInfo.InvariantCulture.NumberFormat;
        return $"{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {X.ToString(format, formatProvider)}";
    }

    private static readonly Vector3Int _zero = new(0, 0, 0);
    private static readonly Vector3Int _one = new(1, 1, 1);
    private static readonly Vector3Int _right = new(1, 0, 0);
    private static readonly Vector3Int _left = new(-1, 0, 0);
    private static readonly Vector3Int _up = new(0, 1, 0);
    private static readonly Vector3Int _down = new(0, -1, 0);
    private static readonly Vector3Int _forward = new(0, 0, 1);
    private static readonly Vector3Int _back = new(0, 0, -1);

    public static Vector3Int Zero => _zero;
    public static Vector3Int One => _one;
    public static Vector3Int Right => _right;
    public static Vector3Int Left => _left;
    public static Vector3Int Up => _up;
    public static Vector3Int Down => _down;
    public static Vector3Int Forward => _forward;
    public static Vector3Int Back => _back;
}
