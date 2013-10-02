using System;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NI_Test_Software.Utility.Commandline_Launch;
using NI_Test_Software.config;

namespace NI_Test_Software.Utility.Printing
{
    class Bartender_Print
    {
        private string label_print(string aFormFile, string aDataFile)
        {
            string output;
            
            output = LaunchEXE.Run  (
                                    Configuration.BarTenderExe,
                                    String.Format("/F=\"{0}\" /D=\"{1}\" /P /x", aFormFile, aDataFile),
                                    10
                                    );
            return output;
        }

        public void Test_Print(string txt)
        {
            string root_path = System.Windows.Forms.Application.StartupPath;

            if (File.Exists(root_path + app.Default.MedSenseLabelFormFileName))
            {
                string temp_file_path = root_path + app.Default.TempPrintFile;
                StreamWriter temp_file = File.CreateText(temp_file_path);
                temp_file.WriteLine(txt);
                temp_file.Flush();
                temp_file.Close();
                label_print(root_path + app.Default.MedSenseLabelFormFileName, temp_file_path);
                File.Delete(System.Windows.Forms.Application.StartupPath + app.Default.TempPrintFile);
            }
            else 
            {
                MessageBox.Show("Bartender Format not exists!");
            }

        }
    }
}
