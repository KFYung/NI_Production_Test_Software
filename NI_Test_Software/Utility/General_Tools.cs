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
using System.Reflection;
using System.Diagnostics;
using System.Deployment.Application;

namespace NI_Test_Software.Utility
{
    class General_Tools
    {
        public static string GetVersion()
        {
            // Place in local code
            Assembly ass = Assembly.GetExecutingAssembly();
            string product;
            string version;
            if (ass != null)
            {
                FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(ass.Location);

                product = FVI.ProductName;

                if (ApplicationDeployment.IsNetworkDeployed)
                    version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                else
                    version = String.Format("Version ({0:0}.{1:0}.{2:0})",
                              FVI.FileMajorPart.ToString(),
                              FVI.FileMinorPart.ToString(),
                              FVI.FileBuildPart.ToString());
                return product + " " + version;
            }
            else
            {
                return "Unknown";
            }
        }

        public string ConvertStringArrayToString(string[] array)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            StringBuilder stringbuilder = new StringBuilder();
            foreach (string value in array)
            {
                stringbuilder.Append(value);
                stringbuilder.Append(' ');
            }
            return stringbuilder.ToString();
        }

        public string ConvertStringArrayStringToString(string[] array, int Index)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            string temp = "";
            temp = string.Join("", array[Index]);
            return temp;
        }

        public bool string_number_check(string message)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(message, @"\A\b[0-9]+\b\Z"))
            {
                return true;
            }
            else
                return false;
        }

        public bool string_letter_check(string message)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(message, @"\A\b[a-zA-z]+\b\Z"))
            {
                return true;
            }
            else
                return false;
        }

        public bool string_unichar_string_check(string message, char pattern)
        {
            foreach (char c in message)
            {
                if (c != pattern)
                    return false;
            }
            return true;
        }

        public bool result_saving(string path, string data)
        {
            return false;
        }

        public static string[] result_file_string = {
                                                      "Completed Product:",
                                                      "Fail Product:",
                                                      "Pass Product:",
                                                      "",
                                                   };
        
        public enum result_file
        { 
            Complete_Product = 0,
            Fail_Product,
            Pass_Product,
            Dummy,
            End
        }

        public object[] result_file_extractor(string path)
        {
            object[] result = new object[(int) result_file.End];

            StreamReader test_result_file = new StreamReader(path);
            Int64 CompleteProduct;
            Int64 FailProduct;
            Int64 PassProduct;
            List<string> record = new List<string>();

            //Complete Product
            CompleteProduct = Convert.ToInt64(test_result_file.ReadLine().Replace(result_file_string[(int)result_file.Complete_Product] + "\t", ""));
            
            //Fail Product
            FailProduct = Convert.ToInt64(test_result_file.ReadLine().Replace(result_file_string[(int)result_file.Fail_Product] + "\t", ""));

            //Pass Product
            PassProduct = Convert.ToInt64(test_result_file.ReadLine().Replace(result_file_string[(int)result_file.Pass_Product] + "\t", ""));

            string temp = test_result_file.ReadLine(); //Read out Dummy

            while ((temp = test_result_file.ReadLine()) != null)
            {
                record.Add(temp);
            }
            test_result_file.Close();

            //Constructing the Result
            result[(int)result_file.Complete_Product] = CompleteProduct;
            result[(int)result_file.Fail_Product] = FailProduct;
            result[(int)result_file.Pass_Product] = PassProduct;
            result[(int)result_file.Dummy] = record;                

            return result;
        }

        public void Draw_Prograss_Bar_Txt(System.Windows.Forms.ProgressBar Prograss_Bar, string txt, Font font)
        {
            using (Graphics text = Prograss_Bar.CreateGraphics())
            {
                text.DrawString(txt, font, Brushes.Black, new PointF(Prograss_Bar.Width / 2 - (text.MeasureString(txt, font).Width / 2.0F), Prograss_Bar.Height / 2 - (text.MeasureString(txt, font).Height / 2.0F)));
            }
        }
    }
}
