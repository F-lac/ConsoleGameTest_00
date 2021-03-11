using System;
using System.Collections.Generic;

namespace ConsoleGameTest_00
{
  public class Volume
  {
    public int ID;

    public double Volum;
    public double Surface;
    public Vector3D Center;

    public Volume(int id)
    {
      ID = id;
    }

    public virtual void CalculateVolume() {}
    public virtual void CalculateSurface() {}
    public virtual void CalculateCenter() {}
    public virtual string ToSTL() { return ""; }
  }

  public class HexVolume : Volume
  {
    public Vertex Vertex1;
    public Vertex Vertex2;
    public Vertex Vertex3;
    public Vertex Vertex4;
    public Vertex Vertex5;
    public Vertex Vertex6;
    public Vertex Vertex7;
    public Vertex Vertex8;
    public Edge Edge1;
    public Edge Edge2;
    public Edge Edge3;
    public Edge Edge4;
    public Edge Edge5;
    public Edge Edge6;
    public Edge Edge7;
    public Edge Edge8;
    public Edge Edge9;
    public Edge Edge10;
    public Edge Edge11;
    public Edge Edge12;
    public QuadFace Face1;
    public QuadFace Face2;
    public QuadFace Face3;
    public QuadFace Face4;
    public QuadFace Face5;
    public QuadFace Face6;

    public HexVolume(int id) : base(id) { }

    public override void CalculateVolume()
    {
      Vector3D Face1Normal = (Face1.Center - Center).Dot(Face1.Normal) > 0 ? Face1.Normal : -Face1.Normal;
      Vector3D Face2Normal = (Face2.Center - Center).Dot(Face2.Normal) > 0 ? Face2.Normal : -Face2.Normal;
      Vector3D Face3Normal = (Face3.Center - Center).Dot(Face3.Normal) > 0 ? Face3.Normal : -Face3.Normal;
      Vector3D Face4Normal = (Face4.Center - Center).Dot(Face4.Normal) > 0 ? Face4.Normal : -Face4.Normal;
      Vector3D Face5Normal = (Face5.Center - Center).Dot(Face5.Normal) > 0 ? Face5.Normal : -Face5.Normal;
      Vector3D Face6Normal = (Face6.Center - Center).Dot(Face6.Normal) > 0 ? Face6.Normal : -Face6.Normal;

      Volum = Constants.OneThird * (
        Face1.Center.Dot(Face1Normal)*Face1.Area + 
        Face2.Center.Dot(Face2Normal)*Face2.Area + 
        Face3.Center.Dot(Face3Normal)*Face3.Area + 
        Face4.Center.Dot(Face4Normal)*Face4.Area + 
        Face5.Center.Dot(Face5Normal)*Face5.Area + 
        Face6.Center.Dot(Face6Normal)*Face6.Area);
    }

    public override void CalculateSurface()
    {
      Surface = Face1.Area + Face2.Area + Face3.Area + Face4.Area + Face5.Area + Face6.Area;
    }

    public override void CalculateCenter()
    {
      Center = (Vertex1.Position + Vertex2.Position + Vertex3.Position + Vertex4.Position + Vertex5.Position + Vertex6.Position + Vertex7.Position + Vertex8.Position) * 0.125;
    }

    public static HexVolume CreateVolume(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vertex v5, Vertex v6, Vertex v7, Vertex v8)
    {
      HexVolume vol = new HexVolume(IDCounter.ClaimID());
      return vol;
    }

