using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NI_Test_Software.NI_Equipment.PXI4110_DCPower;
using NI_Test_Software.NI_Equipment.PXIe6363_DAQ;
using NI_Test_Software.Utility;
using NI_Test_Software.Utility.Printing;

namespace NI_Test_Software
{
    public partial class Main_Form : Form
    {
        public PXIe_6363_DAQ NI_DAQ = new PXIe_6363_DAQ();
        private Flag_Control Main_Form_Flag;

        private bool start_button_pressed = false;
        private string[] Test_List;
        private string[] All_Test_List_Path;
        private string[] All_Test_Result_Path;
        private string[] All_Test_Result_Template_Path;
        private string TestRootPath;
        private string[] Test_List_Num;
        private General_Tools Common_Tool = new General_Tools();
        
        private void Test_List_Prepare()
        {
            TestRootPath = System.Windows.Forms.Application.StartupPath;

            //Check Test Flow Folder
            if (!Directory.Exists(TestRootPath + app.Default.TestListFolder))
            {
                Directory.CreateDirectory(TestRootPath + app.Default.TestListFolder);
            }

            //Check Test Result Folder
            if (!Directory.Exists(TestRootPath + app.Default.TestResultFolder))
            {
                Directory.CreateDirectory(TestRootPath + app.Default.TestResultFolder);
            }

            //Check Test Result Template Folder
            if (!Directory.Exists(TestRootPath + app.Default.TestResultTemplateFolder))
            {
                Directory.CreateDirectory(TestRootPath + app.Default.TestResultTemplateFolder);
            }

            string[] csv_list = Directory.GetFiles(TestRootPath + app.Default.TestListFolder, "*.csv", SearchOption.AllDirectories);
            string[] txt_list = Directory.GetFiles(TestRootPath + app.Default.TestListFolder, "*.txt", SearchOption.AllDirectories);
            
            Test_List = new string[csv_list.Length + txt_list.Length];
            
            All_Test_List_Path = new string[Test_List.Length];
            All_Test_Result_Path = new string[Test_List.Length];
            All_Test_Result_Template_Path = new string[Test_List.Length];

            Test_List_Num = new string[Test_List.Length];
            csv_list.CopyTo(Test_List, 0);
            txt_list.CopyTo(Test_List, csv_list.Length);
            Test_List.CopyTo(All_Test_List_Path, 0);
            Test_List.CopyTo(All_Test_Result_Path, 0);
            Test_List.CopyTo(All_Test_Result_Template_Path, 0);
        }

