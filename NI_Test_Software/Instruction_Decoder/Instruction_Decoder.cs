using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NationalInstruments;
using NationalInstruments.DAQmx;
using NationalInstruments.ModularInstruments.NIDCPower;
using NI_Test_Software.NI_Equipment.PXIe6363_DAQ;
using NI_Test_Software.NI_Equipment.PXI4110_DCPower;
using NI_Test_Software.NI_Equipment.PXI2527_Switch;
using NI_Test_Software.Utility;
using NI_Test_Software.Instruction_Operation;

namespace NI_Test_Software.Instruction_Operation
{
    public class Instruction_Decoder
    {
        //Hardware Session
        PXI4110_DCPower PowerSession = new PXI4110_DCPower();
        PXIe_6363_DAQ DAQSession = new PXIe_6363_DAQ();
        PXI2527_Switch SwitchSession = new PXI2527_Switch();

        /********************/

        private Dictionary<string, Func<object[], string>> instruction_decoding = new Dictionary<string, Func<object[], string>>();
        private General_Tools Instruction_operation_tool = new General_Tools();
        private string[] result_list = Instruction_Executor.executation_result;
        private List<string> recorded_result = new List<string>();
        private string fifo_result = "";

        public enum parameter_pos
        {
            Created_List = 0,
            GeneralDataSet,
            First,
            Second,
            Third,
            Fourth,
            Fifth,
            Sixth,
            Seventh,
        }

        public enum General_Data
        {
            Executor = 0,
            Template,
            Error_Message,
            System_Flag,
            End,
            Total_number,
        }

        public Dictionary<string, Func<object[], string>> read_instruction_libarary
        {
            get { return instruction_decoding;  }
        }

        public bool instruction_check(string msg)
        {
            bool result = false;
            
            try
            {
                if (instruction_decoding[msg] != null)
                    result = true;
            }
            catch(Exception e)
            {
                result = false;
            }

            return result;
        }


        public Instruction_Decoder()
        {
            try
            {
                instruction_decoding.Add("Measure Voltage", new Func<object[], string>(Test_Measure_Voltage));
                instruction_decoding.Add("Measure Current", new Func<object[], string>(Test_Measure_Current));
                instruction_decoding.Add("Measure State", new Func<object[], string>(Test_Measure_State));
                instruction_decoding.Add("Route", new Func<object[], string>(Test_Route));
                instruction_decoding.Add("Route Reset", new Func<object[], string>(Test_Route_Reset));
                instruction_decoding.Add("MUX Reset", new Func<object[], string>(Test_MUX_Reset));
                instruction_decoding.Add("DAQ Reset", new Func<object[], string>(Test_DAQ_Reset));
                instruction_decoding.Add("Set DIO", new Func<object[], string>(Test_Set_DIO_Reset));
                instruction_decoding.Add("Set Power", new Func<object[], string>(Test_Set_Power));
                instruction_decoding.Add("Power ON", new Func<object[], string>(Test_Power_ON));
                instruction_decoding.Add("Power OFF", new Func<object[], string>(Test_Power_OFF));
                instruction_decoding.Add("Delay", new Func<object[], string>(Test_Delay));
                instruction_decoding.Add("Set PWM", new Func<object[], string>(Test_Set_PWM));
                instruction_decoding.Add("Set ID Check", new Func<object[], string>(Test_Set_ID_Check));
                instruction_decoding.Add("Base Program", new Func<object[], string>(Test_Base_Program));
                instruction_decoding.Add("Base Produtcion Program", new Func<object[], string>(Test_Base_Production_Program));
                instruction_decoding.Add("Base Erase", new Func<object[], string>(Test_Base_Erase));
                instruction_decoding.Add("CC2510 Program", new Func<object[], string>(Test_CC2510_Program));
                instruction_decoding.Add("CC2510 Proudction Program", new Func<object[], string>(Test_CC2510_Production_Program));
                instruction_decoding.Add("CC2510 Erase", new Func<object[], string>(Test_CC2510_Erase));
                instruction_decoding.Add("Pop Message", new Func<object[], string>(Test_Message_Popup));
                instruction_decoding.Add("Record Result", new Func<object[], string>(Test_Record_Result));
                instruction_decoding.Add("Save Result", new Func<object[], string>(Test_Save_Result));
                instruction_decoding.Add("End", new Func<object[], string>(Test_End));
                instruction_decoding.Add("Jump", new Func<object[], string>(Test_Jump));
                instruction_decoding.Add("CC2510 Self Test Config", new Func<object[], string>(Test_CC2510_Self_Test_Config));
                instruction_decoding.Add("CC2510 Self Test Req", new Func<object[], string>(Test_CC2510_Self_Test_Req));
                instruction_decoding.Add("Label Print", new Func<object[], string>(Test_Label_Print));
                instruction_decoding.Add("Set Voltage Out", new Func<object[], string>(Test_Set_Voltage_Out));
                instruction_decoding.Add("PWM Start", new Func<object[], string>(Test_PWM_Start));
                instruction_decoding.Add("PWM Stop", new Func<object[], string>(Test_PWM_Stop));
                //Question Sub Set
                instruction_decoding.Add("Question", new Func<object[], string>(Test_Question));
                instruction_decoding.Add("Question End", new Func<object[], string>(Test_Question_End));
                instruction_decoding.Add("Message Left", new Func<object[], string>(Sub_Test_Message_Left));
                instruction_decoding.Add("Message Right", new Func<object[], string>(Sub_Test_Message_Right));
                instruction_decoding.Add("Set Button Left", new Func<object[], string>(Sub_Test_Sub_Set_Button_Left));
                instruction_decoding.Add("Set Button Right", new Func<object[], string>(Sub_Test_Sub_Set_Button_Right));
                instruction_decoding.Add("Set Button Left React", new Func<object[], string>(Sub_Test_Sub_Set_Button_Left_React));
                instruction_decoding.Add("Set Button Right React", new Func<object[], string>(Sub_Test_Sub_Set_Button_Right_React));
                instruction_decoding.Add("Set Test Image", new Func<object[], string>(Sub_Test_Set_Image));
                instruction_decoding.Add("Set Exit Button", new Func<object[], string>(Sub_Test_Set_Exit_Button));
            }
            catch (Exception e)
            {
                MessageBox.Show("Dictionary Add Fail\n" + e);
            }
        }

