﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Potting_Information
{
    public partial class frmLabelPrinting : Form
    {
        DataSet PottingRecordSelection;

        public frmLabelPrinting(DataSet pottingrecordselection)
        {
            InitializeComponent();
            PottingRecordSelection = pottingrecordselection;
            cmbLabel.SelectedIndex = Properties.Settings.Default.defLabel;

            DataTable Compressed = new DataTable();

            Compressed.Columns.Add("Lot ID", typeof(string));
            Compressed.Columns.Add("Qty", typeof(string));
            Compressed.Columns.Add("Trans Week", typeof(string));
            Compressed.Columns.Add("Item Description", typeof(string));
            Compressed.Columns.Add("To Size", typeof(string));
            Compressed.Columns.Add("From Size", typeof(string));
            Compressed.Columns.Add("Ready Week", typeof(string));

            string prevLotId = "";
            foreach (DataRow dr in PottingRecordSelection.Tables[0].Rows)
            {
                if (prevLotId != dr["LotID"].ToString())
                {
                    Compressed.Rows.Add(dr["LotID"].ToString(), dr["UnitsRemaining"].ToString(), dr["TransWeek"].ToString(), dr["Description"].ToString(),
                        dr["Size"].ToString(), dr["FromSize"].ToString(), dr["Ready"].ToString());
                }
                prevLotId = dr["LotID"].ToString();
            }

            DataGridViewTextBoxColumn QtyToPrint = new DataGridViewTextBoxColumn();
            dgLabels.DataSource = Compressed;
            QtyToPrint.HeaderText = "Qty Labels";
            dgLabels.Columns.Insert(0, QtyToPrint);
            //make all the cells read only.
            for (int i = 1; i <= 7; i++)
            {
                dgLabels.Columns[i].ReadOnly = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Set up for stepping through the grid
            string PrintCommand="";
            DataRow[] SelectedRows;
            string[] FromLocations = new string[4];

            foreach (DataGridViewRow row in dgLabels.Rows)
            {
                if (Convert.ToInt32(row.Cells[0].Value) > 0)
                {
                    int i = 0;
                    Array.Clear(FromLocations, 0, 4);
                    SelectedRows = PottingRecordSelection.Tables[0].Select("LotID ="+Convert.ToInt32(row.Cells[1].Value));
                    switch (cmbLabel.SelectedIndex)
                    { 
                        case 1:
                            PrintCommand = PrintCommand + Helper.Zebra_105SL_3x3_PottingWorkOrder(SelectedRows[0], FromLocations);
                            PrintCommand = PrintCommand + Helper.Zebra_105SL_3x3_PottingFieldLabel(SelectedRows[0], Convert.ToInt32(row.Cells[0].Value));
                            break;
                        case 0:
                            PrintCommand = PrintCommand + Helper.Zebra_2844_35x25_PottingWorkOrder(SelectedRows[0], FromLocations);
                            PrintCommand = PrintCommand + Helper.Zebra_2844_35x25_PottingFieldLabel(SelectedRows[0], Convert.ToInt32(row.Cells[0].Value));
                            break;
                        default:
                            MessageBox.Show("Please select a valid label type.");
                            break;
                    }
                    foreach(DataRow dr in SelectedRows)
                    {
                        if (i < 4)
                        { FromLocations[i] = dr["GHLocation"] + " - " + dr["FlatsInStock"]; }
                        i++;
                    }
                    
                }
            }
            //DEBUGGING  MessageBox.Show(PrintCommand);
            // Allow the user to select a printer.
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                // Send a printer-specific to the printer.
                RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, PrintCommand);
                
            }
        }

        private void cmbLabel_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.defLabel = cmbLabel.SelectedIndex;
            Properties.Settings.Default.Save();
        }
    }
}
