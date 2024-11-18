using System;
using System.Collections.Generic;
using System.Diagnostics;
using static POEPART1MunicipalApp.Models.ReportClasses;

public class ReportBST
{
    // Node class represents each element in the Binary Search Tree (BST)
    public class Node
    {
        public Report Data { get; set; } // The data stored in this node, in this case, a Report
        public Node Left { get; set; }  // Pointer to the left child node
        public Node Right { get; set; } // Pointer to the right child node

        public Node(Report data)
        {
            Data = data; // Initialize the node with report data
            Left = Right = null; // Initialize child nodes to null
        }
    }

    public Node Root { get; set; } // The root of the BST

    // Insert method adds a new report to the BST
    public void Insert(Report report)
    {
        Debug.WriteLine($"[BST] Inserting Report ID: {report.Id}");
        Root = InsertRec(Root, report); // Helper method to insert recursively
    }

    // Recursive helper for inserting a report
    private Node InsertRec(Node root, Report report)
    {
        if (root == null) // Base case: if the current position is null, insert here
        {
            Debug.WriteLine($"[BST] Inserted Report ID: {report.Id} at a leaf.");
            return new Node(report); // Create a new node for the report
        }

        if (report.Id < root.Data.Id) // If the report ID is less, go left
        {
            Debug.WriteLine($"[BST] Going left to insert Report ID: {report.Id}");
            root.Left = InsertRec(root.Left, report);
        }
        else if (report.Id > root.Data.Id) // If the report ID is greater, go right
        {
            Debug.WriteLine($"[BST] Going right to insert Report ID: {report.Id}");
            root.Right = InsertRec(root.Right, report);
        }

        return root; // Return the root of the subtree
    }

    // Search method looks for a report in the BST by its ID
    public Report Search(int id)
    {
        Debug.WriteLine($"[BST] Searching for Report ID: {id}");
        return SearchRec(Root, id); // Helper method to search recursively
    }

    // Recursive helper for searching a report by ID
    private Report SearchRec(Node root, int id)
    {
        if (root == null) // If we reach a null position, the report is not found
        {
            Debug.WriteLine($"[BST] Report ID: {id} not found.");
            return null;
        }

        if (root.Data.Id == id) // If the current node contains the report, return it
        {
            Debug.WriteLine($"[BST] Found Report ID: {id}");
            return root.Data;
        }

        // If the report ID is less, search in the left subtree
        if (id < root.Data.Id)
            return SearchRec(root.Left, id);

        // If the report ID is greater, search in the right subtree
        return SearchRec(root.Right, id);
    }

    // InOrder method retrieves all reports in ascending order of their IDs
    public List<Report> InOrder()
    {
        Debug.WriteLine("[BST] Performing in-order traversal...");
        List<Report> reports = new List<Report>(); // List to store the reports
        InOrderRec(Root, reports); // Helper method to perform in-order traversal
        return reports;
    }

    // Recursive helper for in-order traversal
    private void InOrderRec(Node root, List<Report> reports)
    {
        if (root != null) // Continue traversal if the current node is not null
        {
            InOrderRec(root.Left, reports); // Traverse the left subtree
            reports.Add(root.Data); // Add the current node's data to the list
            Debug.WriteLine($"[BST] Visited Report ID: {root.Data.Id}");
            InOrderRec(root.Right, reports); // Traverse the right subtree
        }
    }
}

// SOURCE : https://csharpexamples.com/c-binary-search-tree-implementation/ //