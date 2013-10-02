using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace NI_Test_Software.config
{
    class Configuration
    {
        public static string BarTenderExe
        {
            get
            {
                return ProgramFilesx86() + app.Default.BarTenderExePath + app.Default.BarTenderExe;


            }
        }
        
        public static string CC2510FlashTool
        {
            get
            {
                return ProgramFilesx86() + app.Default.CC2510FlashToolPath + app.Default.CC2510FlashTool;
            }
        }

        private static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }
}