    public static HexVolume CreateVolume(QuadFace f1, QuadFace f2, QuadFace f3, QuadFace f4, QuadFace f5, QuadFace f6)
    {
      HexVolume vol = new HexVolume(IDCounter.ClaimID());

      List<Face> faces = new List<Face>() {f1, f2, f3, f4, f5, f6};
      List<Edge> edges = new List<Edge>();
      List<Vertex> vertices = new List<Vertex>();

      foreach(Face f in faces)
      {
        if(vol.Face1 == null && f is QuadFace) {
          vol.Face1 = f as QuadFace;
        } else if(vol.Face2 == null && f is QuadFace) {
          vol.Face2 = f as QuadFace;
        } else if(vol.Face3 == null && f is QuadFace) {
          vol.Face3 = f as QuadFace;
        } else if(vol.Face4 == null && f is QuadFace) {
          vol.Face4 = f as QuadFace;
        } else if(vol.Face5 == null && f is QuadFace) {
          vol.Face5 = f as QuadFace;
        } else if(vol.Face6 == null && f is QuadFace) {
          vol.Face6 = f as QuadFace;
        } else {
          Console.WriteLine("Error during volume #" + vol.ID + "creation: Not expected face type.");
        }

        if(f.Volume1 == null)
        {
          f.Volume1 = vol;
        } else if(f.Volume2 == null){
          f.Volume2 = vol;
        } else {
          Console.WriteLine("Error during volume #" + vol.ID + "creation: Face #" + f.ID + " already has.");
        }

        if(f is TriFace)
        {
          if(!edges.Contains((f as TriFace).Edge1)) edges.Add((f as TriFace).Edge1);
          if(!edges.Contains((f as TriFace).Edge2)) edges.Add((f as TriFace).Edge2);
          if(!edges.Contains((f as TriFace).Edge3)) edges.Add((f as TriFace).Edge3);
          if(!vertices.Contains((f as TriFace).Vertex1)) vertices.Add((f as TriFace).Vertex1);
          if(!vertices.Contains((f as TriFace).Vertex2)) vertices.Add((f as TriFace).Vertex2);
          if(!vertices.Contains((f as TriFace).Vertex3)) vertices.Add((f as TriFace).Vertex3);
        }

        if(f is QuadFace)
        {
          if(!edges.Contains((f as QuadFace).Edge1)) edges.Add((f as QuadFace).Edge1);
          if(!edges.Contains((f as QuadFace).Edge2)) edges.Add((f as QuadFace).Edge2);
          if(!edges.Contains((f as QuadFace).Edge3)) edges.Add((f as QuadFace).Edge3);
          if(!edges.Contains((f as QuadFace).Edge4)) edges.Add((f as QuadFace).Edge4);
          if(!vertices.Contains((f as QuadFace).Vertex1)) vertices.Add((f as QuadFace).Vertex1);
          if(!vertices.Contains((f as QuadFace).Vertex2)) vertices.Add((f as QuadFace).Vertex2);
          if(!vertices.Contains((f as QuadFace).Vertex3)) vertices.Add((f as QuadFace).Vertex3);
          if(!vertices.Contains((f as QuadFace).Vertex4)) vertices.Add((f as QuadFace).Vertex4);
        }
      }

      vol.Vertex1 = vertices[0];
      if(!vol.Vertex1.Volumes.Contains(vol)) vol.Vertex1.Volumes.Add(vol);
      vol.Vertex2 = vertices[1];
      if(!vol.Vertex2.Volumes.Contains(vol)) vol.Vertex2.Volumes.Add(vol);
      vol.Vertex3 = vertices[2];
      if(!vol.Vertex3.Volumes.Contains(vol)) vol.Vertex3.Volumes.Add(vol);
      vol.Vertex4 = vertices[3];
      if(!vol.Vertex4.Volumes.Contains(vol)) vol.Vertex4.Volumes.Add(vol);
      vol.Vertex5 = vertices[4];
      if(!vol.Vertex5.Volumes.Contains(vol)) vol.Vertex5.Volumes.Add(vol);
      vol.Vertex6 = vertices[5];
      if(!vol.Vertex6.Volumes.Contains(vol)) vol.Vertex6.Volumes.Add(vol);
      vol.Vertex7 = vertices[6];
      if(!vol.Vertex7.Volumes.Contains(vol)) vol.Vertex7.Volumes.Add(vol);
      vol.Vertex8 = vertices[7];
      if(!vol.Vertex8.Volumes.Contains(vol)) vol.Vertex8.Volumes.Add(vol);

      vol.Edge1 = edges[0];
      if(!vol.Edge1.Volumes.Contains(vol)) vol.Edge1.Volumes.Add(vol);
      vol.Edge2 = edges[1];
      if(!vol.Edge2.Volumes.Contains(vol)) vol.Edge2.Volumes.Add(vol);
      vol.Edge3 = edges[2];
      if(!vol.Edge3.Volumes.Contains(vol)) vol.Edge3.Volumes.Add(vol);
      vol.Edge4 = edges[3];
      if(!vol.Edge4.Volumes.Contains(vol)) vol.Edge4.Volumes.Add(vol);
      vol.Edge5 = edges[4];
      if(!vol.Edge5.Volumes.Contains(vol)) vol.Edge5.Volumes.Add(vol);
      vol.Edge6 = edges[5];
      if(!vol.Edge6.Volumes.Contains(vol)) vol.Edge6.Volumes.Add(vol);
      vol.Edge7 = edges[6];
      if(!vol.Edge7.Volumes.Contains(vol)) vol.Edge7.Volumes.Add(vol);
      vol.Edge8 = edges[7];
      if(!vol.Edge8.Volumes.Contains(vol)) vol.Edge8.Volumes.Add(vol);
      vol.Edge9 = edges[8];
      if(!vol.Edge9.Volumes.Contains(vol)) vol.Edge9.Volumes.Add(vol);
      vol.Edge10 = edges[9];
      if(!vol.Edge10.Volumes.Contains(vol)) vol.Edge10.Volumes.Add(vol);
      vol.Edge11 = edges[10];
      if(!vol.Edge11.Volumes.Contains(vol)) vol.Edge11.Volumes.Add(vol);
      vol.Edge12 = edges[11];
      if(!vol.Edge12.Volumes.Contains(vol)) vol.Edge12.Volumes.Add(vol);
      
      vol.CalculateCenter();
      vol.CalculateSurface();
      vol.CalculateVolume();

      return vol;
    }

