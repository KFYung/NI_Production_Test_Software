namespace NI_Test_Software
{
    partial class Test_Record_Template
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
            this.Save = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.Column = new System.Windows.Forms.Label();
            this.Column_Input = new System.Windows.Forms.TextBox();
            this.TableRC_Set = new System.Windows.Forms.Button();
            this.Table_Layout = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.Label();
            this.Row_Input = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Table_Layout)).BeginInit();
            this.SuspendLayout();
            // 
            // Save
            // 
            this.Save.AutoSize = true;
            this.Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save.Location = new System.Drawing.Point(397, 12);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(81, 35);
            this.Save.TabIndex = 0;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Reset
            // 
            this.Reset.AutoSize = true;
            this.Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reset.Location = new System.Drawing.Point(484, 12);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(94, 35);
            this.Reset.TabIndex = 1;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Exit
            // 
            this.Exit.AutoSize = true;
            this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Location = new System.Drawing.Point(584, 12);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(94, 35);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Column
            // 
            this.Column.AutoSize = true;
            this.Column.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column.Location = new System.Drawing.Point(12, 31);
            this.Column.Name = "Column";
            this.Column.Size = new System.Drawing.Size(59, 16);
            this.Column.TabIndex = 6;
            this.Column.Text = "Column";
            // 
            // Column_Input
            // 
            this.Column_Input.Location = new System.Drawing.Point(77, 30);
            this.Column_Input.Name = "Column_Input";
            this.Column_Input.Size = new System.Drawing.Size(100, 20);
            this.Column_Input.TabIndex = 7;
            // 
            // TableRC_Set
            // 
            this.TableRC_Set.AutoSize = true;
            this.TableRC_Set.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableRC_Set.Location = new System.Drawing.Point(183, 12);
            this.TableRC_Set.Name = "TableRC_Set";
            this.TableRC_Set.Size = new System.Drawing.Size(54, 35);
            this.TableRC_Set.TabIndex = 8;
            this.TableRC_Set.Text = "Set";
            this.TableRC_Set.UseVisualStyleBackColor = true;
            this.TableRC_Set.Click += new System.EventHandler(this.TableRC_Set_Click);
            // 
            // Table_Layout
            // 
            this.Table_Layout.AllowUserToOrderColumns = true;
            this.Table_Layout.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Table_Layout.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Table_Layout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table_Layout.Location = new System.Drawing.Point(12, 57);
            this.Table_Layout.Name = "Table_Layout";
            this.Table_Layout.Size = new System.Drawing.Size(666, 471);
            this.Table_Layout.TabIndex = 9;
            // 
            // Row
            // 
            this.Row.AutoSize = true;
            this.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Row.Location = new System.Drawing.Point(12, 5);
            this.Row.Name = "Row";
            this.Row.Size = new System.Drawing.Size(38, 16);
            this.Row.TabIndex = 10;
            this.Row.Text = "Row";
            // 
            // Row_Input
            // 
            this.Row_Input.Location = new System.Drawing.Point(77, 4);
            this.Row_Input.Name = "Row_Input";
            this.Row_Input.Size = new System.Drawing.Size(100, 20);
            this.Row_Input.TabIndex = 11;
            // 
            // Test_Record_Template
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(690, 540);
            this.Controls.Add(this.Row_Input);
            this.Controls.Add(this.Row);
            this.Controls.Add(this.Table_Layout);
            this.Controls.Add(this.TableRC_Set);
            this.Controls.Add(this.Column_Input);
            this.Controls.Add(this.Column);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Save);
            this.Name = "Test_Record_Template";
            this.Text = "Test_Record_Template";
            ((System.ComponentModel.ISupportInitialize)(this.Table_Layout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Label Column;
        private System.Windows.Forms.TextBox Column_Input;
        private System.Windows.Forms.Button TableRC_Set;
        private System.Windows.Forms.DataGridView Table_Layout;
        private System.Windows.Forms.Label Row;
        private System.Windows.Forms.TextBox Row_Input;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}