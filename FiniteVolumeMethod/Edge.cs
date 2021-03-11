using System;
using System.Collections.Generic;

namespace ConsoleGameTest_00
{
  public class Edge
  {
    public int ID;
    public Vertex Vertex1;
    public Vertex Vertex2;
    
    public List<Face> Faces;
    public List<Volume> Volumes;

    public double Length;
    public Vector3D Center;

    public Edge(int id)
    {
      ID = id;
      Faces = new List<Face>();
      Volumes = new List<Volume>();
    }

    // Length calculation
    public void CalculateLength()
    {
      Length = (Vertex1.Position - Vertex2.Position).Length();
    }
    
    // Center calculation
    public void CalculateCenter()
    {
      Center = 0.5 * (Vertex1.Position + Vertex2.Position);
    }

    // Edge creating methods
    public static Edge CreateEdge(Vertex v1, Vertex v2)
    {
      Edge edge = new Edge(IDCounter.ClaimID());
      edge.Vertex1 = v1;
      edge.Vertex2 = v2;
      v1.Edges.Add(edge);
      v1.Connections.Add(v2);
      v2.Edges.Add(edge);
      v2.Connections.Add(v1);
      edge.CalculateCenter();
      edge.CalculateLength();
      return edge;
    }
  }
}