    // Saving as STL for testing purposes
    public override string ToSTL()
    {
      // Title
      string output = "solid \"ID" + ID + "<stl unit=MM>\"\n";

      Vector3D Face1Normal = (Face1.Center - Center).Dot(Face1.Normal) > 0 ? Face1.Normal : -Face1.Normal;
      Vector3D Face2Normal = (Face2.Center - Center).Dot(Face2.Normal) > 0 ? Face2.Normal : -Face2.Normal;
      Vector3D Face3Normal = (Face3.Center - Center).Dot(Face3.Normal) > 0 ? Face3.Normal : -Face3.Normal;
      Vector3D Face4Normal = (Face4.Center - Center).Dot(Face4.Normal) > 0 ? Face4.Normal : -Face4.Normal;
      Vector3D Face5Normal = (Face5.Center - Center).Dot(Face5.Normal) > 0 ? Face5.Normal : -Face5.Normal;
      Vector3D Face6Normal = (Face6.Center - Center).Dot(Face6.Normal) > 0 ? Face6.Normal : -Face6.Normal;

      // Face1
      output += "\tfacet normal " + Face1Normal.X + " " + Face1Normal.Y + " " + Face1Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face1.Vertex1.Position.X + " " + Face1.Vertex1.Position.Y + " " + Face1.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex2.Position.X + " " + Face1.Vertex2.Position.Y + " " + Face1.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex3.Position.X + " " + Face1.Vertex3.Position.Y + " " + Face1.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face1Normal.X + " " + Face1Normal.Y + " " + Face1Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face1.Vertex3.Position.X + " " + Face1.Vertex3.Position.Y + " " + Face1.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex4.Position.X + " " + Face1.Vertex4.Position.Y + " " + Face1.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex1.Position.X + " " + Face1.Vertex1.Position.Y + " " + Face1.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face2
      output += "\tfacet normal " + Face2Normal.X + " " + Face2Normal.Y + " " + Face2Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face2.Vertex1.Position.X + " " + Face2.Vertex1.Position.Y + " " + Face2.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex2.Position.X + " " + Face2.Vertex2.Position.Y + " " + Face2.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex3.Position.X + " " + Face2.Vertex3.Position.Y + " " + Face2.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face2Normal.X + " " + Face2Normal.Y + " " + Face2Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face2.Vertex3.Position.X + " " + Face2.Vertex3.Position.Y + " " + Face2.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex4.Position.X + " " + Face2.Vertex4.Position.Y + " " + Face2.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex1.Position.X + " " + Face2.Vertex1.Position.Y + " " + Face2.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face3
      output += "\tfacet normal " + Face3Normal.X + " " + Face3Normal.Y + " " + Face3Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face3.Vertex1.Position.X + " " + Face3.Vertex1.Position.Y + " " + Face3.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex2.Position.X + " " + Face3.Vertex2.Position.Y + " " + Face3.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex3.Position.X + " " + Face3.Vertex3.Position.Y + " " + Face3.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face3Normal.X + " " + Face3Normal.Y + " " + Face3Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face3.Vertex3.Position.X + " " + Face3.Vertex3.Position.Y + " " + Face3.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex4.Position.X + " " + Face3.Vertex4.Position.Y + " " + Face3.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex1.Position.X + " " + Face3.Vertex1.Position.Y + " " + Face3.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face4
      output += "\tfacet normal " + Face4Normal.X + " " + Face4Normal.Y + " " + Face4Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face4.Vertex1.Position.X + " " + Face4.Vertex1.Position.Y + " " + Face4.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex2.Position.X + " " + Face4.Vertex2.Position.Y + " " + Face4.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex3.Position.X + " " + Face4.Vertex3.Position.Y + " " + Face4.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tend facet\n";
      output += "\tfacet normal " + Face4Normal.X + " " + Face4Normal.Y + " " + Face4Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face4.Vertex3.Position.X + " " + Face4.Vertex3.Position.Y + " " + Face4.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex4.Position.X + " " + Face4.Vertex4.Position.Y + " " + Face4.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex1.Position.X + " " + Face4.Vertex1.Position.Y + " " + Face4.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face5
      output += "\tfacet normal " + Face5Normal.X + " " + Face5Normal.Y + " " + Face5Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face5.Vertex1.Position.X + " " + Face5.Vertex1.Position.Y + " " + Face5.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex2.Position.X + " " + Face5.Vertex2.Position.Y + " " + Face5.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex3.Position.X + " " + Face5.Vertex3.Position.Y + " " + Face5.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face5Normal.X + " " + Face5Normal.Y + " " + Face5Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face5.Vertex3.Position.X + " " + Face5.Vertex3.Position.Y + " " + Face5.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex4.Position.X + " " + Face5.Vertex4.Position.Y + " " + Face5.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex1.Position.X + " " + Face5.Vertex1.Position.Y + " " + Face5.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face6
      output += "\tfacet normal " + Face6Normal.X + " " + Face6Normal.Y + " " + Face6Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face6.Vertex1.Position.X + " " + Face6.Vertex1.Position.Y + " " + Face6.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face6.Vertex2.Position.X + " " + Face6.Vertex2.Position.Y + " " + Face6.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face6.Vertex3.Position.X + " " + Face6.Vertex3.Position.Y + " " + Face6.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face6Normal.X + " " + Face6Normal.Y + " " + Face6Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face6.Vertex3.Position.X + " " + Face6.Vertex3.Position.Y + " " + Face6.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face6.Vertex4.Position.X + " " + Face6.Vertex4.Position.Y + " " + Face6.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face6.Vertex1.Position.X + " " + Face6.Vertex1.Position.Y + " " + Face6.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // End
      output += "endsolid\n";

      return output;
    }
  }

