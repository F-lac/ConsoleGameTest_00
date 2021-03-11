using System;

namespace ConsoleGameTest_00
{
    public struct Matrix3D
    {
        public double XX;
        public double XY;
        public double XZ;
        public double YX;
        public double YY;
        public double YZ;
        public double ZX;
        public double ZY;
        public double ZZ;

        public Matrix3D(double xx, double xy, double xz, double yx, double yy, double yz, double zx, double zy, double zz)
        {
            XX = xx;
            XY = xy;
            XZ = xz;
            YX = yx;
            YY = yy;
            YZ = yz;
            ZX = zx;
            ZY = zy;
            ZZ = zz;
        }

        public override string ToString() => "[(" + XX + ", " + XY + ", " + XZ + "), (" + YX + ", " + YY + ", " + YZ + "), (" + ZX + ", " + ZY + ", " + ZZ + ")]";

        public Vector3D RowX => new Vector3D(this.XX, this.XY, this.XZ);
        public Vector3D RowY => new Vector3D(this.YX, this.YY, this.YZ);
        public Vector3D RowZ => new Vector3D(this.ZX, this.ZY, this.ZZ);
        public Vector3D ColX => new Vector3D(this.XX, this.YX, this.ZX);
        public Vector3D ColY => new Vector3D(this.XY, this.YY, this.ZY);
        public Vector3D ColZ => new Vector3D(this.XZ, this.YZ, this.ZZ);

        public static Matrix3D operator +(Matrix3D m1) => m1;
        public static Matrix3D operator -(Matrix3D m1) => new Matrix3D(-m1.XX, -m1.XY, -m1.XZ, -m1.YX, -m1.YY, -m1.YZ, -m1.ZX, -m1.ZY, -m1.ZZ);

        public static Matrix3D operator *(Matrix3D m1, double a1) => new Matrix3D(m1.XX * a1, m1.XY * a1, m1.XZ * a1, m1.YX * a1, m1.YY * a1, m1.YZ * a1, m1.ZX * a1, m1.ZY * a1, m1.ZZ * a1);
        public static Matrix3D operator *(double a1, Matrix3D m1) => new Matrix3D(m1.XX * a1, m1.XY * a1, m1.XZ * a1, m1.YX * a1, m1.YY * a1, m1.YZ * a1, m1.ZX * a1, m1.ZY * a1, m1.ZZ * a1);
        public static Matrix3D operator /(Matrix3D m1, double a1) => m1 * (1 / a1);

        public static Matrix3D operator +(Matrix3D m1, Matrix3D m2) => new Matrix3D(m1.XX + m2.XX, m1.XY + m2.XY, m1.XZ + m2.XZ, m1.YX + m2.YX, m1.YY + m2.YY, m1.YZ + m2.YZ, m1.ZX + m2.ZX, m1.ZY + m2.ZY, m1.ZZ + m2.ZZ);
        public static Matrix3D operator -(Matrix3D m1, Matrix3D m2) => m1 + (-m2);

        public Matrix3D Dot(Matrix3D m2) => new Matrix3D(
            this.RowX.Dot(m2.ColX),
            this.RowX.Dot(m2.ColY),
            this.RowX.Dot(m2.ColZ),
            this.RowY.Dot(m2.ColX),
            this.RowY.Dot(m2.ColY),
            this.RowY.Dot(m2.ColZ),
            this.RowZ.Dot(m2.ColX),
            this.RowZ.Dot(m2.ColY),
            this.RowZ.Dot(m2.ColZ)
        );

        public Matrix3D Transpose() => new Matrix3D(XX,YX,ZX,XY,YY,ZY,XZ,YZ,ZZ);

        public Vector3D Dot(Vector3D v2) => new Vector3D(
            this.RowX.Dot(v2),
            this.RowY.Dot(v2),
            this.RowZ.Dot(v2)
        );

        public static Matrix3D Rotation(int axis, double th)
        {
            switch(axis)
            {
                case 0:
                    return new Matrix3D(1,0,0,0,Math.Cos(th),-Math.Sin(th),0,Math.Sin(th),Math.Cos(th));
                case 1:
                    return new Matrix3D(Math.Cos(th),0,Math.Sin(th),0,1,0,-Math.Sin(th),0,Math.Cos(th));
                case 2:
                    return new Matrix3D(Math.Cos(th),-Math.Sin(th),0,Math.Sin(th),Math.Cos(th),0,0,0,1);
                default:
                    return new Matrix3D(1,0,0,0,1,0,0,0,1);
            }
        }
    }
}