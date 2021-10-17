using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project_CIS262.Dal;

namespace Final_Project_CIS262.Models
{
    public class ReportModel
    {

        private ReportAdapter adapter = new ReportAdapter();

        public List<Report> ReportRows { get; set; }

        public void PopulateReport()
        {
            ReportRows = adapter.GetReportRows();
        }
    }
}