  public class WedgeVolume : Volume
  {
    public Vertex Vertex1;
    public Vertex Vertex2;
    public Vertex Vertex3;
    public Vertex Vertex4;
    public Vertex Vertex5;
    public Vertex Vertex6;
    public Edge Edge1;
    public Edge Edge2;
    public Edge Edge3;
    public Edge Edge4;
    public Edge Edge5;
    public Edge Edge6;
    public Edge Edge7;
    public Edge Edge8;
    public Edge Edge9;
    public TriFace Face1;
    public TriFace Face2;
    public QuadFace Face3;
    public QuadFace Face4;
    public QuadFace Face5;
    
    public WedgeVolume(int id) : base(id) { }

    public override void CalculateVolume()
    {
      Vector3D Face1Normal = (Face1.Center - Center).Dot(Face1.Normal) > 0 ? Face1.Normal : -Face1.Normal;
      Vector3D Face2Normal = (Face2.Center - Center).Dot(Face2.Normal) > 0 ? Face2.Normal : -Face2.Normal;
      Vector3D Face3Normal = (Face3.Center - Center).Dot(Face3.Normal) > 0 ? Face3.Normal : -Face3.Normal;
      Vector3D Face4Normal = (Face4.Center - Center).Dot(Face4.Normal) > 0 ? Face4.Normal : -Face4.Normal;
      Vector3D Face5Normal = (Face5.Center - Center).Dot(Face5.Normal) > 0 ? Face5.Normal : -Face5.Normal;

      Volum = Constants.OneThird * (
        Face1.Center.Dot(Face1Normal)*Face1.Area + 
        Face2.Center.Dot(Face2Normal)*Face2.Area + 
        Face3.Center.Dot(Face3Normal)*Face3.Area + 
        Face4.Center.Dot(Face4Normal)*Face4.Area + 
        Face5.Center.Dot(Face5Normal)*Face5.Area);
    }

    public override void CalculateSurface()
    {
      Surface = Face1.Area + Face2.Area + Face3.Area + Face4.Area + Face5.Area;
    }

    public override void CalculateCenter()
    {
      Center = (Vertex1.Position + Vertex2.Position + Vertex3.Position + Vertex4.Position + Vertex5.Position + Vertex6.Position) * Constants.OneThird * 0.5;
    }

    public static WedgeVolume CreateVolume(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vertex v5, Vertex v6)
    {
      WedgeVolume vol = new WedgeVolume(IDCounter.ClaimID());
      return vol;
    }

