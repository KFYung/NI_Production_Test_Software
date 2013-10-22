using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NI_Test_Software.Instruction_Operation
{
    public class Instruction_Executor
    {
        private int current_instruction_step = 0;
        private int current_question = 0;               //This is the counter for which question it is in now.
        private int current_question_list_step = 0;     //This is the counter for the Question Itself ONLY.
        public string path;
        public string buffer_exe_result = "";
        public string[] result = new string[2];
        public bool test_start = false;
        public bool test_pause = false;
        private Instruction_Decoder instruction_library = new Instruction_Decoder();
        public int instruction_mask_cnt = 0;
        public List<string> Error_List = new List<string>();
        public static string[] executation_result =     {
                                                            "Complete",
                                                            "stop",
                                                            "incomplete",
                                                            "Executation Error",
                                                            "Question Start",
                                                            "Question Complete",
                                                            "Pass",
                                                            "Fail",
                                                            "Reset",
                                                            "Measure Error",
                                                            "Pin Assign Error",
                                                            "Out of Range",
                                                         };

        public enum exe_result
        {
            complete = 0,
            Stop,
            Incomplete,
            Exe_Err,
            Q_Start,
            Q_Complete,
            Pass,
            Fail,
            Reset,
            Measure_Err,
            Pin_Err,
            Out_Range_Err,
        }

        public enum question_loc
        {
            start = 0,
            end
        }

        public enum instruction_structure
        {
            step = 0,
            level,
            command,
            Data,
            Error
        }

        public enum record_result
        { 
            temp = 0,
            accumulated_result,
            error_message
        }

        public bool Executation_Status
        {
            get { return test_start; }
            set { test_start = value; }
        }

        public bool Question_Status
        {
            get { return test_pause; }
            set { test_pause = value; }
        }

        public int instruction_step
        {
            get { return current_instruction_step; }
            set { current_instruction_step = value; }
        }

        public int question_step
        {
            get { return current_question; }
            set { current_question = value; }
        }

        public int question_list_step
        {
            get { return current_question_list_step; }
            set { current_question_list_step = value; }
        }

        public int mask_cnt
        {
            get { return instruction_mask_cnt; }
            set { instruction_mask_cnt = value; }
        }

        public string[] recorded_result
        {
            get { return result; }
            set { result = value; }
        }

        public string instruction_list_executation(Instruction_List_Creator Created_List, Flag_Control System_Flag)
        {
            Dictionary<int, List<string>> instruction_list = Created_List.Read_Instruction_List;
            Dictionary<int, int[]> question_instruction_loction = Created_List.Read_Instruction_Question_List;

            if (test_start == false)
            {
                test_start = true;
                //instruction_library.Create_Instruction_Set();
                current_instruction_step = 0;
            }

            try
            {
                for (; current_instruction_step < instruction_list.Count; current_instruction_step++)
                {
                    string command = instruction_list[current_instruction_step][(int)instruction_structure.command];

                    //Pack Up the Data for Instruction Use
                    object[] Parameter_List = Data_Packaging(
                                                                Created_List, 
                                                                this, 
                                                                null, 
                                                                instruction_list[current_instruction_step][(int)instruction_structure.Data], 
                                                                instruction_list[current_instruction_step][(int)instruction_structure.Error], 
                                                                System_Flag
                                                            );

                    Created_List.Temp_Test_Result = (string)instruction_library.read_instruction_libarary[command].Invoke(Parameter_List);      //Instruction Execute here

                    //If instruction executation Fail, Stop it 
                    if (Created_List.Temp_Test_Result.Contains(executation_result[(int)exe_result.Fail]))
                    {
                        return  executation_result[(int)exe_result.Incomplete] + 
                                "\n" + 
                                instruction_list[current_instruction_step][(int)instruction_structure.step] + 
                                "\n" + 
                                Created_List.Temp_Test_Result 
                                ;
                    }
                    else if (Created_List.Temp_Test_Result.Contains(executation_result[(int)exe_result.Measure_Err]))
                    {
                        Error_List.Add(instruction_list[current_instruction_step][(int)instruction_structure.step] + " " + instruction_list[current_instruction_step][(int)instruction_structure.Error] + "\n" + Created_List.Temp_Test_Result);
                    }

                    if (Created_List.Temp_Test_Result == executation_result[(int)exe_result.Q_Start])
                    {
                        return Created_List.Temp_Test_Result;
                    }
                    
                }

                //If all instruction has been Executate
                return executation_result[(int)exe_result.complete];
            }
            catch (Exception e)
            {
                return executation_result[(int)exe_result.Exe_Err] + "\n" + "Step" + current_instruction_step.ToString();
            }
        }


        public string list_executation(Dictionary<int, List<string>> instruction_list, Instruction_List_Creator Created_List, Test_Template_Form Template)
        {
            //Reset to 0
            instruction_mask_cnt = 0;
            
            try
            {
                foreach (KeyValuePair<int, List<string>> instruction in instruction_list)
                {
                    string command = instruction_list[instruction.Key][(int)instruction_structure.command];

                    current_question_list_step = instruction.Key;

                    if (instruction_mask_cnt == 0)
                    {
                        object[] Parameter_List = Data_Packaging(
                                                                    Created_List,
                                                                    this,
                                                                    Template,
                                                                    instruction_list[instruction.Key][(int)instruction_structure.Data],
                                                                    instruction_list[instruction.Key][(int)instruction_structure.Error],
                                                                    Template.template_flag
                                                                );
                        Created_List.Temp_Test_Result = (string)instruction_library.read_instruction_libarary[command].Invoke(Parameter_List);      //Instruction Execute here
                    }
                    else
                        instruction_mask_cnt--;


                    //If instruction executation Fail, Stop it 
                    if (Created_List.Temp_Test_Result.Contains(executation_result[(int)exe_result.Fail]))
                    {
                        return executation_result[(int)exe_result.Incomplete] + "\n" + instruction_list[instruction.Key][(int)instruction_structure.step] + "\n" + Created_List.Temp_Test_Result;
                    }
                    else if (Created_List.Temp_Test_Result.Contains(executation_result[(int)exe_result.Measure_Err]))
                    {
                        Error_List.Add(instruction_list[instruction.Key][(int)instruction_structure.step] + " " + instruction_list[instruction.Key][(int)instruction_structure.Error] + "\n" + Created_List.Temp_Test_Result);
                    }


                }
                //If all instruction has been Executate
                return executation_result[(int)exe_result.complete];

            }
            catch (Exception e)
            {
                return executation_result[(int)exe_result.Exe_Err] + "\n" + "Step" + current_instruction_step.ToString();
            }
        }


        public void executation_end()
        { 
           // instruction_library.Reset_All_Hardware_Task();
           instruction_library = new Instruction_Decoder(); //Create a new Decoder to reset everything 
        }

        private enum data_packaing_pos
        {
            Created_list = 0,
            Executor,
            Template,
            data_string,
            Error_Message,
            System_Flag
        }

        private object[] Data_Packaging(params object[] data)
        {
            object[] packaged_data;

            ArrayList Temp_List = new ArrayList();

            //Add Creaated List
            Temp_List.Add((Instruction_List_Creator)data[(int)data_packaing_pos.Created_list]);            //First Default Parameter

            //Pack Template and Executor into same position
            object[] General_Data = new object[(int)Instruction_Decoder.General_Data.Total_number];
            General_Data[(int)Instruction_Decoder.General_Data.Executor] = (Instruction_Executor)data[(int)data_packaing_pos.Executor];
            General_Data[(int)Instruction_Decoder.General_Data.Template] = (Test_Template_Form)data[(int)data_packaing_pos.Template];
            General_Data[(int)Instruction_Decoder.General_Data.Error_Message] = (string)data[(int)data_packaing_pos.Error_Message];
            General_Data[(int)Instruction_Decoder.General_Data.System_Flag] = (Flag_Control)data[(int)data_packaing_pos.System_Flag];
            Temp_List.Add(General_Data);                //Second Default Parameter

            string parameter = (string)data[(int)data_packaing_pos.data_string];
            Temp_List.AddRange(parameter.Split('\t'));

            packaged_data = Temp_List.ToArray();

            return packaged_data;
        }
    }
}
