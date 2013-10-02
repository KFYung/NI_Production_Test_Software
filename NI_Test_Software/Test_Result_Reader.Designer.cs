namespace NI_Test_Software
{
    partial class Test_Result_Reader
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Table_Layout = new System.Windows.Forms.DataGridView();
            this.Exit = new System.Windows.Forms.Button();
            this.File_Export = new System.Windows.Forms.Button();
            this.Complete_Product = new System.Windows.Forms.Label();
            this.Fail_Product = new System.Windows.Forms.Label();
            this.Pass_Product = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Table_Layout)).BeginInit();
            this.SuspendLayout();
            // 
            // Table_Layout
            // 
            this.Table_Layout.AllowUserToOrderColumns = true;
            this.Table_Layout.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Table_Layout.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Table_Layout.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Table_Layout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Table_Layout.DefaultCellStyle = dataGridViewCellStyle5;
            this.Table_Layout.Location = new System.Drawing.Point(12, 94);
            this.Table_Layout.Name = "Table_Layout";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Table_Layout.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Table_Layout.Size = new System.Drawing.Size(733, 471);
            this.Table_Layout.TabIndex = 10;
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Location = new System.Drawing.Point(606, 12);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(139, 32);
            this.Exit.TabIndex = 11;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // File_Export
            // 
            this.File_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.File_Export.Location = new System.Drawing.Point(606, 50);
            this.File_Export.Name = "File_Export";
            this.File_Export.Size = new System.Drawing.Size(139, 32);
            this.File_Export.TabIndex = 12;
            this.File_Export.Text = "Export";
            this.File_Export.UseVisualStyleBackColor = true;
            this.File_Export.Click += new System.EventHandler(this.File_Export_Click);
            // 
            // Complete_Product
            // 
            this.Complete_Product.AutoSize = true;
            this.Complete_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Complete_Product.Location = new System.Drawing.Point(13, 12);
            this.Complete_Product.Name = "Complete_Product";
            this.Complete_Product.Size = new System.Drawing.Size(189, 25);
            this.Complete_Product.TabIndex = 13;
            this.Complete_Product.Text = "Complete Product:";
            // 
            // Fail_Product
            // 
            this.Fail_Product.AutoSize = true;
            this.Fail_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fail_Product.Location = new System.Drawing.Point(13, 37);
            this.Fail_Product.Name = "Fail_Product";
            this.Fail_Product.Size = new System.Drawing.Size(133, 25);
            this.Fail_Product.TabIndex = 14;
            this.Fail_Product.Text = "Fail Product:";
            // 
            // Pass_Product
            // 
            this.Pass_Product.AutoSize = true;
            this.Pass_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pass_Product.Location = new System.Drawing.Point(13, 62);
            this.Pass_Product.Name = "Pass_Product";
            this.Pass_Product.Size = new System.Drawing.Size(146, 25);
            this.Pass_Product.TabIndex = 15;
            this.Pass_Product.Text = "Pass Product:";
            // 
            // Test_Result_Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(757, 577);
            this.Controls.Add(this.Pass_Product);
            this.Controls.Add(this.Fail_Product);
            this.Controls.Add(this.Complete_Product);
            this.Controls.Add(this.File_Export);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Table_Layout);
            this.Name = "Test_Result_Reader";
            this.Text = "Test_Result_Reader";
            ((System.ComponentModel.ISupportInitialize)(this.Table_Layout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Table_Layout;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button File_Export;
        private System.Windows.Forms.Label Complete_Product;
        private System.Windows.Forms.Label Fail_Product;
        private System.Windows.Forms.Label Pass_Product;
    }
}