    public static WedgeVolume CreateVolume(TriFace f1, TriFace f2, QuadFace f3, QuadFace f4, QuadFace f5)
    {
      WedgeVolume vol = new WedgeVolume(IDCounter.ClaimID());

      List<Face> faces = new List<Face>() {f1, f2, f3, f4, f5};
      List<Edge> edges = new List<Edge>();
      List<Vertex> vertices = new List<Vertex>();

      foreach(Face f in faces)
      {
        if(vol.Face1 == null && f is TriFace) {
          vol.Face1 = f as TriFace;
        } else if(vol.Face2 == null && f is TriFace) {
          vol.Face2 = f as TriFace;
        } else if(vol.Face3 == null && f is QuadFace) {
          vol.Face3 = f as QuadFace;
        } else if(vol.Face4 == null && f is QuadFace) {
          vol.Face4 = f as QuadFace;
        } else if(vol.Face5 == null && f is QuadFace) {
          vol.Face5 = f as QuadFace;
        } else {
          Console.WriteLine("Error during volume #" + vol.ID + "creation: Not expected face type.");
        }

        if(f.Volume1 == null)
        {
          f.Volume1 = vol;
        } else if(f.Volume2 == null){
          f.Volume2 = vol;
        } else {
          Console.WriteLine("Error during volume #" + vol.ID + "creation: Face #" + f.ID + " already has");
        }

        if(f is TriFace)
        {
          if(!edges.Contains((f as TriFace).Edge1)) edges.Add((f as TriFace).Edge1);
          if(!edges.Contains((f as TriFace).Edge2)) edges.Add((f as TriFace).Edge2);
          if(!edges.Contains((f as TriFace).Edge3)) edges.Add((f as TriFace).Edge3);
          if(!vertices.Contains((f as TriFace).Vertex1)) vertices.Add((f as TriFace).Vertex1);
          if(!vertices.Contains((f as TriFace).Vertex2)) vertices.Add((f as TriFace).Vertex2);
          if(!vertices.Contains((f as TriFace).Vertex3)) vertices.Add((f as TriFace).Vertex3);
        }

        if(f is QuadFace)
        {
          if(!edges.Contains((f as QuadFace).Edge1)) edges.Add((f as QuadFace).Edge1);
          if(!edges.Contains((f as QuadFace).Edge2)) edges.Add((f as QuadFace).Edge2);
          if(!edges.Contains((f as QuadFace).Edge3)) edges.Add((f as QuadFace).Edge3);
          if(!edges.Contains((f as QuadFace).Edge4)) edges.Add((f as QuadFace).Edge4);
          if(!vertices.Contains((f as QuadFace).Vertex1)) vertices.Add((f as QuadFace).Vertex1);
          if(!vertices.Contains((f as QuadFace).Vertex2)) vertices.Add((f as QuadFace).Vertex2);
          if(!vertices.Contains((f as QuadFace).Vertex3)) vertices.Add((f as QuadFace).Vertex3);
          if(!vertices.Contains((f as QuadFace).Vertex4)) vertices.Add((f as QuadFace).Vertex4);
        }
      }

      vol.Vertex1 = vertices[0];
      if(!vol.Vertex1.Volumes.Contains(vol)) vol.Vertex1.Volumes.Add(vol);
      vol.Vertex2 = vertices[1];
      if(!vol.Vertex2.Volumes.Contains(vol)) vol.Vertex2.Volumes.Add(vol);
      vol.Vertex3 = vertices[2];
      if(!vol.Vertex3.Volumes.Contains(vol)) vol.Vertex3.Volumes.Add(vol);
      vol.Vertex4 = vertices[3];
      if(!vol.Vertex4.Volumes.Contains(vol)) vol.Vertex4.Volumes.Add(vol);
      vol.Vertex5 = vertices[4];
      if(!vol.Vertex5.Volumes.Contains(vol)) vol.Vertex5.Volumes.Add(vol);
      vol.Vertex6 = vertices[5];
      if(!vol.Vertex6.Volumes.Contains(vol)) vol.Vertex6.Volumes.Add(vol);

      vol.Edge1 = edges[0];
      if(!vol.Edge1.Volumes.Contains(vol)) vol.Edge1.Volumes.Add(vol);
      vol.Edge2 = edges[1];
      if(!vol.Edge2.Volumes.Contains(vol)) vol.Edge2.Volumes.Add(vol);
      vol.Edge3 = edges[2];
      if(!vol.Edge3.Volumes.Contains(vol)) vol.Edge3.Volumes.Add(vol);
      vol.Edge4 = edges[3];
      if(!vol.Edge4.Volumes.Contains(vol)) vol.Edge4.Volumes.Add(vol);
      vol.Edge5 = edges[4];
      if(!vol.Edge5.Volumes.Contains(vol)) vol.Edge5.Volumes.Add(vol);
      vol.Edge6 = edges[5];
      if(!vol.Edge6.Volumes.Contains(vol)) vol.Edge6.Volumes.Add(vol);
      vol.Edge7 = edges[6];
      if(!vol.Edge7.Volumes.Contains(vol)) vol.Edge7.Volumes.Add(vol);
      vol.Edge8 = edges[7];
      if(!vol.Edge8.Volumes.Contains(vol)) vol.Edge8.Volumes.Add(vol);
      vol.Edge9 = edges[8];
      if(!vol.Edge9.Volumes.Contains(vol)) vol.Edge9.Volumes.Add(vol);
      
      vol.CalculateCenter();
      vol.CalculateSurface();
      vol.CalculateVolume();

      return vol;
    }

