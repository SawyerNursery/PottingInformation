using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Potting_Information
{
    public partial class Reportviewer : Form
    {
        string reportname;
        DataSet reportdata;


        public Reportviewer(string ReportName, DataSet ReportData)
        {
            InitializeComponent();
            reportname = ReportName;
            reportdata = ReportData;
        }

        private void Reportviewer_Load(object sender, EventArgs e)
        {
            if (reportname == "PottingSummary")
            {
                ReportDocument PottingSummaryReport = new PottingSummary();
                PottingSummaryReport.SetDataSource(reportdata.Tables[0]);
                crViewer.ReportSource = PottingSummaryReport;
            }
            crViewer.Refresh();
        }
    }
}
