namespace NI_Test_Software
{
    partial class Test_Template_Form
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
            this.Test_Label = new System.Windows.Forms.Label();
            this.Test_Image = new System.Windows.Forms.PictureBox();
            this.Current_Date = new System.Windows.Forms.MonthCalendar();
            this.Test_Progress_Bar = new System.Windows.Forms.ProgressBar();
            this.Button_Left = new System.Windows.Forms.Button();
            this.Button_Right = new System.Windows.Forms.Button();
            this.Instruction_and_Result = new System.Windows.Forms.SplitContainer();
            this.Left_Panel_txt = new System.Windows.Forms.Label();
            this.Right_Panel_txt = new System.Windows.Forms.Label();
            this.Result_Pass = new System.Windows.Forms.Label();
            this.Result_Fail = new System.Windows.Forms.Label();
            this.Button_Exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Test_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Instruction_and_Result)).BeginInit();
            this.Instruction_and_Result.SuspendLayout();
            this.SuspendLayout();
            // 
            // Test_Label
            // 
            this.Test_Label.AutoSize = true;
            this.Test_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test_Label.Location = new System.Drawing.Point(12, 9);
            this.Test_Label.Name = "Test_Label";
            this.Test_Label.Size = new System.Drawing.Size(175, 37);
            this.Test_Label.TabIndex = 0;
            this.Test_Label.Text = "Text Label";
            this.Test_Label.Click += new System.EventHandler(this.Test_Label_Click);
            // 
            // Test_Image
            // 
            this.Test_Image.Image = global::NI_Test_Software.Properties.Resources.Company_Logo;
            this.Test_Image.ImageLocation = "";
            this.Test_Image.Location = new System.Drawing.Point(579, 9);
            this.Test_Image.Name = "Test_Image";
            this.Test_Image.Size = new System.Drawing.Size(190, 122);
            this.Test_Image.TabIndex = 1;
            this.Test_Image.TabStop = false;
            // 
            // Current_Date
            // 
            this.Current_Date.Location = new System.Drawing.Point(781, 9);
            this.Current_Date.Name = "Current_Date";
            this.Current_Date.TabIndex = 2;
            // 
            // Test_Progress_Bar
            // 
            this.Test_Progress_Bar.Location = new System.Drawing.Point(19, 137);
            this.Test_Progress_Bar.Name = "Test_Progress_Bar";
            this.Test_Progress_Bar.Size = new System.Drawing.Size(750, 34);
            this.Test_Progress_Bar.TabIndex = 3;
            this.Test_Progress_Bar.Click += new System.EventHandler(this.Test_Progress_Bar_Click);
            // 
            // Button_Left
            // 
            this.Button_Left.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Left.Location = new System.Drawing.Point(19, 622);
            this.Button_Left.Name = "Button_Left";
            this.Button_Left.Size = new System.Drawing.Size(329, 117);
            this.Button_Left.TabIndex = 4;
            this.Button_Left.UseCompatibleTextRendering = true;
            this.Button_Left.UseVisualStyleBackColor = true;
            this.Button_Left.Click += new System.EventHandler(this.Button_Left_Click);
            // 
            // Button_Right
            // 
            this.Button_Right.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Right.Location = new System.Drawing.Point(679, 622);
            this.Button_Right.Name = "Button_Right";
            this.Button_Right.Size = new System.Drawing.Size(329, 117);
            this.Button_Right.TabIndex = 5;
            this.Button_Right.UseCompatibleTextRendering = true;
            this.Button_Right.UseVisualStyleBackColor = true;
            this.Button_Right.Click += new System.EventHandler(this.Button_Right_Click);
            // 
            // Instruction_and_Result
            // 
            this.Instruction_and_Result.Location = new System.Drawing.Point(19, 177);
            this.Instruction_and_Result.Name = "Instruction_and_Result";
            // 
            // Instruction_and_Result.Panel1
            // 
            this.Instruction_and_Result.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Instruction_and_Result_Panel1_Paint);
            // 
            // Instruction_and_Result.Panel2
            // 
            this.Instruction_and_Result.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Instruction_and_Result_Panel2_Paint);
            this.Instruction_and_Result.Size = new System.Drawing.Size(990, 428);
            this.Instruction_and_Result.SplitterDistance = 495;
            this.Instruction_and_Result.TabIndex = 6;
            // 
            // Left_Panel_txt
            // 
            this.Left_Panel_txt.AutoEllipsis = true;
            this.Left_Panel_txt.AutoSize = true;
            this.Left_Panel_txt.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Left_Panel_txt.Location = new System.Drawing.Point(3, 3);
            this.Left_Panel_txt.MaximumSize = new System.Drawing.Size(490, 428);
            this.Left_Panel_txt.MinimumSize = new System.Drawing.Size(490, 428);
            this.Left_Panel_txt.Name = "Left_Panel_txt";
            this.Left_Panel_txt.Size = new System.Drawing.Size(490, 428);
            this.Left_Panel_txt.TabIndex = 0;
            this.Left_Panel_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Right_Panel_txt
            // 
            this.Right_Panel_txt.AutoEllipsis = true;
            this.Right_Panel_txt.AutoSize = true;
            this.Right_Panel_txt.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Right_Panel_txt.Location = new System.Drawing.Point(1, 3);
            this.Right_Panel_txt.MaximumSize = new System.Drawing.Size(490, 428);
            this.Right_Panel_txt.MinimumSize = new System.Drawing.Size(490, 428);
            this.Right_Panel_txt.Name = "Right_Panel_txt";
            this.Right_Panel_txt.Size = new System.Drawing.Size(490, 428);
            this.Right_Panel_txt.TabIndex = 1;
            this.Right_Panel_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Result_Pass
            // 
            this.Result_Pass.AutoSize = true;
            this.Result_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Result_Pass.ForeColor = System.Drawing.Color.Lime;
            this.Result_Pass.Location = new System.Drawing.Point(444, 640);
            this.Result_Pass.Name = "Result_Pass";
            this.Result_Pass.Size = new System.Drawing.Size(150, 42);
            this.Result_Pass.TabIndex = 7;
            this.Result_Pass.Text = "Pass: 0";
            this.Result_Pass.Click += new System.EventHandler(this.Result_Pass_Click);
            // 
            // Result_Fail
            // 
            this.Result_Fail.AutoSize = true;
            this.Result_Fail.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Result_Fail.ForeColor = System.Drawing.Color.Red;
            this.Result_Fail.Location = new System.Drawing.Point(459, 680);
            this.Result_Fail.Name = "Result_Fail";
            this.Result_Fail.Size = new System.Drawing.Size(126, 42);
            this.Result_Fail.TabIndex = 8;
            this.Result_Fail.Text = "Fail: 0";
            this.Result_Fail.Click += new System.EventHandler(this.Result_Fail_Click);
            // 
            // Button_Exit
            // 
            this.Button_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Exit.Location = new System.Drawing.Point(19, 55);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(124, 52);
            this.Button_Exit.TabIndex = 9;
            this.Button_Exit.Text = "Exit";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // Test_Template_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 751);
            //this.Controls.Add(this.Button_Exit);
            this.Controls.Add(this.Result_Fail);
            this.Controls.Add(this.Result_Pass);
            this.Controls.Add(this.Instruction_and_Result);
            //this.Controls.Add(this.Button_Right);
            //this.Controls.Add(this.Button_Left);
            this.Controls.Add(this.Test_Progress_Bar);
            this.Controls.Add(this.Current_Date);
            this.Controls.Add(this.Test_Image);
            this.Controls.Add(this.Test_Label);
            this.Name = "Test_Template_Form";
            this.Text = "GS Testing Software: ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Test_Template_Form_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.Test_Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Instruction_and_Result)).EndInit();
            this.Instruction_and_Result.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Test_Label;
        private System.Windows.Forms.PictureBox Test_Image;
        private System.Windows.Forms.MonthCalendar Current_Date;
        private System.Windows.Forms.ProgressBar Test_Progress_Bar;
        private System.Windows.Forms.Button Button_Left;
        private System.Windows.Forms.Button Button_Right;
        private System.Windows.Forms.SplitContainer Instruction_and_Result;
        private System.Windows.Forms.Label Result_Pass;
        private System.Windows.Forms.Label Result_Fail;
        private System.Windows.Forms.Label Left_Panel_txt;
        private System.Windows.Forms.Label Right_Panel_txt;
        private System.Windows.Forms.Button Button_Exit;
    }
}