        private void Test_List_Setup()
        {
            StreamReader Test_Result_Complete_Num;
            StreamReader Test_Flow_Name;
            //Remove the Complete Test List Path
            for (int Test_List_cnt = 0; Test_List_cnt < Test_List.Length; Test_List_cnt++)
            {
                All_Test_Result_Path[Test_List_cnt] = All_Test_Result_Path[Test_List_cnt].Replace(app.Default.TestListFolder, app.Default.TestResultFolder);
                //If Result File not exists, Create it
                if(!File.Exists(All_Test_Result_Path[Test_List_cnt]))
                {
                    StreamWriter result_File = new StreamWriter(All_Test_Result_Path[Test_List_cnt]);
                    

                    result_File.WriteLine(General_Tools.result_file_string[(int)General_Tools.result_file.Complete_Product] + "\t0");
                    result_File.WriteLine(General_Tools.result_file_string[(int)General_Tools.result_file.Fail_Product] + "\t0");
                    result_File.WriteLine(General_Tools.result_file_string[(int)General_Tools.result_file.Pass_Product] + "\t0");
                    result_File.WriteLine(General_Tools.result_file_string[(int)General_Tools.result_file.Dummy]);  //For End of File
                    result_File.Close();
                }

                All_Test_Result_Template_Path[Test_List_cnt] = All_Test_Result_Path[Test_List_cnt].Replace(app.Default.TestResultFolder, app.Default.TestResultTemplateFolder);
                //If Result Template File not exists, Create it
                if (!File.Exists(All_Test_Result_Template_Path[Test_List_cnt]))
                {
                    StreamWriter result_File = new StreamWriter(All_Test_Result_Template_Path[Test_List_cnt]);
                    result_File.WriteLine("");
                    result_File.WriteLine("");  //For End of File
                    result_File.Close();
                }

                //Open Tested Product Record
                Test_Result_Complete_Num = new StreamReader(All_Test_Result_Path[Test_List_cnt]);

                //Start Reading the Total Tested Number
                if ((Test_List_Num[Test_List_cnt] = Test_Result_Complete_Num.ReadLine()) != null)
                {
                    Test_List_Num[Test_List_cnt] = Test_List_Num[Test_List_cnt].Replace(app.Default.TagCompleteNum + '\t', "");
                }
                else
                    Test_List_Num[Test_List_cnt] = "";
                
                //Close the File for safety
                Test_Result_Complete_Num.Close();

                
                //Open the File
                Test_Flow_Name = new StreamReader(All_Test_List_Path[Test_List_cnt]);
                
                //Read the Test Title
                Test_List[Test_List_cnt] = Test_Flow_Name.ReadLine();

                //Check is the Test Title available
                try
                {
                    if (Test_List[Test_List_cnt].StartsWith(app.Default.TagTestFlowName))
                    {
                        Test_List[Test_List_cnt] = Test_List[Test_List_cnt].Replace(app.Default.TagTestFlowName + '\t', "");
                    }
                    else
                        throw new Exception();
                }
                //If no Test File Name
                catch(Exception e)
                {
                    Test_List[Test_List_cnt] = All_Test_List_Path[Test_List_cnt].Replace(TestRootPath + app.Default.TestListFolder + "\\", "");
                }

                Test_Flow_Name.Close();
            }

            if (Test_List != null)
            {
                this.Test_Flow_Selection_Box.Items.Clear();
                this.Test_Flow_Selection_Box.Items.AddRange(Test_List);
                this.Test_Result_Count.Items.Clear();
                this.Test_Result_Count.Items.AddRange(Test_List_Num);
            }
        }

        public Main_Form(Flag_Control System_Flag)
        {
            Main_Form_Flag = System_Flag;       //Pass the System be use in this Template

            //Hardware Initial
           // NI_DAQ.Pin_Library_Initial();
            //string test = NI_DAQ.Pin_Library_Read_Out(PXIe_6363_DAQ.connector.connector_0, 34, PXIe_6363_DAQ.pin_data_type.polarity);

            //Prepare the Test List Element
            Test_List_Prepare();
            
            InitializeComponent();

            //Setup the Test List Data for UI
            Test_List_Setup();
            
        }

