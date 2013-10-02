using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NI_Test_Software.Instruction_Operation;

namespace NI_Test_Software
{
    static class Program
    {
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Form Main_Mode = ;
            //Form Test_Mode;
            Instruction_Executor Instruction_Exe = new Instruction_Executor();
            Instruction_List_Creator Instruction_Creation = new Instruction_List_Creator();
            Flag_Control System_Flag = new Flag_Control();
            string exe_result = "";

            string[] instruction_result = Instruction_Executor.executation_result;
            //string Selected_mode = "Start";            
    
            do
            {
                if (System_Flag.mode == Flag_Control.Mode_Flag_Name[(int)Flag_Control.Mode_Flag.Start])
                {
                    Application.Run(new Main_Form(System_Flag));
                    Application.DoEvents();
                }
                else
                {
                    //For First Time Generate the Question List
                    if (System_Flag.Test_status == (int)Flag_Control.Test_status_Lib.stop)          //If Test is not start do the initial
                    {
                        Instruction_Exe = new Instruction_Executor();
                        exe_result = Instruction_Creation.instruction_list_making(System_Flag.mode);
                        
                        if (exe_result.Contains(instruction_result[(int)Instruction_Executor.exe_result.Fail]))
                        {
                            MessageBox.Show(exe_result);
                            System_Flag.Flag_Reset(); ; //Return Test Selection Mode
                        }
                        else
                        {
                            System_Flag.Test_status = (int)Flag_Control.Test_status_Lib.running;
                        }
                    }
                    else if (System_Flag.Test_status == (int)Flag_Control.Test_status_Lib.running)  //If The Test is Running
                    {
                        exe_result = Instruction_Exe.instruction_list_executation(Instruction_Creation, System_Flag);        //Execute the Instruction 

                        if (
                            (exe_result == instruction_result[(int)Instruction_Executor.exe_result.complete]) ||
                            (exe_result.Contains(instruction_result[(int)Instruction_Executor.exe_result.Incomplete]))
                           )
                        {
                            Instruction_Exe.executation_end();          //Stop All Hardware Task
                            Instruction_Exe.instruction_step = 0;       //Set Back to the First Step
                            if (exe_result.Contains(instruction_result[(int)Instruction_Executor.exe_result.Incomplete]))
                                MessageBox.Show("Top Level Command Error\n" + exe_result + "\n It will go back to Test Selection Mode");
                            System_Flag.Flag_Reset();
                            //Hardware Reset
                            /**********/
                        }
                        
                    }
                    else if (System_Flag.Test_status == (int)Flag_Control.Test_status_Lib.pause)  //If Test main line it pause
                    {
                        if (exe_result == instruction_result[(int)Instruction_Executor.exe_result.Q_Start])
                        {
                            Application.Run(new Test_Template_Form(System_Flag, Instruction_Creation, Instruction_Exe));
                        }
                    }
                    else if (System_Flag.Test_status == (int)Flag_Control.Test_status_Lib.resume)  //If Test main line it pause
                    {

                        System_Flag.Test_status = (int)Flag_Control.Test_status_Lib.running;       //A Step to Resume it to Main Thread
                    }
                }

            } while (System_Flag.mode != "");
            
        }
    } 
}
