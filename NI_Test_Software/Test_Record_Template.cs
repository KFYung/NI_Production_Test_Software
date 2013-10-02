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
    public partial class Test_Record_Template : Form
    {
        private General_Tools tools = new General_Tools();
        private string Test_path;
        private Dictionary<Int64, string[]> file = new Dictionary<Int64, string[]>();
        private Int64[] title_loc = new Int64[2];
        private bool title_found = false;

        private enum title_pos
        { 
            start = 0,
            end,
        }
        private int current_row_num;
        private int current_column_num;

        private enum table_draw_result
        { 
            row = 0,
            column,
        }

        private int[] table_drawing(string path, int row_limit, int col_limit)
        {
            int max_column = 0;
            int remaining_column = 0;
            int column_cnt = 0;
            int row_cnt = 0;

            foreach (string line in File.ReadLines(path))
            {
                if (line.Length > 1)
                {
                    string[] title_txt = line.Split('\t');

                    if (col_limit < title_txt.Length)
                    {
                        remaining_column = title_txt.Length - col_limit;

                        col_limit = title_txt.Length;

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
                    if (col_limit > 0)
                        max_column = col_limit;
                    else
                        max_column = title_txt.Length;

                    for (int c_cnt = 0; c_cnt < max_column; c_cnt++)
                    {
                        if (c_cnt < title_txt.Length)
                            this.Table_Layout.Rows[row_cnt].Cells[c_cnt].Value = title_txt[c_cnt];
                        else
                            this.Table_Layout.Rows[row_cnt].Cells[c_cnt].Value = "";
                    }
                }

                row_cnt++;
            }

            while (row_limit > row_cnt)
            {
                for (int c_cnt = 0; c_cnt < col_limit; c_cnt++)
                {
                    this.Table_Layout.Rows[row_cnt].Cells[c_cnt].Value = "";
                }
                row_cnt++;
            }

            int[] max_row_column = {row_cnt, max_column};

            return max_row_column;
        }

        public Test_Record_Template(string path)
        {
            Test_path = path;
            InitializeComponent();

            if (File.Exists(path))
            {
                int[] max_row_column = table_drawing(path, 0, 0);
                current_row_num = max_row_column[(int)table_draw_result.row];
                current_column_num = max_row_column[(int)table_draw_result.column];
            }
            else
            {
                StreamWriter Template = new StreamWriter(path);
                Template.WriteLine("");
                Template.Close();
            }
        }

        private void TableRC_Set_Click(object sender, EventArgs e)
        {
            int row_num     = 0;
            int column_num  = 0;

            this.Table_Layout.Columns.Clear();
            this.Table_Layout.Rows.Clear();

            if (tools.string_number_check(this.Row_Input.Text))
            {
                current_row_num = row_num = Convert.ToInt16(this.Row_Input.Text);
            }
            
            if (tools.string_number_check(this.Column_Input.Text))
            {
                current_column_num = column_num = Convert.ToInt16(this.Column_Input.Text);
            }

            for (int column_cnt = 0; column_cnt < column_num; column_cnt++)
            {
                this.Table_Layout.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn());
                this.Table_Layout.Columns[column_cnt].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
                this.Table_Layout.Columns[column_cnt].HeaderText = "Column" + column_cnt.ToString();
                this.Table_Layout.Columns[column_cnt].Name = "Column" + column_cnt.ToString();
                this.Table_Layout.Columns[column_cnt].Width = 73;
            }

            for (int row_cnt = 0; row_cnt < row_num; row_cnt++)
            {
                this.Table_Layout.Rows.Add();
            }

            table_drawing(Test_path, current_row_num, current_column_num);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            StreamWriter Test_File = new StreamWriter(Test_path);
 
            for (int row_cnt = 0; row_cnt < current_row_num; row_cnt++)
            {
                string temp = "";
                for (int column_cnt = 0; column_cnt < current_column_num; column_cnt++)
                {
                    temp += this.Table_Layout.Rows[row_cnt].Cells[column_cnt].Value;
                    if (column_cnt != current_column_num - 1)
                        temp += "\t";
                }
                Test_File.WriteLine(temp);
            }
            
            Test_File.Close();
            this.Close();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            this.Table_Layout.Columns.Clear();
            this.Table_Layout.Rows.Clear();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