    // Saving as STL for testing purposes
    public override string ToSTL()
    {
      // Title
      string output = "solid \"ID" + ID + "<stl unit=MM>\"\n";

      Vector3D Face1Normal = (Face1.Center - Center).Dot(Face1.Normal) > 0 ? Face1.Normal : -Face1.Normal;
      Vector3D Face2Normal = (Face2.Center - Center).Dot(Face2.Normal) > 0 ? Face2.Normal : -Face2.Normal;
      Vector3D Face3Normal = (Face3.Center - Center).Dot(Face3.Normal) > 0 ? Face3.Normal : -Face3.Normal;
      Vector3D Face4Normal = (Face4.Center - Center).Dot(Face4.Normal) > 0 ? Face4.Normal : -Face4.Normal;
      Vector3D Face5Normal = (Face5.Center - Center).Dot(Face5.Normal) > 0 ? Face5.Normal : -Face5.Normal;

      // Face1
      output += "\tfacet normal " + Face1Normal.X + " " + Face1Normal.Y + " " + Face1Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face1.Vertex1.Position.X + " " + Face1.Vertex1.Position.Y + " " + Face1.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex2.Position.X + " " + Face1.Vertex2.Position.Y + " " + Face1.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex3.Position.X + " " + Face1.Vertex3.Position.Y + " " + Face1.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face2
      output += "\tfacet normal " + Face2Normal.X + " " + Face2Normal.Y + " " + Face2Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face2.Vertex1.Position.X + " " + Face2.Vertex1.Position.Y + " " + Face2.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex2.Position.X + " " + Face2.Vertex2.Position.Y + " " + Face2.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex3.Position.X + " " + Face2.Vertex3.Position.Y + " " + Face2.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face3
      output += "\tfacet normal " + Face3Normal.X + " " + Face3Normal.Y + " " + Face3Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face3.Vertex1.Position.X + " " + Face3.Vertex1.Position.Y + " " + Face3.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex2.Position.X + " " + Face3.Vertex2.Position.Y + " " + Face3.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex3.Position.X + " " + Face3.Vertex3.Position.Y + " " + Face3.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face3Normal.X + " " + Face3Normal.Y + " " + Face3Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face3.Vertex3.Position.X + " " + Face3.Vertex3.Position.Y + " " + Face3.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex4.Position.X + " " + Face3.Vertex4.Position.Y + " " + Face3.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex1.Position.X + " " + Face3.Vertex1.Position.Y + " " + Face3.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face4
      output += "\tfacet normal " + Face4Normal.X + " " + Face4Normal.Y + " " + Face4Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face4.Vertex1.Position.X + " " + Face4.Vertex1.Position.Y + " " + Face4.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex2.Position.X + " " + Face4.Vertex2.Position.Y + " " + Face4.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex3.Position.X + " " + Face4.Vertex3.Position.Y + " " + Face4.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tend facet\n";
      output += "\tfacet normal " + Face4Normal.X + " " + Face4Normal.Y + " " + Face4Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face4.Vertex3.Position.X + " " + Face4.Vertex3.Position.Y + " " + Face4.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex4.Position.X + " " + Face4.Vertex4.Position.Y + " " + Face4.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex1.Position.X + " " + Face4.Vertex1.Position.Y + " " + Face4.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face5
      output += "\tfacet normal " + Face5Normal.X + " " + Face5Normal.Y + " " + Face5Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face5.Vertex1.Position.X + " " + Face5.Vertex1.Position.Y + " " + Face5.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex2.Position.X + " " + Face5.Vertex2.Position.Y + " " + Face5.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex3.Position.X + " " + Face5.Vertex3.Position.Y + " " + Face5.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face5Normal.X + " " + Face5Normal.Y + " " + Face5Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face5.Vertex3.Position.X + " " + Face5.Vertex3.Position.Y + " " + Face5.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex4.Position.X + " " + Face5.Vertex4.Position.Y + " " + Face5.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex1.Position.X + " " + Face5.Vertex1.Position.Y + " " + Face5.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // End
      output += "endsolid\n";

      return output;
    }
  }

  public class PyramidVolume : Volume
  {
    public Vertex Vertex1;
    public Vertex Vertex2;
    public Vertex Vertex3;
    public Vertex Vertex4;
    public Vertex Vertex5;
    public Edge Edge1;
    public Edge Edge2;
    public Edge Edge3;
    public Edge Edge4;
    public Edge Edge5;
    public Edge Edge6;
    public Edge Edge7;
    public Edge Edge8;
    public TriFace Face1;
    public TriFace Face2;
    public TriFace Face3;
    public TriFace Face4;
    public QuadFace Face5;
    
    public PyramidVolume(int id) : base(id) { }

    public override void CalculateVolume()
    {
      Vector3D Face1Normal = (Face1.Center - Center).Dot(Face1.Normal) > 0 ? Face1.Normal : -Face1.Normal;
      Vector3D Face2Normal = (Face2.Center - Center).Dot(Face2.Normal) > 0 ? Face2.Normal : -Face2.Normal;
      Vector3D Face3Normal = (Face3.Center - Center).Dot(Face3.Normal) > 0 ? Face3.Normal : -Face3.Normal;
      Vector3D Face4Normal = (Face4.Center - Center).Dot(Face4.Normal) > 0 ? Face4.Normal : -Face4.Normal;
      Vector3D Face5Normal = (Face5.Center - Center).Dot(Face5.Normal) > 0 ? Face5.Normal : -Face5.Normal;

      Volum = Constants.OneThird * (
        Face1.Center.Dot(Face1Normal)*Face1.Area + 
        Face2.Center.Dot(Face2Normal)*Face2.Area + 
        Face3.Center.Dot(Face3Normal)*Face3.Area + 
        Face4.Center.Dot(Face4Normal)*Face4.Area + 
        Face5.Center.Dot(Face5Normal)*Face5.Area);
        
        /*
        if(Double.IsNaN(Volum))
        {
          Console.WriteLine(Face1.Area);
          Console.WriteLine(Face2.Area);
          Console.WriteLine(Face3.Area);
          Console.WriteLine(Face4.Area);
          Console.WriteLine(Face5.Area);
        }
        */
    }

