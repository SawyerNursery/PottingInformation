using System;
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
    public partial class frmPreferances : Form
    {
        public frmPreferances()
        {
            InitializeComponent();
            txtDBName.Text = Properties.Settings.Default.dbName;
            txtPass.Text = Properties.Settings.Default.dbPassword;
            txtUname.Text = Properties.Settings.Default.dbUser;
            txtServer.Text = Properties.Settings.Default.dbServer;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.dbName = txtDBName.Text;
            Properties.Settings.Default.dbPassword = txtPass.Text;
            Properties.Settings.Default.dbUser = txtUname.Text;
            Properties.Settings.Default.dbServer = txtServer.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnTestPrinter_Click(object sender, EventArgs e)
        {
            // Allow the user to select a printer.
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            if (DialogResult.OK == pd.ShowDialog(this))
            {
                // Send a printer-specific to the printer.
                Helper.Test_Print(pd.PrinterSettings.PrinterName, txtPrntCommand.Text);
            }
        }

        private void btnCredentials_Click(object sender, EventArgs e)
        {
            using (Kerr.PromptForCredential dialog = new Kerr.PromptForCredential())
            {
                if (txtServer.Text!="")
                {
                    string server;
                    server = txtServer.Text.Replace(',', ':');
                    dialog.TargetName = server;
                    dialog.Message = "Please enter your SBI login information and it will be added to the Windows credential vault";
                    dialog.UserName = txtUname.Text;
                    
                    dialog.ExpectConfirmation = true;

                    if (DialogResult.OK == dialog.ShowDialog(this))
                    {
                        if (dialog.SaveChecked)
                        {
                            dialog.ConfirmCredentials();
                        }
                    }
                }
            }
        }

    }
}
