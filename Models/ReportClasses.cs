﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;

namespace POEPART1MunicipalApp.Models
{
    public class ReportClasses
    {
        public class Report
        {
            public int Id { get; set; }             // Report ID (Unique Identifier)
            public string Location { get; set; }    // Location entered by the user
            public string Category { get; set; }    // Category selected from ComboBox
            public string Description { get; set; } // Description entered in the RichTextBox
            public string MediaPath { get; set; }   // File path of the attached media
            public string Status { get; set; }      // Status of the service request (e.g., Pending, In Progress, Completed)
        }
    }
}

