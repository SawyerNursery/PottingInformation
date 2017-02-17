using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using General;
//using Microsoft.VisualBasic.PowerPacks;

namespace Potting_Information
{
    /// <summary>
    /// Massive overhaul to the potting information system.
    /// </summary>
    public partial class frmPotDetail : Form
    {
        DataSet PottingDS = new DataSet();

        public frmPotDetail()
        {
            InitializeComponent();
            //Set up remote connection to SBI
            dtmStartDate.Value = DateTime.Today.AddDays(-7);
            dtmEndDate.Value = DateTime.Today.AddDays(7);
            cmbSite.Text = Properties.Settings.Default.defSite;

            DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn();
            selectColumn.HeaderText = "Select";
            selectColumn.FalseValue = false;
            selectColumn.TrueValue = true;
            dgPottingDetail.Columns.Insert(0, selectColumn);
            dgPottingDetail.Columns[0].Visible = false;
            connectToDB();
        }

        private void connectToDB()
        {
            if (Properties.Settings.Default.dbServer == "")
            {
                if (MessageBox.Show("No database server configured. Press OK to do this now") == System.Windows.Forms.DialogResult.OK)
                {
                    frmPreferances form = new frmPreferances();
                    form.ShowDialog();
                }
            }
            else
            {
                try
                {
                    string dbServer = Properties.Settings.Default.dbServer;
                    string dbName = Properties.Settings.Default.dbName;
                    string dbUser = Properties.Settings.Default.dbUser;
                    string dbPass = Properties.Settings.Default.dbPassword;

                    //This basically makes sure that we can connect to SBI. They can't select a plan unless they do this.
                    //2-16-2017 Rewrite for new production module. SBI in house now.
                    //Global.SetConnectionString("SBI","Server="+dbServer+";Database="+dbName+";Integrated Security=SSPI;User ID="+dbUser+";Password="+dbPass);
                    Global.SetConnectionString(dbServer, dbName);
                    cmbPlanName.DataSource = Global.GetData("usp_PI_SelectActivePlans").Tables[0];
                    cmbPlanName.DisplayMember = "Description";
                    cmbPlanName.ValueMember = "id";

                    btnConnect.BackColor = System.Drawing.Color.Green;
                    btnSearch.Enabled = true;

                    cmbPlanName.SelectedIndex = Properties.Settings.Default.defPlan;
                }
                catch
                { MessageBox.Show("There was a problem connecting to the database. Check the connection settings"); }
            }
        }

        private void button1_Click(object sender, EventArgs e) //Test connection to SB
        {
            connectToDB();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string PottingQuery;
            string site = cmbSite.Text;
            string type = cmbItemType.Text;
            string StDate = dtmStartDate.Value.ToString();
            string EnDate = dtmEndDate.Value.ToString();
            int Completed = (cmbComplete.Text == "Yes") ? 0 : 5;
            int CG = (ckCG.Checked == true) ? 1 : 0;
            string PlanID = cmbPlanName.SelectedValue.ToString();

            PottingQuery = "usp_PI_SelectPlanDetail @site="+site+",@startDate='"+StDate+"',@endDate='"+EnDate+"',@completed="+Completed+",@planId="+PlanID+",@cg="+CG+",@type='"+type+"'";

            PottingDS = Global.GetData(PottingQuery);

            dgPottingDetail.DataSource = PottingDS.Tables[0];

            //set up datagrid
            dgPottingDetail.Columns[0].Visible = true;
            dgPottingDetail.Columns[1].HeaderText = "Qty Rem";
            dgPottingDetail.Columns[2].HeaderText = "Qty Done";
            dgPottingDetail.Columns[3].HeaderText = "Per Pot";
            dgPottingDetail.Columns[4].HeaderText = "Prod Key";
            dgPottingDetail.Columns[5].HeaderText = "Trans Wk";
            dgPottingDetail.Columns[6].HeaderText = "Ready Wk";
            dgPottingDetail.Columns[7].HeaderText = "Description";
            dgPottingDetail.Columns[8].HeaderText = "To Size";
            dgPottingDetail.Columns[9].HeaderText = "From Size";
            dgPottingDetail.Columns[10].HeaderText = "Lot Id";
            dgPottingDetail.Columns[11].HeaderText = "Site";
            
            //make all the cells read only.
            for (int i=1;i<=10;i++)
            {
                dgPottingDetail.Columns[i].ReadOnly = true;
            }
        }

        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e) //Set default site
        {
            Properties.Settings.Default.defSite = cmbSite.Text;
            Properties.Settings.Default.Save();
        }

        private void cmbPlanName_Leave(object sender, EventArgs e) //Set default plan
        {
            int SearchIndex = cmbPlanName.SelectedIndex;
            Properties.Settings.Default.defPlan = SearchIndex;
            Properties.Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLabels_Click(object sender, EventArgs e)
        {
            frmLabelPrinting form = new frmLabelPrinting(Helper.PottingDetailData(GetCheckedRows()));
            form.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferances form = new frmPreferances();
            form.ShowDialog();
        }

        private void sUMMARYREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportviewer RV = new Reportviewer("PottingSummary", Helper.PottingDetailData(GetCheckedRows()));
            RV.ShowDialog();
        }

        private void dgPottingDetail_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgPottingDetail.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void chkSelectAll_Click(object sender, EventArgs e)
        {
            chkSelectAll.Checked = true;
            foreach (DataGridViewRow row in dgPottingDetail.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null
            }
        }

        private StringBuilder GetCheckedRows()
        {
            StringBuilder LotIds = new StringBuilder();

            foreach (DataGridViewRow row in dgPottingDetail.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {

                    LotIds.Append("," + row.Cells[10].Value.ToString());
                }
            }
            if (LotIds.Length > 0)
            {   LotIds.Remove(0, 1);}
            return LotIds;
        }

    }
}
