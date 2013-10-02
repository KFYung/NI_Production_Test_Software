using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NI_Test_Software.Utility;
using NI_Test_Software.Instruction_Operation;

namespace NI_Test_Software.Instruction_Operation
{
    public class Instruction_List_Creator
    {
        public Dictionary<int, int[]> instruction_question_list = new Dictionary<int, int[]>();
        public Dictionary<int, List<string>> instruction_list = new Dictionary<int,List<string>>();
        public string Test_Title;
        public string Temp_Result;

        private General_Tools private_tool = new General_Tools();
        private Instruction_Decoder instruction_decode_tool = new Instruction_Decoder();
        private bool question_list_recording = false;

        public string Temp_Test_Result
        {
            set { Temp_Result = value; }
            get { return Temp_Result; }
        }
        
        public string Test_Name
        {
            get { return Test_Title; }
        }

        public Dictionary<int, List<string>> Read_Instruction_List
        {
            get 
            {
                return instruction_list;
            }
        }

        public Dictionary<int, int[]> Read_Instruction_Question_List
        {
            get
            {
                return instruction_question_list;
            }
        }

        public string instruction_list_making(string file_path)
        { 
            int question_cnt = 0;
            
            if(!File.Exists(file_path))
            {
                MessageBox.Show("Fail to open the file");
                return "Fail to open the file";
            }


            //Extract the List of Command
            instruction_list.Clear();       //Clean Everything in the List
            instruction_question_list.Clear();  //Clean the All Question List
            
            string[] instruction;
            string txt;

            //instruction_decode_tool.Create_Instruction_Set(); //Prepare the Instruction Set

            StreamReader instruction_step_read = new StreamReader(file_path);
            Test_Title = instruction_step_read.ReadLine();
           
            for(int instruction_step = 0; (txt = instruction_step_read.ReadLine()) != null; instruction_step++)
            {
                if (txt.EndsWith("/"))
                {
                    instruction = txt.Split('\t');
                    for(int cnt = 0; cnt < instruction.Length; cnt++)
                    {
                        switch(cnt)
                        {
                            case 0: //Sequence
                                if (!private_tool.string_number_check(instruction[0]))
                                {
                                    MessageBox.Show(txt + "\n" + "Sequence Error");
                                    return txt + "\n" + "Sequence Error";
                                }
                                else
                                {
                                    instruction_list.Add(instruction_step, new List<string>());
                                    instruction_list[instruction_step].Add(instruction[0]);     //Store the actual Step name
                                }
                                break;
                            case 1: //Level
                                if (private_tool.string_unichar_string_check(instruction[1], '\\'))
                                {
                                    //For Record the Question List Location Start and End
                                    if ((instruction[1].Length > 1) && (question_list_recording == false))
                                    {
                                        question_list_recording = true;
                                        instruction_question_list.Add(question_cnt, new int[2]);
                                        instruction_question_list[question_cnt][0] = instruction_step;
                                    }
                                    else if ((question_list_recording == true) && (instruction[1].Length == 1))
                                    {
                                        question_list_recording = false;
                                        instruction_question_list[question_cnt++][1] = instruction_step - 1;
                                    }

                                    instruction_list[instruction_step].Add(instruction[1]);
                                }
                                else
                                {
                                    MessageBox.Show(txt + "\n" + "Level Error");
                                    return txt + "\n" + "Level Error";
                                }
                                break;
                            case 2: //Command
                                if(!instruction_decode_tool.instruction_check(instruction[2]))
                                {
                                    MessageBox.Show(txt + "\n" + "Command Error");
                                    return txt + "\n" + "Command Error";
                                }
                                instruction_list[instruction_step].Add(instruction[2]);
                                break;
                            case 3: //Data
                                if (instruction[3] == ":")
                                {
                                    string temp = "";   // instruction[cnt + 1];

                                    try
                                    {
                                        while (instruction[++cnt] != ":")
                                        {
                                            if (instruction[cnt] == "/")
                                            {
                                                MessageBox.Show(txt + "\n" + "Data Error");
                                                return txt + "\n" + "Data Error";
                                            }

                                            if (instruction[cnt] != "")
                                                temp += instruction[cnt] + "\t";
                                        }
                                        temp = temp.Remove(temp.Length - 1, 1);
                                        instruction_list[instruction_step].Add(temp);
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(txt + "\n" + "Data Error");
                                        return txt + "\n" + "Data Error";
                                    }

                                }
                                else
                                {
                                    //Trade Data and Error are Empty
                                    instruction_list[instruction_step].Add("");
                                    instruction_list[instruction_step].Add("");
                                    cnt = instruction.Length;   //Stop this line process
                                }
                                break;
                            default: //Error Message/Code
                                if ((instruction[cnt] == "/") && (instruction[cnt-1] == ":"))
                                    instruction_list[instruction_step].Add("");
                                else if ((instruction[cnt] != "/") && (instruction[cnt] != ":"))
                                    instruction_list[instruction_step].Add(instruction[cnt]);
                                break;
                        }
                    }

                }
                else if((txt == "") || private_tool.string_unichar_string_check(txt, '\t') )
                {
                    instruction_step--; //Reverse the Step for next valid instruction
                }
                else
                {
                    MessageBox.Show(txt + "\t" + "Instruction is not complete");
                    return txt + "\t" + "Instruction is not complete";
                }
                

            }

            //instruction_step
            instruction_step_read.Close();
            
            return "Done";
        }
        
    }
}
