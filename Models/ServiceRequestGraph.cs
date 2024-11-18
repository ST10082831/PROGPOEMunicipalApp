using System;
using System.Collections.Generic;
using System.Diagnostics;
using static POEPART1MunicipalApp.Models.ReportClasses;

public class ServiceRequestGraph
{
    // Dictionary to store reports as vertices in the graph, using Report ID as the key
    public Dictionary<int, Report> Vertices { get; set; }

    // Dictionary to store edges, representing relationships between reports
    // Each key is a Report ID, and the value is a list of connected Report IDs
    public Dictionary<int, List<int>> Edges { get; set; }

    public ServiceRequestGraph()
    {
        // Initialize the graph with empty vertices and edges
        Vertices = new Dictionary<int, Report>();
        Edges = new Dictionary<int, List<int>>();
    }

    // Method to add a new vertex (report) to the graph
    public void AddVertex(Report report)
    {
        Debug.WriteLine($"[Graph] Adding Vertex for Report ID: {report.Id}");

        // Add the report to the vertices dictionary
        Vertices[report.Id] = report;

        // Initialize an empty list of edges for the new vertex
        Edges[report.Id] = new List<int>();
    }

    // Method to add a directed edge from one report to another
    public void AddEdge(int fromId, int toId)
    {
        Debug.WriteLine($"[Graph] Adding Edge from Report ID {fromId} to Report ID {toId}");

        // Check if the source report exists in the edges dictionary
        if (Edges.ContainsKey(fromId))
        {
            // Add the destination report ID to the list of edges for the source report
            Edges[fromId].Add(toId);
        }
    }

    // Method to traverse the graph and log its structure
    public void TraverseGraph()
    {
        Debug.WriteLine("[Graph] Traversing the Graph:");

        // Iterate through all vertices in the graph
        foreach (var vertex in Vertices)
        {
            // Log the current vertex (report) 
            Debug.WriteLine($"[Graph] Report ID: {vertex.Key}, Edges: {string.Join(", ", Edges[vertex.Key])}");
        }
    }
}

// SOurce : https://masterdotnet.com/csharp/ds/graphincsharp/