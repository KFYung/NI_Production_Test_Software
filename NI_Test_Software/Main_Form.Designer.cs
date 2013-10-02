namespace NI_Test_Software
{
    partial class Main_Form
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
            this.Test_Flow_Selection_Box = new System.Windows.Forms.ListBox();
            this.Test_Result_Count = new System.Windows.Forms.ListBox();
            this.Peripheral_Test = new System.Windows.Forms.Label();
            this.Barcode_Hex_TXT = new System.Windows.Forms.TextBox();
            this.Hex_Printer_Test_Button = new System.Windows.Forms.Button();
            this.Barcode_Dec_TXT = new System.Windows.Forms.TextBox();
            this.Hex_Format = new System.Windows.Forms.Label();
            this.Dec_Format = new System.Windows.Forms.Label();
            this.Dec_Printer_Test_Button = new System.Windows.Forms.Button();
            this.Hardware_Test = new System.Windows.Forms.Label();
            this.Hardware_Test_List = new System.Windows.Forms.CheckedListBox();
            this.Hardware_Test_Button = new System.Windows.Forms.Button();
            this.Test_Start_Button = new System.Windows.Forms.Button();
            this.Test_End_Button = new System.Windows.Forms.Button();
            this.Test_Progress_Bar = new System.Windows.Forms.ProgressBar();
            this.Test_Prograss_Label = new System.Windows.Forms.Label();
            this.Record_Table_Layout = new System.Windows.Forms.Button();
            this.Test_Result_Reader = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Test_Flow_Selection_Box
            // 
            this.Test_Flow_Selection_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test_Flow_Selection_Box.FormattingEnabled = true;
            this.Test_Flow_Selection_Box.ItemHeight = 20;
            this.Test_Flow_Selection_Box.Items.AddRange(new object[] {
            "No Test Flow are found",
            "Please add Test Flow under the",
            "Folder \"Test_List\""});
            this.Test_Flow_Selection_Box.Location = new System.Drawing.Point(13, 13);
            this.Test_Flow_Selection_Box.Name = "Test_Flow_Selection_Box";
            this.Test_Flow_Selection_Box.Size = new System.Drawing.Size(249, 544);
            this.Test_Flow_Selection_Box.TabIndex = 0;
            this.Test_Flow_Selection_Box.SelectedIndexChanged += new System.EventHandler(this.Test_Flow_Selection_Box_SelectedIndexChanged);
            // 
            // Test_Result_Count
            // 
            this.Test_Result_Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test_Result_Count.FormattingEnabled = true;
            this.Test_Result_Count.ItemHeight = 20;
            this.Test_Result_Count.Location = new System.Drawing.Point(268, 13);
            this.Test_Result_Count.Name = "Test_Result_Count";
            this.Test_Result_Count.Size = new System.Drawing.Size(46, 544);
            this.Test_Result_Count.TabIndex = 3;
            this.Test_Result_Count.SelectedIndexChanged += new System.EventHandler(this.Test_Result_Count_SelectedIndexChanged);
            // 
            // Peripheral_Test
            // 
            this.Peripheral_Test.AutoSize = true;
            this.Peripheral_Test.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Peripheral_Test.Location = new System.Drawing.Point(320, 245);
            this.Peripheral_Test.Name = "Peripheral_Test";
            this.Peripheral_Test.Size = new System.Drawing.Size(144, 29);
            this.Peripheral_Test.TabIndex = 4;
            this.Peripheral_Test.Text = "Printer Test";
            // 
            // Barcode_Hex_TXT
            // 
            this.Barcode_Hex_TXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Barcode_Hex_TXT.Location = new System.Drawing.Point(320, 298);
            this.Barcode_Hex_TXT.Name = "Barcode_Hex_TXT";
            this.Barcode_Hex_TXT.Size = new System.Drawing.Size(359, 22);
            this.Barcode_Hex_TXT.TabIndex = 5;
            this.Barcode_Hex_TXT.TextChanged += new System.EventHandler(this.Barcode_Hex_TXT_TextChanged);
            // 
            // Hex_Printer_Test_Button
            // 
            this.Hex_Printer_Test_Button.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hex_Printer_Test_Button.Location = new System.Drawing.Point(685, 298);
            this.Hex_Printer_Test_Button.Name = "Hex_Printer_Test_Button";
            this.Hex_Printer_Test_Button.Size = new System.Drawing.Size(55, 22);
            this.Hex_Printer_Test_Button.TabIndex = 6;
            this.Hex_Printer_Test_Button.Text = "Print";
            this.Hex_Printer_Test_Button.UseVisualStyleBackColor = true;
            this.Hex_Printer_Test_Button.Click += new System.EventHandler(this.Hex_Printer_Test_Button_Click);
            // 
            // Barcode_Dec_TXT
            // 
            this.Barcode_Dec_TXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Barcode_Dec_TXT.Location = new System.Drawing.Point(320, 344);
            this.Barcode_Dec_TXT.Name = "Barcode_Dec_TXT";
            this.Barcode_Dec_TXT.Size = new System.Drawing.Size(361, 22);
            this.Barcode_Dec_TXT.TabIndex = 7;
            this.Barcode_Dec_TXT.TextChanged += new System.EventHandler(this.Barcode_Dec_TXT_TextChanged);
            // 
            // Hex_Format
            // 
            this.Hex_Format.AutoSize = true;
            this.Hex_Format.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hex_Format.Location = new System.Drawing.Point(320, 277);
            this.Hex_Format.Name = "Hex_Format";
            this.Hex_Format.Size = new System.Drawing.Size(89, 18);
            this.Hex_Format.TabIndex = 8;
            this.Hex_Format.Text = "Hex Format";
            // 
            // Dec_Format
            // 
            this.Dec_Format.AutoSize = true;
            this.Dec_Format.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dec_Format.Location = new System.Drawing.Point(322, 324);
            this.Dec_Format.Name = "Dec_Format";
            this.Dec_Format.Size = new System.Drawing.Size(91, 18);
            this.Dec_Format.TabIndex = 9;
            this.Dec_Format.Text = "Dec Format";
            // 
            // Dec_Printer_Test_Button
            // 
            this.Dec_Printer_Test_Button.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dec_Printer_Test_Button.Location = new System.Drawing.Point(687, 344);
            this.Dec_Printer_Test_Button.Name = "Dec_Printer_Test_Button";
            this.Dec_Printer_Test_Button.Size = new System.Drawing.Size(55, 22);
            this.Dec_Printer_Test_Button.TabIndex = 10;
            this.Dec_Printer_Test_Button.Text = "Print";
            this.Dec_Printer_Test_Button.UseVisualStyleBackColor = true;
            this.Dec_Printer_Test_Button.Click += new System.EventHandler(this.Dec_Printer_Test_Button_Click);
            // 
            // Hardware_Test
            // 
            this.Hardware_Test.AutoSize = true;
            this.Hardware_Test.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hardware_Test.Location = new System.Drawing.Point(320, 379);
            this.Hardware_Test.Name = "Hardware_Test";
            this.Hardware_Test.Size = new System.Drawing.Size(175, 29);
            this.Hardware_Test.TabIndex = 11;
            this.Hardware_Test.Text = "Hardware Test";
            // 
            // Hardware_Test_List
            // 
            this.Hardware_Test_List.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hardware_Test_List.FormattingEnabled = true;
            this.Hardware_Test_List.Items.AddRange(new object[] {
            "Power Supply",
            "Switch",
            "Voltage Detection "});
            this.Hardware_Test_List.Location = new System.Drawing.Point(320, 411);
            this.Hardware_Test_List.Name = "Hardware_Test_List";
            this.Hardware_Test_List.Size = new System.Drawing.Size(256, 64);
            this.Hardware_Test_List.TabIndex = 12;
            // 
            // Hardware_Test_Button
            // 
            this.Hardware_Test_Button.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hardware_Test_Button.Location = new System.Drawing.Point(320, 481);
            this.Hardware_Test_Button.Name = "Hardware_Test_Button";
            this.Hardware_Test_Button.Size = new System.Drawing.Size(256, 30);
            this.Hardware_Test_Button.TabIndex = 13;
            this.Hardware_Test_Button.Text = "Test";
            this.Hardware_Test_Button.UseVisualStyleBackColor = true;
            this.Hardware_Test_Button.Click += new System.EventHandler(this.Hardware_Test_Button_Click);
            // 
            // Test_Start_Button
            // 
            this.Test_Start_Button.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test_Start_Button.Location = new System.Drawing.Point(320, 13);
            this.Test_Start_Button.Name = "Test_Start_Button";
            this.Test_Start_Button.Size = new System.Drawing.Size(208, 113);
            this.Test_Start_Button.TabIndex = 14;
            this.Test_Start_Button.Text = "Start";
            this.Test_Start_Button.UseVisualStyleBackColor = true;
            this.Test_Start_Button.Click += new System.EventHandler(this.Test_Start_Button_Click);
            // 
            // Test_End_Button
            // 
            this.Test_End_Button.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test_End_Button.Location = new System.Drawing.Point(534, 13);
            this.Test_End_Button.Name = "Test_End_Button";
            this.Test_End_Button.Size = new System.Drawing.Size(208, 113);
            this.Test_End_Button.TabIndex = 15;
            this.Test_End_Button.Text = "Exit";
            this.Test_End_Button.UseVisualStyleBackColor = true;
            this.Test_End_Button.Click += new System.EventHandler(this.Test_End_Button_Click);
            // 
            // Test_Progress_Bar
            // 
            this.Test_Progress_Bar.Location = new System.Drawing.Point(320, 517);
            this.Test_Progress_Bar.Name = "Test_Progress_Bar";
            this.Test_Progress_Bar.Size = new System.Drawing.Size(422, 40);
            this.Test_Progress_Bar.TabIndex = 16;
            this.Test_Progress_Bar.UseWaitCursor = true;
            this.Test_Progress_Bar.Click += new System.EventHandler(this.Test_Progress_Bar_Click);
            // 
            // Test_Prograss_Label
            // 
            this.Test_Prograss_Label.AutoSize = true;
            this.Test_Prograss_Label.BackColor = System.Drawing.Color.Transparent;
            this.Test_Prograss_Label.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test_Prograss_Label.Location = new System.Drawing.Point(327, 523);
            this.Test_Prograss_Label.Name = "Test_Prograss_Label";
            this.Test_Prograss_Label.Size = new System.Drawing.Size(0, 29);
            this.Test_Prograss_Label.TabIndex = 17;
            // 
            // Record_Table_Layout
            // 
            this.Record_Table_Layout.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Record_Table_Layout.Location = new System.Drawing.Point(320, 146);
            this.Record_Table_Layout.Name = "Record_Table_Layout";
            this.Record_Table_Layout.Size = new System.Drawing.Size(208, 69);
            this.Record_Table_Layout.TabIndex = 18;
            this.Record_Table_Layout.Text = "Test Record Table Layout";
            this.Record_Table_Layout.UseCompatibleTextRendering = true;
            this.Record_Table_Layout.UseVisualStyleBackColor = true;
            this.Record_Table_Layout.Click += new System.EventHandler(this.Record_Table_Layout_Click);
            // 
            // Test_Result_Reader
            // 
            this.Test_Result_Reader.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test_Result_Reader.Location = new System.Drawing.Point(534, 146);
            this.Test_Result_Reader.Name = "Test_Result_Reader";
            this.Test_Result_Reader.Size = new System.Drawing.Size(208, 69);
            this.Test_Result_Reader.TabIndex = 19;
            this.Test_Result_Reader.Text = "Test Result Reader";
            this.Test_Result_Reader.UseCompatibleTextRendering = true;
            this.Test_Result_Reader.UseVisualStyleBackColor = true;
            this.Test_Result_Reader.Click += new System.EventHandler(this.Test_Result_Export_Click);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 574);
            this.Controls.Add(this.Test_Result_Reader);
            this.Controls.Add(this.Record_Table_Layout);
            this.Controls.Add(this.Test_Prograss_Label);
            this.Controls.Add(this.Test_Progress_Bar);
            this.Controls.Add(this.Test_End_Button);
            this.Controls.Add(this.Test_Start_Button);
            this.Controls.Add(this.Hardware_Test_Button);
            this.Controls.Add(this.Hardware_Test_List);
            this.Controls.Add(this.Hardware_Test);
            this.Controls.Add(this.Dec_Printer_Test_Button);
            this.Controls.Add(this.Dec_Format);
            this.Controls.Add(this.Hex_Format);
            this.Controls.Add(this.Barcode_Dec_TXT);
            this.Controls.Add(this.Hex_Printer_Test_Button);
            this.Controls.Add(this.Barcode_Hex_TXT);
            this.Controls.Add(this.Peripheral_Test);
            this.Controls.Add(this.Test_Result_Count);
            this.Controls.Add(this.Test_Flow_Selection_Box);
            this.Name = "Main_Form";
            this.Text = "GS Testing Software 2013";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Test_Flow_Selection_Box;
        private System.Windows.Forms.ListBox Test_Result_Count;
        private System.Windows.Forms.Label Peripheral_Test;
        private System.Windows.Forms.TextBox Barcode_Hex_TXT;
        private System.Windows.Forms.Button Hex_Printer_Test_Button;
        private System.Windows.Forms.TextBox Barcode_Dec_TXT;
        private System.Windows.Forms.Label Hex_Format;
        private System.Windows.Forms.Label Dec_Format;
        private System.Windows.Forms.Button Dec_Printer_Test_Button;
        private System.Windows.Forms.Label Hardware_Test;
        private System.Windows.Forms.CheckedListBox Hardware_Test_List;
        private System.Windows.Forms.Button Hardware_Test_Button;
        private System.Windows.Forms.Button Test_Start_Button;
        private System.Windows.Forms.Button Test_End_Button;
        private System.Windows.Forms.ProgressBar Test_Progress_Bar;
        private System.Windows.Forms.Label Test_Prograss_Label;
        private System.Windows.Forms.Button Record_Table_Layout;
        private System.Windows.Forms.Button Test_Result_Reader;
    }
}

