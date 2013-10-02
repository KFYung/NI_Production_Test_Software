using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NI_Test_Software.Instruction_Operation
{
    class Instruction_Common_Utilities
    {
        public UInt64   Test_Step_Cnt           = 0;
        public UInt64   Test_Step_Total         = 0;
        public int      Test_Step_Max_Sub_Level = 0;

        private Boolean num_chk(string txt)
        {
            return false;
        }

        private Boolean charater_chk(string txt)
        {
            return false;
        }

        private Boolean File_chk(string path, string extension)
        {
            return false;
        }

        public bool Instruction_Syntax_check(string message)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(message, @"\A\b[:/\|]+\b\Z"))
            {
                return true;
            }
            else
                return false;
        }

    }
}
