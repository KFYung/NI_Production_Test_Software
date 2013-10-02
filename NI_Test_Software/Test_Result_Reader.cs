using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NI_Test_Software.Utility;

namespace NI_Test_Software
{
    public partial class Test_Result_Reader : Form
    {
        General_Tools Common_Tool = new General_Tools();
        private string file_path;
        private string FileName;
        private string Title_path;
        private List<string> buffer = new List<string>();

        public Test_Result_Reader(string path, string Title, string File_name)
        {
            Title_path = Title;
            file_path = path;
            FileName = File_name;
            object[] Test_Result = Common_Tool.result_file_extractor(path);

            InitializeComponent();

            Int64 temp_int = (Int64)Test_Result[(int)General_Tools.result_file.Complete_Product];
            string temp = temp_int.ToString();
            buffer.Add(this.Complete_Product.Text + "\t" + temp);       //Store to Buffer for Result Export
            this.Complete_Product.Text += " " + temp;                   //Set up the Display

            temp_int = (Int64)Test_Result[(int)General_Tools.result_file.Fail_Product];
            temp = temp_int.ToString();
            buffer.Add(this.Fail_Product.Text + "\t" + temp);       //Store to Buffer for Result Export
            this.Fail_Product.Text += " " + temp;                   //Set up the Display

            temp_int = (Int64)Test_Result[(int)General_Tools.result_file.Pass_Product];
            temp = temp_int.ToString();
            buffer.Add(this.Pass_Product.Text + "\t" + temp);       //Store to Buffer for Result Export
            this.Pass_Product.Text += " " + temp;                   //Set up the Display

            buffer.Add("");                                         //Add Dummy Line

            int max_column = 0;
            int column_cnt = 0;
            int remaining_column;
            int row_cnt = 0;


            foreach (string line in File.ReadLines(Title_path))
            {
                if (line.Length > 1)
                {
                    buffer.Add(line);
                    string[] title_txt = line.Split('\t');

                    if (max_column < title_txt.Length)
                    {
                        remaining_column = title_txt.Length - max_column;

                        max_column = title_txt.Length;

                        for (; column_cnt < remaining_column; column_cnt++)
                        {
                            this.Table_Layout.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn());
                            this.Table_Layout.Columns[column_cnt].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
                            this.Table_Layout.Columns[column_cnt].HeaderText = "Column" + column_cnt.ToString();
                            this.Table_Layout.Columns[column_cnt].Name = "Column" + column_cnt.ToString();
                            this.Table_Layout.Columns[column_cnt].Width = 73;

                        }
                    }

                    this.Table_Layout.Rows.Add();

                    for (int c_cnt = 0; c_cnt < max_column; c_cnt++)
                    {
                        this.Table_Layout.Rows[row_cnt].Cells[c_cnt].Value = title_txt[c_cnt];

                    }
                }

                row_cnt++;
            }

            foreach (string line in (List<string>)Test_Result[(int)General_Tools.result_file.Dummy])
            {
                buffer.Add(line);
                if (line.Length > 1)
                {
                    string[] title_txt = line.Split('\t');

                    if (max_column < title_txt.Length)
                    {
                        remaining_column = title_txt.Length - max_column;

                        max_column = title_txt.Length;

                        for (; column_cnt < remaining_column; column_cnt++)
                        {
                            this.Table_Layout.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn());
                            this.Table_Layout.Columns[column_cnt].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
                            this.Table_Layout.Columns[column_cnt].HeaderText = "Column" + column_cnt.ToString();
                            this.Table_Layout.Columns[column_cnt].Name = "Column" + column_cnt.ToString();
                            this.Table_Layout.Columns[column_cnt].Width = 73;

                        }
                    }

                    this.Table_Layout.Rows.Add();

                    for (int c_cnt = 0; c_cnt < max_column; c_cnt++)
                    {
                        if(c_cnt < title_txt.Length)
                            this.Table_Layout.Rows[row_cnt].Cells[c_cnt].Value = title_txt[c_cnt];
                        else
                            this.Table_Layout.Rows[row_cnt].Cells[c_cnt].Value = "";

                        this.Table_Layout.Rows[row_cnt].Cells[c_cnt].ReadOnly = true;
                    }
                }

                row_cnt++;
            }

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            //Close the Reader and Exit
            this.Close();
        }

        private void File_Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog File_Saving = new SaveFileDialog();
            
            File_Saving.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File_Saving.FileName = FileName;
            File_Saving.Filter = "Text Files (*.txt)|*.txt";
            File_Saving.FilterIndex = 1;
            File_Saving.RestoreDirectory = true;
            File_Saving.CheckFileExists = false;
            File_Saving.CheckPathExists = true;

            if (File_Saving.ShowDialog() == DialogResult.OK)
            {
                StreamWriter export_result = new StreamWriter(File_Saving.FileName);

                foreach (string line in buffer)
                {
                    export_result.WriteLine(line);
                }
                export_result.Close();
                
            }
        }
    }
}