    public override void CalculateSurface()
    {
      Surface = Face1.Area + Face2.Area + Face3.Area + Face4.Area + Face5.Area;
    }

    public override void CalculateCenter()
    {
      Center = (Vertex1.Position + Vertex2.Position + Vertex3.Position + Vertex4.Position + Vertex5.Position) * 0.2;
    }

    public static PyramidVolume CreateVolume(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vertex v5)
    {
      PyramidVolume vol = new PyramidVolume(IDCounter.ClaimID());
      return vol;
    }
    
    public static PyramidVolume CreateVolume(TriFace f1, TriFace f2, TriFace f3, TriFace f4, QuadFace f5)
    {
      PyramidVolume vol = new PyramidVolume(IDCounter.ClaimID());

      List<Face> faces = new List<Face>() {f1, f2, f3, f4, f5};
      List<Edge> edges = new List<Edge>();
      List<Vertex> vertices = new List<Vertex>();

      foreach(Face f in faces)
      {
        if(vol.Face1 == null && f is TriFace) {
          vol.Face1 = f as TriFace;
        } else if(vol.Face2 == null && f is TriFace) {
          vol.Face2 = f as TriFace;
        } else if(vol.Face3 == null && f is TriFace) {
          vol.Face3 = f as TriFace;
        } else if(vol.Face4 == null && f is TriFace) {
          vol.Face4 = f as TriFace;
        } else if(vol.Face5 == null && f is QuadFace) {
          vol.Face5 = f as QuadFace;
        } else {
          Console.WriteLine("Error during volume #" + vol.ID + "creation: Not expected face type.");
        }

        if(f.Volume1 == null)
        {
          f.Volume1 = vol;
        } else if(f.Volume2 == null){
          f.Volume2 = vol;
        } else {
          Console.WriteLine("Error during volume #" + vol.ID + "creation: Face #" + f.ID + " already has");
        }

        if(f is TriFace)
        {
          if(!edges.Contains((f as TriFace).Edge1)) edges.Add((f as TriFace).Edge1);
          if(!edges.Contains((f as TriFace).Edge2)) edges.Add((f as TriFace).Edge2);
          if(!edges.Contains((f as TriFace).Edge3)) edges.Add((f as TriFace).Edge3);
          if(!vertices.Contains((f as TriFace).Vertex1)) vertices.Add((f as TriFace).Vertex1);
          if(!vertices.Contains((f as TriFace).Vertex2)) vertices.Add((f as TriFace).Vertex2);
          if(!vertices.Contains((f as TriFace).Vertex3)) vertices.Add((f as TriFace).Vertex3);
        }

        if(f is QuadFace)
        {
          if(!edges.Contains((f as QuadFace).Edge1)) edges.Add((f as QuadFace).Edge1);
          if(!edges.Contains((f as QuadFace).Edge2)) edges.Add((f as QuadFace).Edge2);
          if(!edges.Contains((f as QuadFace).Edge3)) edges.Add((f as QuadFace).Edge3);
          if(!edges.Contains((f as QuadFace).Edge4)) edges.Add((f as QuadFace).Edge4);
          if(!vertices.Contains((f as QuadFace).Vertex1)) vertices.Add((f as QuadFace).Vertex1);
          if(!vertices.Contains((f as QuadFace).Vertex2)) vertices.Add((f as QuadFace).Vertex2);
          if(!vertices.Contains((f as QuadFace).Vertex3)) vertices.Add((f as QuadFace).Vertex3);
          if(!vertices.Contains((f as QuadFace).Vertex4)) vertices.Add((f as QuadFace).Vertex4);
        }
      }

      vol.Vertex1 = vertices[0];
      if(!vol.Vertex1.Volumes.Contains(vol)) vol.Vertex1.Volumes.Add(vol);
      vol.Vertex2 = vertices[1];
      if(!vol.Vertex2.Volumes.Contains(vol)) vol.Vertex2.Volumes.Add(vol);
      vol.Vertex3 = vertices[2];
      if(!vol.Vertex3.Volumes.Contains(vol)) vol.Vertex3.Volumes.Add(vol);
      vol.Vertex4 = vertices[3];
      if(!vol.Vertex4.Volumes.Contains(vol)) vol.Vertex4.Volumes.Add(vol);
      vol.Vertex5 = vertices[4];
      if(!vol.Vertex5.Volumes.Contains(vol)) vol.Vertex5.Volumes.Add(vol);

      vol.Edge1 = edges[0];
      if(!vol.Edge1.Volumes.Contains(vol)) vol.Edge1.Volumes.Add(vol);
      vol.Edge2 = edges[1];
      if(!vol.Edge2.Volumes.Contains(vol)) vol.Edge2.Volumes.Add(vol);
      vol.Edge3 = edges[2];
      if(!vol.Edge3.Volumes.Contains(vol)) vol.Edge3.Volumes.Add(vol);
      vol.Edge4 = edges[3];
      if(!vol.Edge4.Volumes.Contains(vol)) vol.Edge4.Volumes.Add(vol);
      vol.Edge5 = edges[4];
      if(!vol.Edge5.Volumes.Contains(vol)) vol.Edge5.Volumes.Add(vol);
      vol.Edge6 = edges[5];
      if(!vol.Edge6.Volumes.Contains(vol)) vol.Edge6.Volumes.Add(vol);
      vol.Edge7 = edges[6];
      if(!vol.Edge7.Volumes.Contains(vol)) vol.Edge7.Volumes.Add(vol);
      vol.Edge8 = edges[7];
      if(!vol.Edge8.Volumes.Contains(vol)) vol.Edge8.Volumes.Add(vol);
      
      vol.CalculateCenter();
      vol.CalculateSurface();
      vol.CalculateVolume();

      return vol;
    }