        private int Task_cnt = 0;
        

        /*
         * arg[0] = Instruction_List_Creator Created_List
         * arg[1] = Test_Template_Form Template and Executor itself it is an object[]
            ** object[0] is executor
            ** object[1] is Template it will be null sometime so make sure the usage of the command
         */

        //Instruction "Measure Voltage" 
        /*
         * arg[(int)parameter_pos.First  ] = pin
         * arg[(int)parameter_pos.Second ] = min range
         * arg[(int)parameter_pos.Third  ] = max range
         */
        private string Test_Measure_Voltage(params object[] arg)
        {
            try
            {
                string pin = (string)arg[(int)parameter_pos.First];
                string min = (string)arg[(int)parameter_pos.Second];
                string max = (string)arg[(int)parameter_pos.Third];

                fifo_result = DAQSession.voltage_measure(pin, -10, 10);

                if (!fifo_result.Contains(PXIe_6363_DAQ.operation_Lib[(int)PXIe_6363_DAQ.operation_Status.v_Measure_Err]))
                {
                    if(Convert.ToDouble(fifo_result) < Convert.ToDouble(min))
                    {
                        return result_list[(int)Instruction_Executor.exe_result.Measure_Err] + "\nVoltage " + "Below the Minimum Limit";
                    }
                    if(Convert.ToDouble(fifo_result) > Convert.ToDouble(max))
                    {
                        return result_list[(int)Instruction_Executor.exe_result.Measure_Err] + "\nVoltage " + "Above the Maximum Limit";
                    }
                }
                else if (!fifo_result.Contains(PXIe_6363_DAQ.operation_Lib[(int)PXIe_6363_DAQ.operation_Status.v_Pin_Err]))
                {
                    return result_list[(int)Instruction_Executor.exe_result.Pin_Err];
                }
         
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
            
        }

        //Instruction "Measure Current" 
        /*
         * arg[(int)parameter_pos.First   ] = channel
         * arg[(int)parameter_pos.Second  ] = min range
         * arg[(int)parameter_pos.Third   ] = max range
         */
        private string Test_Measure_Current(params object[] arg)
        {
            try
            {
                string channel = (string)arg[(int)parameter_pos.First   ];
                string min = (string)arg[(int)parameter_pos.Second  ];
                string max = (string)arg[(int)parameter_pos.Third   ];

                DCPowerMeasureResult result = PowerSession.NIDC_Measurement_Result(channel);
                double sum = 0;
                foreach (double num in result.CurrentMeasurements)
                {
                    sum += num;
                }

                sum /= result.CurrentMeasurements.Length;

                fifo_result = sum.ToString();

                if ((sum < Convert.ToDouble(min)) || (sum > Convert.ToDouble(max)))
                {
                    return result_list[(int)Instruction_Executor.exe_result.Out_Range_Err];
                }
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Measure State" 
        /*
         * arg[(int)parameter_pos.First   ] = pin
         * arg[(int)parameter_pos.Second  ] = state
         */
        private string Test_Measure_State(params object[] arg)
        {
            //string pin, string state
            try
            {
                string pin = (string)arg[(int)parameter_pos.First];
                string state = (string)arg[(int)parameter_pos.Second];
             
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Route"
        /*
         * src = arg[(int)parameter_pos.First   ]
         * dst = arg[(int)parameter_pos.Second  ]
         */
        private string Test_Route(params object[] arg)
        {
            try
            {
                string src = (string)arg[(int)parameter_pos.First];
                string dst = (string)arg[(int)parameter_pos.Second];

                SwitchSession.path(src, dst, true);
                
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Route Reset"
        private string Test_Route_Reset(params object[] arg)
        {
            try
            {
                SwitchSession.path_reset();
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "MUX Reset"
        private string Test_MUX_Reset(params object[] arg)
        {
            try
            {
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }
        //Instruction "DAQ Reset"
        private string Test_DAQ_Reset(params object[] arg)
        {
            try
            {
                DAQSession.DAQ_Reset();
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Set DIO"
        /*
         * pin  = arg[(int)parameter_pos.First   ]
         * IO   = arg[(int)parameter_pos.Second  ]
         * state= arg[(int)parameter_pos.Third   ]
         */
        private string Test_Set_DIO_Reset(params object[] arg)
        {
            //pin, IO, state
            try
            {
                string pin = (string)arg[(int)parameter_pos.First];
                string IO = (string)arg[(int)parameter_pos.Second];
                string state = (string)arg[(int)parameter_pos.Third];

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Set Power Voltage
        /*
         * Channel = arg[(int)parameter_pos.First   ]
         * Voltage = arg[(int)parameter_pos.Second  ]
         * current = arg[(int)parameter_pos.Third   ]
         */
        private string Test_Set_Power(params object[] arg)
        {
            try
            {
                string Channel = (string)arg[(int)parameter_pos.First];
                string Voltage = (string)arg[(int)parameter_pos.Second];
                string Current = (string)arg[(int)parameter_pos.Third];

                PowerSession.NIDC_setup(Channel, Convert.ToDouble(Voltage), Convert.ToDouble(Current+"e-3"));

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Power ON"
        /*
         * Channel = arg[(int)parameter_pos.First]
         */
        private string Test_Power_ON(params object[] arg)
        {
            try
            {
                string Channel = (string)arg[(int)parameter_pos.First];

                PowerSession.NIDC_Power_ON(Channel);

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Power OFF"
        /*
         * Channel = arg[(int)parameter_pos.First]
         */
        private string Test_Power_OFF(params object[] arg)
        {
            try
            {
                string Channel = (string)arg[(int)parameter_pos.First];

                PowerSession.NIDC_Power_OFF(Channel);

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Delay"
        /*
         * delay_ms = arg[(int)parameter_pos.First]
         */
        private string Test_Delay(params object[] arg)
        {
            try
            {
                string delay_ms = (string)arg[(int)parameter_pos.First];
                Thread.Sleep(Convert.ToInt32(delay_ms));
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Set PWM"
        /*
         * Frequency = arg[(int)parameter_pos.First]
         * Duty_Cycle = arg[(int)parameter_pos.Second]
         */
        private string Test_Set_PWM(params object[] arg)
        {
            try
            {
                string pin = (string)arg[(int)parameter_pos.First];
                double Frequency = Convert.ToDouble((string)arg[(int)parameter_pos.Second]);
                double Duty_Cycle = Convert.ToDouble((string)arg[(int)parameter_pos.Third]);

                if(Duty_Cycle > 100)
                    return result_list[(int)Instruction_Executor.exe_result.Fail];

                DAQSession.PWM_Setup(pin, Frequency, Duty_Cycle);

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Set ID Check"
        /*
         *  = arg[(int)parameter_pos.First]
         *  = arg[(int)parameter_pos.Second]
         */
        private string Test_Set_ID_Check(params object[] arg)
        {
            try
            {
                //string Frequency = (string)arg[(int)parameter_pos.First];
                //string Duty_Cycle = (string)arg[(int)parameter_pos.Second];

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Base Program"
        /*
         * firmware_path = arg[(int)parameter_pos.First]
         */
        private string Test_Base_Program(params object[] arg)
        {
            try
            {
                string firmware_path = (string)arg[(int)parameter_pos.First];
                
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Base Production Program"
        /*
         * firmware_folder = arg[(int)parameter_pos.First]
         */
        private string Test_Base_Production_Program(params object[] arg)
        {
            try
            {
                string firmware_folder = (string)arg[(int)parameter_pos.First];

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Base Erase"
        private string Test_Base_Erase(params object[] arg)
        {
            try
            {
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }
        
        //Instruction "CC2510 Program"
        /*
         * firmware_path = arg[(int)parameter_pos.First];
         */
        private string Test_CC2510_Program(params object[] arg)
        {
            try
            {
                string firmware_path = (string)arg[(int)parameter_pos.First];

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }

        }

        //Instruction "CC2510 Production Program"
        /*
         * firmware_folder = arg[(int)parameter_pos.First];
         */
        private string Test_CC2510_Production_Program(params object[] arg)
        {
            try
            {
                string firmware_folder = (string)arg[(int)parameter_pos.First];

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }

        }
        
        //Instruction "CC2510 Erase"
        private string Test_CC2510_Erase(params object[] arg)
        {
            try
            {
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }
        
        //Instruction "Pop Message"
        /*
         * message = arg[(int)parameter_pos.First];
         */
        private string Test_Message_Popup(params object[] arg)
        {
            try
            {
                MessageBox.Show((string)arg[(int)parameter_pos.First]);
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "Record Result"
        /*
         * prefix = arg[(int)parameter_pos.First    ]
         * subfix = arg[(int)parameter_pos.Second   ]
         */
        private string Test_Record_Result(params object[] arg)
        {
            try
            {
                string prefix = (string)arg[(int)parameter_pos.First    ];
                string subfix = (string)arg[(int)parameter_pos.Second   ];
                recorded_result.Add(prefix + fifo_result + subfix);
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }

        }

        //Instruction "Save Result"
        private string Test_Save_Result(params object[] arg)
        {
            try
            {
                
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Flag_Control Sys_Flag = (Flag_Control)GeneralData[(int)General_Data.System_Flag];
                
                //Setup the Result Path
                string temp = Sys_Flag.mode.Replace(app.Default.TestListFolder, app.Default.TestResultFolder);
                
                //Extract the Result
                object[] result_data = Instruction_operation_tool.result_file_extractor(temp);

                //Ready the file to be written
                StreamWriter result_file = new StreamWriter(temp, true);

                //Setup the next number
                temp = ((Int64)result_data[(int)General_Tools.result_file.Complete_Product] + 1).ToString() + "\t";

                foreach (string result in recorded_result)
                {
                    temp += result + '\t';
                }

                //Write in the next line
                result_file.WriteLine(temp);

                result_file.Close();
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }

        }

        //Instruction "End"
        /*
         * executor = (Instruction_Executor)arg[(int)parameter_pos.Current_Template];
         */
        private string Test_End(params object[] arg)
        {
            object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];

            Instruction_Executor executor = (Instruction_Executor)GeneralData[(int)General_Data.Executor];
            
            executor.instruction_step = 0;
            executor.question_step = 0;
            //executor.Question_Status = false;

            DAQSession.DAQ_Reset();
            SwitchSession.path_reset();

            recorded_result.Clear();
            fifo_result = "";

            return result_list[(int)Instruction_Executor.exe_result.Stop];
        }

        //Instruction "Jump"
        private string Test_Jump(params object[] arg)
        {
            string step = (string)arg[(int)parameter_pos.First];
            
            Instruction_List_Creator create_instruction_list = (Instruction_List_Creator) arg[(int)parameter_pos.Created_List];

            Dictionary<int, List<string>> instruction_list = create_instruction_list.Read_Instruction_List;

            object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
            Instruction_Executor exec_list = (Instruction_Executor)GeneralData[(int)General_Data.Executor];

            foreach (KeyValuePair<int, List<string>> item in instruction_list)
            { 
                string item_step = item.Value[(int)Instruction_Executor.instruction_structure.step];
                string arg_step = (string) arg[(int)parameter_pos.First];
                if(item_step == arg_step)
                {
                    item_step = item.Value[(int)Instruction_Executor.instruction_structure.level];
                    arg_step = (string) arg[(int)parameter_pos.Second];
                    if (item_step == arg_step)
                    {
                        exec_list.instruction_step = item.Key;
                        return result_list[(int)Instruction_Executor.exe_result.Pass];        
                    }

                }
            }

            return result_list[(int)Instruction_Executor.exe_result.Fail] + " No Such Step for Jump " + (string)arg[(int)parameter_pos.First] + (string)arg[(int)parameter_pos.Second];
            
        }
        //Instruction "CC2510 Selft Test Config"
        private string Test_CC2510_Self_Test_Config(params object[] arg)
        {
            return result_list[(int)Instruction_Executor.exe_result.Fail];
        }

        //Instruction "CC2510 Selft Test Req"
        private string Test_CC2510_Self_Test_Req(params object[] arg)
        {
            return result_list[(int)Instruction_Executor.exe_result.Fail];
        }

        //Instruction "Label Print"
        private string Test_Label_Print(params object[] arg)
        {
            return result_list[(int)Instruction_Executor.exe_result.Fail];
        }

        //Instruction "Label Print"
        private string Test_Set_Voltage_Out(params object[] arg)
        {
            string pin = (string)arg[(int)parameter_pos.First];
            string min = (string)arg[(int)parameter_pos.Second];
            string max = (string)arg[(int)parameter_pos.Third];

            string target = (string)arg[(int)parameter_pos.Fourth];

            try
            {
                PXIe_6363_DAQ NI_DAQ = new PXIe_6363_DAQ();

                if (!NI_DAQ.pin_output(pin, Convert.ToDouble(min), Convert.ToDouble(max), Convert.ToDouble(target)).Contains(PXIe_6363_DAQ.operation_Lib[(int)PXIe_6363_DAQ.operation_Status.v_Pin_Err]))
                {
                    return result_list[(int)Instruction_Executor.exe_result.Pin_Err];
                }

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        //Instruction "PWM Start"
        private string Test_PWM_Start(params object[] arg) 
        {
            try
            {
                string pin = (string)arg[(int)parameter_pos.First];

                if (DAQSession.PWM_start(pin).Contains("Fail"))
                    return result_list[(int)Instruction_Executor.exe_result.Fail];

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception ex)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }

            return result_list[(int)Instruction_Executor.exe_result.Fail];
        }
        
        //Instruction "PWM Stop"
        private string Test_PWM_Stop(params object[] arg)
        {
            try
            {
                string pin = (string)arg[(int)parameter_pos.First];

                if (DAQSession.PWM_stop(pin).Contains("Fail"))
                    return result_list[(int)Instruction_Executor.exe_result.Fail];

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception ex)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }

            return result_list[(int)Instruction_Executor.exe_result.Fail];
        }    

        //Instruction "Question" 
        //For use under the Template of Question only
        private string Test_Question(params object[] arg)
        {
            object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
            
            Flag_Control system_flag = (Flag_Control)GeneralData[(int)General_Data.System_Flag];
            Instruction_Executor executor = (Instruction_Executor)GeneralData[(int)General_Data.Executor];
            Instruction_List_Creator current_exe_list = (Instruction_List_Creator)arg[(int)parameter_pos.Created_List];
            
            foreach(KeyValuePair<int, int[]> item in current_exe_list.instruction_question_list)
            {
                if (item.Value[(int)Instruction_Executor.question_loc.start] == executor.instruction_step + 1)
                {
                    executor.question_step = item.Key;
                    executor.instruction_step = item.Value[(int)Instruction_Executor.question_loc.end] + 1;
                    system_flag.Test_status = (int)Flag_Control.Test_status_Lib.pause;  //Pause the Main Instruction Running Thread
                    return result_list[(int)Instruction_Executor.exe_result.Q_Start];
                }
            }
            
            return result_list[(int)Instruction_Executor.exe_result.Exe_Err];
        }
        /*
         * Sub Instruction Question_End
         * arg[0] for Test-Template_Form
         */
        private string Test_Question_End(params object[] arg)
        {
            return result_list[(int)Instruction_Executor.exe_result.Q_Complete];
        }

        /*
         * Sub Instruction Message Left
         * arg[(int)parameter_pos.Template] for Test-Template_Form
         * arg[(int)parameter_pos.First] for msg
         */
        private string Sub_Test_Message_Left(params object[] arg)
        {
            try
            {
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];
                
                string msg = (string)arg[(int)parameter_pos.First];

                Template.p_Left_Panel_txt.Text = msg;
                if (!Template.p_Instruction_and_Result.Panel1.Created)
                    Template.p_Instruction_and_Result.Panel1.Controls.Add(Template.p_Left_Panel_txt);

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        private string Sub_Test_Message_Right(params object[] arg)
        {
            try
            {
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];
                
                string msg = (string)arg[(int)parameter_pos.First];

                Template.p_Right_Panel_txt.Text = msg;
                if (!Template.p_Instruction_and_Result.Panel2.Created)
                    Template.p_Instruction_and_Result.Panel2.Controls.Add(Template.p_Right_Panel_txt);

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch(Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        private string Sub_Test_Sub_Set_Button_Left(params object[] arg)
        {
            try
            {
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];
                
                string msg = (string)arg[(int)parameter_pos.First];
                
                Template.p_Button_Left.Text = msg;
                if (!Template.p_Button_Left.Created)
                    Template.Controls.Add(Template.p_Button_Left);

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        private string Sub_Test_Sub_Set_Button_Right(params object[] arg)
        {
            try
            {
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];
                
                string msg = (string)arg[(int)parameter_pos.First];

                Template.p_Button_Right.Text = msg;
                if (!Template.p_Button_Right.Created)
                    Template.Controls.Add(Template.p_Button_Right);
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        private string Sub_Test_Sub_Set_Button_Left_React(params object[] arg)
        {
            try
            {
                //instruction_decoding
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Instruction_Executor Executor = (Instruction_Executor)GeneralData[(int)General_Data.Executor];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];

                Template.p_Left_Button_Action.Clear();

                int original_level = Template.p_Question_Instruction_List[Executor.question_list_step][(int)Instruction_Executor.instruction_structure.level].Length;
                Executor.mask_cnt = 0;

                foreach (KeyValuePair<int, List<string>> item in Template.p_Question_Instruction_List) 
                {
                    if (item.Key > Executor.question_list_step)
                    {
                        if (Template.p_Question_Instruction_List[item.Key][(int)Instruction_Executor.instruction_structure.level].Length <= original_level)
                        {
                            break;
                        }
                        Template.p_Left_Button_Action.Add(item.Key, Template.p_Question_Instruction_List[item.Key]);
                        Executor.mask_cnt++;
                    }
                }


                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        private string Sub_Test_Sub_Set_Button_Right_React(params object[] arg)
        {
            try
            {
                //instruction_decoding
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Instruction_Executor Executor = (Instruction_Executor)GeneralData[(int)General_Data.Executor];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];

                Template.p_Right_Button_Action.Clear();

                int original_level = Template.p_Question_Instruction_List[Executor.question_list_step][(int)Instruction_Executor.instruction_structure.level].Length;
                Executor.mask_cnt = 0;

                foreach (KeyValuePair<int, List<string>> item in Template.p_Question_Instruction_List)
                {
                    if (item.Key > Executor.question_list_step)
                    {
                        if (Template.p_Question_Instruction_List[item.Key][(int)Instruction_Executor.instruction_structure.level].Length <= original_level)
                        {
                            break;
                        }
                        Template.p_Right_Button_Action.Add(item.Key, Template.p_Question_Instruction_List[item.Key]);
                        Executor.mask_cnt++;
                    }
                }

                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        private string Sub_Test_Set_Image(params object[] arg)
        {
            try
            {
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];
                
                string path = (string)arg[(int)parameter_pos.First];

                string complete_path;
                if (path.StartsWith("\root"))
                    path.Replace("\root", System.Windows.Forms.Application.StartupPath);
                else if (path.StartsWith("root"))
                    path.Replace("root", System.Windows.Forms.Application.StartupPath);

                Template.p_Test_Image.ImageLocation = path;
                return result_list[(int)Instruction_Executor.exe_result.Pass];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

        private string Sub_Test_Set_Exit_Button(params object[] arg)
        {
            try
            {
                object[] GeneralData = (object[])arg[(int)parameter_pos.GeneralDataSet];
                Test_Template_Form Template = (Test_Template_Form)GeneralData[(int)General_Data.Template];

                if (!Template.p_Button_Exit.Created)
                    Template.Controls.Add(Template.p_Button_Exit);

                return result_list[(int)Instruction_Executor.exe_result.Reset];
            }
            catch (Exception e)
            {
                return result_list[(int)Instruction_Executor.exe_result.Fail];
            }
        }

    }
}
