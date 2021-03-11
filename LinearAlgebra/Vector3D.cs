using System;

namespace ConsoleGameTest_00
{
    public struct Vector3D
    {
        public double X;
        public double Y;
        public double Z;

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString() => "(" + X + ", " + Y + ", " + Z + ")";

        public static Vector3D operator +(Vector3D v1) => v1;
        public static Vector3D operator -(Vector3D v1) => new Vector3D(-v1.X, -v1.Y, -v1.Z);

        public static Vector3D operator *(Vector3D v1, double a1) => new Vector3D(v1.X * a1, v1.Y * a1, v1.Z * a1);
        public static Vector3D operator *(double a1, Vector3D v1) => new Vector3D(v1.X * a1, v1.Y * a1, v1.Z * a1);
        public static Vector3D operator /(Vector3D v1, double a1) => v1 * (1 / a1);

        public static Vector3D operator +(Vector3D v1, Vector3D v2) => new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vector3D operator -(Vector3D v1, Vector3D v2) => new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        public double Dot(Vector3D v2) => this.X * v2.X + this.Y * v2.Y + this.Z * v2.Z;
        public Vector3D Cross(Vector3D v2) => new Vector3D(this.Y * v2.Z - this.Z * v2.Y, this.Z * v2.X - this.X * v2.Z, this.X * v2.Y - this.Y * v2.X);

        public double LengthSqr() => this.Dot(this);
        public double Length() => Math.Sqrt(LengthSqr());

        public Vector3D Dot(Matrix3D m2) => new Vector3D(
            this.Dot(m2.ColX),
            this.Dot(m2.ColY),
            this.Dot(m2.ColZ)
        );

        public Matrix3D Dyad(Vector3D v2) => new Matrix3D(
            this.X * v2.X,
            this.X * v2.Y,
            this.X * v2.Z,
            this.Y * v2.X,
            this.Y * v2.Y,
            this.Y * v2.Z,
            this.Z * v2.X,
            this.Z * v2.Y,
            this.Z * v2.Z
        );

        public Vector3D Normalize() => this / this.Length();
    }
}