    // Saving as STL for testing purposes
    public override string ToSTL()
    {
      // Title
      string output = "solid \"ID" + ID + "<stl unit=MM>\"\n";

      Vector3D Face1Normal = (Face1.Center - Center).Dot(Face1.Normal) > 0 ? Face1.Normal : -Face1.Normal;
      Vector3D Face2Normal = (Face2.Center - Center).Dot(Face2.Normal) > 0 ? Face2.Normal : -Face2.Normal;
      Vector3D Face3Normal = (Face3.Center - Center).Dot(Face3.Normal) > 0 ? Face3.Normal : -Face3.Normal;
      Vector3D Face4Normal = (Face4.Center - Center).Dot(Face4.Normal) > 0 ? Face4.Normal : -Face4.Normal;
      Vector3D Face5Normal = (Face5.Center - Center).Dot(Face5.Normal) > 0 ? Face5.Normal : -Face5.Normal;

      // Face1
      output += "\tfacet normal " + Face1Normal.X + " " + Face1Normal.Y + " " + Face1Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face1.Vertex1.Position.X + " " + Face1.Vertex1.Position.Y + " " + Face1.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex2.Position.X + " " + Face1.Vertex2.Position.Y + " " + Face1.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face1.Vertex3.Position.X + " " + Face1.Vertex3.Position.Y + " " + Face1.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face2
      output += "\tfacet normal " + Face2Normal.X + " " + Face2Normal.Y + " " + Face2Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face2.Vertex1.Position.X + " " + Face2.Vertex1.Position.Y + " " + Face2.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex2.Position.X + " " + Face2.Vertex2.Position.Y + " " + Face2.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face2.Vertex3.Position.X + " " + Face2.Vertex3.Position.Y + " " + Face2.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face3
      output += "\tfacet normal " + Face3Normal.X + " " + Face3Normal.Y + " " + Face3Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face3.Vertex1.Position.X + " " + Face3.Vertex1.Position.Y + " " + Face3.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex2.Position.X + " " + Face3.Vertex2.Position.Y + " " + Face3.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face3.Vertex3.Position.X + " " + Face3.Vertex3.Position.Y + " " + Face3.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // Face4
      output += "\tfacet normal " + Face4Normal.X + " " + Face4Normal.Y + " " + Face4Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face4.Vertex1.Position.X + " " + Face4.Vertex1.Position.Y + " " + Face4.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex2.Position.X + " " + Face4.Vertex2.Position.Y + " " + Face4.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face4.Vertex3.Position.X + " " + Face4.Vertex3.Position.Y + " " + Face4.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tend facet\n";

      // Face5
      output += "\tfacet normal " + Face5Normal.X + " " + Face5Normal.Y + " " + Face5Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face5.Vertex1.Position.X + " " + Face5.Vertex1.Position.Y + " " + Face5.Vertex1.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex2.Position.X + " " + Face5.Vertex2.Position.Y + " " + Face5.Vertex2.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex3.Position.X + " " + Face5.Vertex3.Position.Y + " " + Face5.Vertex3.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";
      output += "\tfacet normal " + Face5Normal.X + " " + Face5Normal.Y + " " + Face5Normal.Z + "\n";
      output += "\t\touter loop\n";
      output += "\t\t\tvertex " + Face5.Vertex3.Position.X + " " + Face5.Vertex3.Position.Y + " " + Face5.Vertex3.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex4.Position.X + " " + Face5.Vertex4.Position.Y + " " + Face5.Vertex4.Position.Z + "\n";
      output += "\t\t\tvertex " + Face5.Vertex1.Position.X + " " + Face5.Vertex1.Position.Y + " " + Face5.Vertex1.Position.Z + "\n";
      output += "\t\tendloop\n";
      output += "\tendfacet\n";

      // End
      output += "endsolid\n";

      return output;
    }
  }
}