        private void Test_Flow_Selection_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Test_Result_Count_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Barcode_Hex_TXT_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(this.Barcode_Hex_TXT.Text, @"\A\b[0-9a-fA-F]+\b\Z"))
            {
                try
                {
                    this.Barcode_Dec_TXT.Text = int.Parse(this.Barcode_Hex_TXT.Text, System.Globalization.NumberStyles.HexNumber).ToString();
                }
                catch (Exception ex)
                {
                    this.Barcode_Dec_TXT.Text = "";
                }
            }
            else if (this.Barcode_Hex_TXT.Text == "")
            {
                this.Barcode_Dec_TXT.Text = "";
            }
        }

        private void Hex_Printer_Test_Button_Click(object sender, EventArgs e)
        {
            Bartender_Print print_out = new Bartender_Print();
            print_out.Test_Print(this.Barcode_Hex_TXT.Text);
        }

        private void Barcode_Dec_TXT_TextChanged(object sender, EventArgs e)
        {
            int check;
            if (int.TryParse(this.Barcode_Dec_TXT.Text, out check))
            {
                try
                {
                    this.Barcode_Hex_TXT.Text = Int32.Parse(this.Barcode_Dec_TXT.Text).ToString("X");
                }
                catch (Exception ex)
                { 
                    this.Barcode_Hex_TXT.Text = ""; 
                }
            }
            else if (this.Barcode_Dec_TXT.Text == "")
            {
                this.Barcode_Hex_TXT.Text = ""; 
            }
        }

        private void Dec_Printer_Test_Button_Click(object sender, EventArgs e)
        {
            Bartender_Print print_out = new Bartender_Print();
            print_out.Test_Print(this.Barcode_Dec_TXT.Text);
        }

        public delegate void PS_Self_Test();
        public void PS_Thread_Self_Test()
        {
            this.BeginInvoke(new PS_Self_Test(Power_Supply_Self_Test), new Object[]{});
        }

        public void Power_Supply_Self_Test_Display_Setup()
        {
            this.Test_Progress_Bar.UseWaitCursor = true;
            this.Test_Progress_Bar.Value = 0;
            this.Test_Progress_Bar.Minimum = 0;
            this.Test_Progress_Bar.Maximum = 1;
            this.Test_Progress_Bar.UseWaitCursor = false;
            
            Common_Tool.Draw_Prograss_Bar_Txt(
                                    this.Test_Progress_Bar,
                                    "DC Power Test Start", 
                                    new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
                                 );
        }

        public void Power_Supply_Self_Test()
        {

            PXI4110_DCPower DC_Self_Test = new PXI4110_DCPower();
            this.Test_Progress_Bar.Value = 1;
                                    
            Common_Tool.Draw_Prograss_Bar_Txt(
                                   this.Test_Progress_Bar,
                                   DC_Self_Test.NIDC_Selftest(), 
                                   new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
                                );
        }

        private void Hardware_Test_Button_Click(object sender, EventArgs e)
        {
            this.Hardware_Test_List.SetItemChecked(0, this.Hardware_Test_List.GetItemChecked(0));
            this.Hardware_Test_List.SetItemChecked(1, this.Hardware_Test_List.GetItemChecked(1));
            this.Hardware_Test_List.SetItemChecked(2, this.Hardware_Test_List.GetItemChecked(2));
            
            Thread HW_Test;

            if (this.Hardware_Test_List.GetItemChecked(0))
            {
                Power_Supply_Self_Test_Display_Setup();
                HW_Test = new Thread(new ThreadStart(PS_Thread_Self_Test));
                HW_Test.Start();
            }
            else if (this.Hardware_Test_List.GetItemChecked(1))
            {
            }
            else if (this.Hardware_Test_List.GetItemChecked(2))
            { 
            }
        }

        private void Test_Start_Button_Click(object sender, EventArgs e)
        {
            if (this.Test_Flow_Selection_Box.SelectedIndex >= 0)
            {
                start_button_pressed = true;
                Main_Form_Flag.mode = Common_Tool.ConvertStringArrayStringToString(this.All_Test_List_Path, this.Test_Flow_Selection_Box.SelectedIndex);
                this.Close();
            }
        }

        private void Test_End_Button_Click(object sender, EventArgs e)
        {
            Main_Form_Flag.mode = Flag_Control.Mode_Flag_Name[(int)Flag_Control.Mode_Flag.Exit];
            this.Close();
        }

        private void Test_Progress_Bar_Click(object sender, EventArgs e)
        {

        }

        private void Main_Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (start_button_pressed == true)
                start_button_pressed = false;
            else
                Main_Form_Flag.mode = Flag_Control.Mode_Flag_Name[(int)Flag_Control.Mode_Flag.Exit];
        }

        private void Record_Table_Layout_Click(object sender, EventArgs e)
        {
            if (this.Test_Flow_Selection_Box.SelectedItem != null)
            {
                Test_Record_Template table = new Test_Record_Template(Common_Tool.ConvertStringArrayStringToString(this.All_Test_Result_Template_Path, this.Test_Flow_Selection_Box.SelectedIndex));
                table.ShowDialog();
            }
            else
                MessageBox.Show("Please Highlight a Test for Test Layout Setup");
        }

        private void Test_Result_Export_Click(object sender, EventArgs e)
        {
            
            if (this.Test_Flow_Selection_Box.SelectedItem != null)
            {
                string File_path = Common_Tool.ConvertStringArrayStringToString(this.All_Test_Result_Path, this.Test_Flow_Selection_Box.SelectedIndex);
                string Title_path = Common_Tool.ConvertStringArrayStringToString(this.All_Test_Result_Template_Path, this.Test_Flow_Selection_Box.SelectedIndex);
                string[] file_name = File_path.Split('\\');
                Test_Result_Reader table = new Test_Result_Reader(File_path, Title_path, file_name[file_name.Length - 1]);
                table.ShowDialog();
            }
            else
                MessageBox.Show("Please Highlight a Test for Test Result Export");

        }
    }
}
