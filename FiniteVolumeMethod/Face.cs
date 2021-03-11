using System;
using System.Collections.Generic;

namespace ConsoleGameTest_00
{
  public class Face
  {
    public int ID;
    public Volume Volume1;
    public Volume Volume2;

    public double Area;
    public double Perimeter;
    public Vector3D Center;
    public Vector3D Normal;

    public Face(int id)
    {
      ID = id;
    }

    public virtual void CalculateArea() {}
    public virtual void CalculatePerimeter() {}
    public virtual void CalculateCenter() {}
  }

  public class TriFace : Face
  {
    public Vertex Vertex1;
    public Vertex Vertex2;
    public Vertex Vertex3;
    public Edge Edge1;
    public Edge Edge2;
    public Edge Edge3;

    public TriFace(int id) : base(id) {}

    // Area calculation
    // 1/2 * |a x b + b x c + c x a|
    public override void CalculateArea()
    {
      Normal = 0.5 * (Vertex1.Position.Cross(Vertex2.Position) + Vertex2.Position.Cross(Vertex3.Position) + Vertex3.Position.Cross(Vertex1.Position));
      Area = Normal.Length();
      Normal /= Area;
      if(Area < 0) Area *= -1.0;
    }

    // Perimeter calculation
    public override void CalculatePerimeter()
    {
      Perimeter = Edge1.Length + Edge2.Length + Edge3.Length;
    }

    // Center calculation
    public override void CalculateCenter()
    {
      Center = (Vertex1.Position + Vertex2.Position + Vertex3.Position) * Constants.OneThird;
    }

    // Face creating methods
    public static TriFace CreateFace(Vertex v1, Vertex v2, Vertex v3, Edge e1, Edge e2, Edge e3)
    {
      TriFace face = new TriFace(IDCounter.ClaimID());

      face.Vertex1 = v1;
      v1.Faces.Add(face);
      face.Vertex2 = v2;
      v2.Faces.Add(face);
      face.Vertex3 = v3;
      v3.Faces.Add(face);
      face.Edge1 = e1;
      e1.Faces.Add(face);
      face.Edge2 = e2;
      e2.Faces.Add(face);
      face.Edge3 = e3;
      e3.Faces.Add(face);
      face.CalculateCenter();
      face.CalculatePerimeter();
      face.CalculateArea();

      return face;
    }
  }

  public class QuadFace : Face
  {
    public Vertex Vertex1;
    public Vertex Vertex2;
    public Vertex Vertex3;
    public Vertex Vertex4;
    public Edge Edge1;
    public Edge Edge2;
    public Edge Edge3;
    public Edge Edge4;

    public QuadFace(int id) : base(id) {}

    // Area calculation
    // 1/2 * |a x b + b x c + c x d + d x a|
    public override void CalculateArea()
    {
      Normal = 0.5 * (Vertex1.Position.Cross(Vertex2.Position) + Vertex2.Position.Cross(Vertex3.Position) + Vertex3.Position.Cross(Vertex4.Position) + Vertex4.Position.Cross(Vertex1.Position));
      Area = Normal.Length();
      Normal /= Area;
      if(Area < 0) Area *= -1.0;
    }

    // Perimeter calculation
    public override void CalculatePerimeter()
    {
      Perimeter = Edge1.Length + Edge2.Length + Edge3.Length + Edge4.Length;
    }

    // Center calculation
    public override void CalculateCenter()
    {
      Center = (Vertex1.Position + Vertex2.Position + Vertex3.Position + Vertex4.Position) * 0.25;
    }

    // Face creating methods
    public static QuadFace CreateFace(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Edge e1, Edge e2, Edge e3, Edge e4)
    {
      QuadFace face = new QuadFace(IDCounter.ClaimID());

      face.Vertex1 = v1;
      v1.Faces.Add(face);
      face.Vertex2 = v2;
      v2.Faces.Add(face);
      face.Vertex3 = v3;
      v3.Faces.Add(face);
      face.Vertex4 = v4;
      v4.Faces.Add(face);
      face.Edge1 = e1;
      e1.Faces.Add(face);
      face.Edge2 = e2;
      e2.Faces.Add(face);
      face.Edge3 = e3;
      e3.Faces.Add(face);
      face.Edge4 = e4;
      e4.Faces.Add(face);
      face.CalculateCenter();
      face.CalculatePerimeter();
      face.CalculateArea();

      return face;
    }
  }
}