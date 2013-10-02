using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NI_Test_Software
{
    public class Flag_Control
    {
        public static string[] Mode_Flag_Name = {
                                                    "",
                                                    "Start",
                                                };
        public enum Mode_Flag
        {
            Exit = 0,
            Start,
        }

        public string mode = Mode_Flag_Name[(int)Mode_Flag.Start];

        public int Test_status = (int)Test_status_Lib.stop;

        public enum Test_status_Lib
        {
            stop = 0,
            running,
            pause,
            resume,
            restart,
            exit
        }

        public void Flag_Reset()
        {
            mode = Mode_Flag_Name[(int)Mode_Flag.Start];
            Test_status = (int)Test_status_Lib.stop;
        }
    }
}
