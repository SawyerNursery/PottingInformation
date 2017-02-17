namespace Potting_Information
{
    partial class frmLabelPrinting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabelPrinting));
            this.dgLabels = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cmbLabel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgLabels)).BeginInit();
            this.SuspendLayout();
            // 
            // dgLabels
            // 
            this.dgLabels.AllowUserToAddRows = false;
            this.dgLabels.AllowUserToDeleteRows = false;
            this.dgLabels.AllowUserToResizeRows = false;
            this.dgLabels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLabels.Location = new System.Drawing.Point(-2, 63);
            this.dgLabels.Name = "dgLabels";
            this.dgLabels.RowHeadersVisible = false;
            this.dgLabels.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgLabels.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgLabels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLabels.Size = new System.Drawing.Size(787, 543);
            this.dgLabels.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = global::Potting_Information.Properties.Resources.cchristeicon;
            this.btnPrint.Location = new System.Drawing.Point(12, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 45);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cmbLabel
            // 
            this.cmbLabel.FormattingEnabled = true;
            this.cmbLabel.Location = new System.Drawing.Point(201, 19);
            this.cmbLabel.Name = "cmbLabel";
            this.cmbLabel.Size = new System.Drawing.Size(221, 21);
            this.cmbLabel.TabIndex = 2;
            this.cmbLabel.Leave += new System.EventHandler(this.cmbLabel_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Label Printer";
            // 
            // frmLabelPrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 605);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLabel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgLabels);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLabelPrinting";
            this.Text = "Print Labels";
            ((System.ComponentModel.ISupportInitialize)(this.dgLabels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgLabels;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cmbLabel;
        private System.Windows.Forms.Label label1;
    }
}