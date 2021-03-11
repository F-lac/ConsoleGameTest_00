using System;
using System.Collections.Generic;

namespace ConsoleGameTest_00
{
  public class Vertex
  {
    public int ID;

    public List<Vertex> Connections;
    public List<Edge> Edges;
    public List<Face> Faces;
    public List<Volume> Volumes;

    public Vector3D Position;

    public Vertex(int id)
    {
      ID = id;
      Connections = new List<Vertex>();
      Edges = new List<Edge>();
      Faces = new List<Face>();
      Volumes = new List<Volume>();
    }

    public static Vertex CreateVertex(Vector3D v)
    {
      Vertex vert = new Vertex(IDCounter.ClaimID());
      vert.Position = v;
      return vert;
    }
  }
}