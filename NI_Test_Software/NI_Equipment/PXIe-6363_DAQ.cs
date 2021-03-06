﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NationalInstruments.DAQmx;
using NI_Test_Software.Utility;

namespace NI_Test_Software.NI_Equipment.PXIe6363_DAQ
{
    public class PXIe_6363_DAQ
    {
        private List<AnalogSingleChannelWriter> AO_List = new List<AnalogSingleChannelWriter>();
        private Dictionary<string, List<object>> PWM_List = new Dictionary<string,List<object>>();
        
        
        //Pin, Type, Num, Polarity, Range
        private double[] limited = {
                                       -10,
                                       10
                                   };
        private enum limited_range
        {
            min = 0,
            max
        };

        private Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> con0_library = new Dictionary<string,Dictionary<string,Dictionary<string,Dictionary<string,string>>>>();
        private Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> con1_library = new Dictionary<string,Dictionary<string,Dictionary<string,Dictionary<string,string>>>>();
        private General_Tools private_tool =  new General_Tools();
        public static string[] file_pin_type_library = {
                                                          "AI",
                                                          "AO",
                                                          "CTR",
                                                          "IO",
                                                       };
        
        public enum pin_type_lib
        { 
            Analog_in = 0,
            Analog_out,
            Counter,
            IO,
        }

        public static string[] result_Lib = { 
                                      "Connector 0 Library Setup Fail",
                                      "Connector 1 Library Setup Fail",
                                      "Pin Read Error May be no Such a Pin",
                                      "No Pin Config File Found",
                                      "Connector 0 Config File Error",
                                      "Connector 0 Config File Found",
                                      "Connector 1 Config File Error",
                                      "Connector 1 Config File Found",
                                      "All Connector Config File Found",
                                      "Pin Name Wrong",
                                      "PWM Setup Fail",
                                      "PWM Setup Successful",
                                      "PWM Start Fail",
                                      "PWM Start Successful",
                                      "PWM Stop Fail",
                                      "PWM Stop Successful",
                                      "Port Reset Fail",
                                      "Port Reset Success",
                                      "Pin Reset Fail",
                                      "Pin Reset Success",
                                      "Pin Set Fail",
                                      "Pin Set Success",
                                            };
        
        public enum result_Status
        {
            con0_Lib_fail = 0,
            con1_Lib_fail,
            pin_Read_Err,
            no_Pin_Config,
            con0_Config_Found,
            con1_Config_Found,
            con0_Config_Err,
            con1_Config_Err,
            all_Con_found,
            pin_Wrong,
            pwm_setup_fail,
            pwm_setup_success,
            pwm_start_fail,
            pwm_start_success,
            pwm_stop_fail,
            pwm_stop_success,
            port_rst_fail,
            port_rst_success,
            pin_rst_fail,
            pin_rst_success,
            pin_set_fail,
            pin_set_success,
        }

        public static string[] operation_Lib = {
                                                   "Voltage Measure Error",
                                                   "Limited Range Error",
                                                   "Voltage Output Error",
                                                   "Pin Assigment Error",
                                               };

        public enum operation_Status
        {
            v_Measure_Err = 0,
            v_Range_Err,
            v_V_out_Err,
            v_Pin_Err,
        }
        
        public string result = "";

        public enum connector {
                                connector_0 = 0,
                                connector_1 = 1
                              };

        public enum pin_data_type { 
                                    pin = 0, 
                                    type = 1, 
                                    num = 2, 
                                    polarity = 3, 
                                    range = 4
                                  };

        public enum  pwm_package { 
                                    status = 0,
                                    task,
                                    frequency,
                                    duty_cycle,
                                    channel_writer,
                                 }

        public PXIe_6363_DAQ()
        {
            result = result_Lib[(int)result_Status.no_Pin_Config];

            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + app.Default.HardwareCfgFolder))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + app.Default.TestListFolder);
            }

            string[] txt_list = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + app.Default.HardwareCfgFolder, "*.txt", SearchOption.AllDirectories);
            foreach (string msg in txt_list)
            {

                if (msg == System.Windows.Forms.Application.StartupPath + app.Default.HardwareCfgFolder + app.Default.Connector0FileName)
                {
                    foreach (string line in File.ReadLines(System.Windows.Forms.Application.StartupPath + app.Default.HardwareCfgFolder + app.Default.Connector0FileName))
                    {
                        if (line != app.Default.HardwareCfgHeadline)
                        {
                            string[] data = line.Split('\t');
                            if ((data.Length > 5) || ((!private_tool.string_number_check(data[4]) && data[4] != "NA")))
                            {
                                result = result_Lib[(int)result_Status.con0_Config_Err];
                                break;
                            }
                            con0_library.Add(data[0], new Dictionary<string, Dictionary<string, Dictionary<string, string>>>());
                            con0_library[data[0]].Add(data[1], new Dictionary<string, Dictionary<string, string>>());
                            con0_library[data[0]][data[1]].Add(data[2], new Dictionary<string, string>());
                            con0_library[data[0]][data[1]][data[2]].Add(data[3], data[4]);
                        }
                    }

                    //result = "Connector 0 Config File Found";
                    result = result_Lib[(int)result_Status.con0_Config_Found];
                }

                if (msg == System.Windows.Forms.Application.StartupPath + app.Default.HardwareCfgFolder + app.Default.Connector1FileName)
                {
                    foreach (string line in File.ReadLines(System.Windows.Forms.Application.StartupPath + app.Default.HardwareCfgFolder + app.Default.Connector1FileName))
                    {
                        if (line != app.Default.HardwareCfgHeadline)
                        {
                            string[] data = line.Split('\t');
                            if ((data.Length > 5) || (!private_tool.string_number_check(data[4])))
                            {
                                //result = "Connector 1 Config File Error";
                                result = result_Lib[(int)result_Status.con1_Config_Err];
                                break;
                            }
                            con1_library.Add(data[0], new Dictionary<string, Dictionary<string, Dictionary<string, string>>>());
                            con1_library[data[0]].Add(data[1], new Dictionary<string, Dictionary<string, string>>());
                            con1_library[data[0]][data[1]].Add(data[2], new Dictionary<string, string>());
                            con1_library[data[0]][data[1]][data[2]].Add(data[3], data[4]);
                        }
                    }

                    //if (result == "Connector 0 Config File Found")
                    if (result == result_Lib[(int)result_Status.con0_Config_Found])
                        //result = "All Connector Config File Found";
                        result = result_Lib[(int)result_Status.all_Con_found];
                    //else if (result == "Connector 0 Config File Error")
                    else if (result == result_Lib[(int)result_Status.con0_Config_Err])
                        //result = result + ' ' + "Connector 1 Config File Found";
                        result = result + ' ' + result_Lib[(int)result_Status.con1_Config_Found];
                    else
                        //result = "Connector 1 Config File Found";
                        result = result_Lib[(int)result_Status.con1_Config_Found];
                }
            }
        }

        ~PXIe_6363_DAQ()
        {
            DAQ_Reset();
        }

        public static string GetDeviceName(string deviceName)
        {
            Device device = DaqSystem.Local.LoadDevice(deviceName);
            if (device.BusType != DeviceBusType.CompactDaq)
                return deviceName;
            else
                return device.CompactDaqChassisDeviceName;
        }
        
        public void DAQ_Reset()
        {
            try
            {
                foreach (AnalogSingleChannelWriter AO_Pin in AO_List)        //Reset All Analog Output to 0V for Safety
                {
                    AO_Pin.WriteSingleSample(true, 0);
                    AO_List.Remove(AO_Pin);
                }
            }
            catch (Exception ex)
            { 
            }

            try
            {
                foreach (KeyValuePair<string, List<object>> PWM_Task in PWM_List)
                {
                    Task PWM = (Task)PWM_Task.Value[(int)pwm_package.task];

                    if ((bool)PWM_Task.Value[(int)pwm_package.status] == true)
                    {
                        PWM.Stop();
                        CounterSingleChannelWriter channelwriter = (CounterSingleChannelWriter)PWM_Task.Value[(int)pwm_package.channel_writer];
                        channelwriter.WriteSingleSample(true, new CODataFrequency(0, 0));
                    }

                    PWM.Dispose();
                    PWM_List.Remove(PWM_Task.Key);
                }
            }
            catch (Exception ex)
            { 

            }
        }

        public string Test_Point_translation(string test_point)
        {
            string translated_channel = "";

            return translated_channel;
        }

        public string pin_translation(string pin, PhysicalChannelTypes type)
        {
            connector CONNECTOR_PORT = new connector();
            int pin_number = 0;

            string[] Device_List = DaqSystem.Local.GetPhysicalChannels(type, PhysicalChannelAccess.External);       //Read out a list of available 

            string[] pin_detail = pin.Split('$');
            if (pin_detail.Length < 2)
                return result_Lib[(int)result_Status.pin_Wrong];

            try
            {
                CONNECTOR_PORT = (connector)Int32.Parse(Regex.Match(pin_detail[0], @"\d+").Value);
                pin_number = Int32.Parse(Regex.Match(pin_detail[1], @"\d+").Value);
            }
            catch
            {
                return result_Lib[(int)result_Status.pin_Wrong];
            }

            string[] pin_type_list = Pin_Library_Read_Out(CONNECTOR_PORT, pin_number, pin_data_type.type).Split('$');
            string type_chk = "";


            foreach (string pin_lib in file_pin_type_library)
            {
                foreach (string pin_type in pin_type_list)
                {
                    if (pin_type == pin_lib)
                    {
                        type_chk = pin_type.ToLower();
                        break;
                    }
                }
                if (type_chk != "")
                    break;
            }

            if (type_chk == "")
                return result_Lib[(int)result_Status.pin_Wrong];
            
            switch (type)
            {
                case PhysicalChannelTypes.DILine:
                case PhysicalChannelTypes.DOLine:
                    string[] line_detail = Pin_Library_Read_Out(CONNECTOR_PORT, pin_number, pin_data_type.num).Split('.');
                    line_detail[0] = "port" + line_detail[0];
                    line_detail[1] = "line" + line_detail[1];
                    
                    foreach (string device_name in Device_List)
                    {
                        string[] device_spilt = device_name.Split('/');

                        if ((device_spilt[1] == line_detail[0]) && 
                            (device_spilt[2] == line_detail[1]))
                            return device_name;
                    }
                    break;
                case PhysicalChannelTypes.DIPort:
                case PhysicalChannelTypes.DOPort:
                    string[] port_detail = Pin_Library_Read_Out(CONNECTOR_PORT, pin_number, pin_data_type.num).Split('.');
                    port_detail[0] = "port" + port_detail[0];
                    
                    foreach (string device_name in Device_List)
                    {
                        string[] device_spilt = device_name.Split('/');

                        if (device_spilt[1] == port_detail[0])
                            return device_name;
                    }
                    break;
                case PhysicalChannelTypes.AI:
                case PhysicalChannelTypes.AO:
                    double pin_num = Convert.ToDouble(Pin_Library_Read_Out(CONNECTOR_PORT, pin_number, pin_data_type.num));
                    if (CONNECTOR_PORT == connector.connector_1)
                        pin_num += 16;
                    type_chk += pin_num.ToString();

                    foreach (string device_name in Device_List)
                    {
                        string[] device_spilt = device_name.Split('/');

                        if (device_spilt[1] == type_chk)
                            return device_name;
                    }
                    break;
                case PhysicalChannelTypes.CO:
                    foreach (string device_name in Device_List)
                    {
                        string[] device_spilt = device_name.Split('/');
                        if (device_spilt[1] == type_chk.Substring(0, 4))
                            return device_name;
                    }
                    break;

            }

            return result_Lib[(int)result_Status.pin_Wrong];
        }

        public string Pin_Library_Read_Out(connector connector_port, int pin, pin_data_type data)
        {
            string result = "";
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> library_array = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
            
            switch(connector_port)
            {
                case connector.connector_0:
                    try
                    {
                        library_array = con0_library;
                    }
                    catch (Exception e)
                    {
                        //return "Connector 0 Library Setup Fail";
                        return result_Lib[(int)result_Status.con0_Lib_fail];
                    }
                    break;
                case connector.connector_1:
                    try
                    {
                        library_array = con1_library;
                    }
                    catch (Exception e)
                    {
                        //return "Connector 1 Library Setup Fail";
                        return result_Lib[(int)result_Status.con1_Lib_fail];
                    }
                    break;
            }
            
            try
            {
                string[] pin_contain = new string[4];
                library_array[pin.ToString()].Keys.CopyTo(pin_contain, 0);  //Read Out Type
                library_array[pin.ToString()][pin_contain[0]] .Keys.CopyTo(pin_contain, 1); // Read Out Num
                library_array[pin.ToString()][pin_contain[0]][pin_contain[1]].Keys.CopyTo(pin_contain, 2);  //Read Out Polarity
                pin_contain[3] = library_array[pin.ToString()][pin_contain[0]][pin_contain[1]][pin_contain[2]];  //Read Out Range

                result = pin_contain[(int)data - 1];
            }
            catch (Exception e)
            {
                //return "Pin Read Error May be no Such a Pin";
                return result_Lib[(int)result_Status.pin_Read_Err];
            }

            return result;
        }

        
        public string analog_output(string pin, double min, double max, double target)
        {
            string result = "";
            
            string pin_name = pin_translation(pin, PhysicalChannelTypes.AO);

            if (pin_name.Contains(result_Lib[(int)result_Status.pin_Wrong]))
                return operation_Lib[(int)operation_Status.v_Measure_Err];
            try
            {
                using (Task v_out = new Task())
                {
                    v_out.AOChannels.CreateVoltageChannel(pin_name, "", min, max, AOVoltageUnits.Volts);
                    AnalogSingleChannelWriter vout = new AnalogSingleChannelWriter(v_out.Stream);
                    AO_List.Add(vout);      //Store for All Analog Output Reset
                    vout.WriteSingleSample(true, target);
                }
            }
            catch (DaqException exception)
            {
                return operation_Lib[(int)operation_Status.v_V_out_Err] + "\n" + exception.ToString();
            }

            return result;
        }

        public string voltage_measure(string pin, double min, double max)
        {
            if((max > limited[(int)limited_range.max]) || (min < limited[(int)limited_range.min]))
                return operation_Lib[(int)operation_Status.v_Range_Err];

            string result = "";
            
            string pin_name = pin_translation(pin, PhysicalChannelTypes.AI);

            if (pin_name.Contains(result_Lib[(int)result_Status.pin_Wrong]))
                return operation_Lib[(int)operation_Status.v_Pin_Err];

            try
            {
                using (Task Voltage_Measure_Task = new Task())
                {
                    // Create a virtual channel
                    Voltage_Measure_Task.AIChannels.CreateVoltageChannel(pin_name, "", (AITerminalConfiguration)(-1), min, max, AIVoltageUnits.Volts);

                    // Configure the timing parameters
                    Voltage_Measure_Task.Timing.ConfigureSampleClock("", 10000, SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples, 1000);

                    // Verify the Task
                    Voltage_Measure_Task.Control(TaskAction.Verify);

                    //Setup Voltage Reader
                    AnalogSingleChannelReader voltage_reader = new AnalogSingleChannelReader(Voltage_Measure_Task.Stream);

                    double[] read_vol = voltage_reader.ReadMultiSample(100);
                    double sum_vol = 0;
                    foreach (double vol in read_vol)
                    {
                        sum_vol += vol;
                    }

                    sum_vol /= read_vol.Length;

                    result = sum_vol.ToString();
                }
            }
            catch (DaqException exception)
            {
                return operation_Lib[(int)operation_Status.v_Measure_Err] + "\n" + exception.ToString();
            }

            return result;
        }

        public string PWM_Setup(string pin, double frequency, double duty_cycle)
        {
            try
            {
                Task PWM_Task = new Task();
                {
                    string pin_name = pin_translation(pin, PhysicalChannelTypes.CO);

                    if (pin_name.Contains(result_Lib[(int)result_Status.pin_Wrong]))
                        return operation_Lib[(int)operation_Status.v_Pin_Err];

                    PWM_Task.COChannels.CreatePulseChannelFrequency(pin_name,
                            "PWM " + pin.Split('$')[1],
                            COPulseFrequencyUnits.Hertz,
                            COPulseIdleState.Low,
                            0.0,
                            frequency,
                            duty_cycle);

                    PWM_Task.Timing.ConfigureSampleClock("/" + GetDeviceName(pin_name.Split('/')[0]) + "/" + "ai/SampleClock",
                        1000,
                        SampleClockActiveEdge.Rising,
                        SampleQuantityMode.HardwareTimedSinglePoint);

                    PWM_List.Add(pin, new List<object>());
                    PWM_List[pin].Add((object)false);

                    PWM_List[pin].Add((object)PWM_Task);

                    PWM_List[pin].Add((object)frequency);
                    PWM_List[pin].Add((object)duty_cycle);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return result_Lib[(int)result_Status.pwm_setup_fail];
            }

            return result_Lib[(int)result_Status.pwm_setup_success];
        }

        public string PWM_start(string pin)
        {
            try
            {
                bool PWM_Task_Status = (bool)PWM_List[pin][(int)pwm_package.status]; 
                Task PWM_Task = (Task)PWM_List[pin][(int)pwm_package.task];

                if (PWM_Task_Status == false)
                {
                    PWM_Task.Start();
                    PWM_List[pin].Add((object)(new CounterSingleChannelWriter(PWM_Task.Stream)));
                    PWM_List[pin][(int)pwm_package.status] = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
                return result_Lib[(int)result_Status.pwm_start_fail];
            }
            return result_Lib[(int)result_Status.pwm_start_success];
        }

        public string PWM_stop(string pin)
        {
            try
            {
                bool PWM_Task_Status = (bool)PWM_List[pin][(int)pwm_package.status];
                Task PWM_Task = (Task)PWM_List[pin][(int)pwm_package.task];

                if (PWM_Task_Status == true)
                {
                    PWM_Task.Stop();
                    PWM_List[pin].Remove(PWM_List[pin][(int)pwm_package.channel_writer]);
                    PWM_List[pin][(int)pwm_package.status] = false;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
                return result_Lib[(int)result_Status.pwm_stop_fail];
            }
            return result_Lib[(int)result_Status.pwm_stop_success];
        }

        public string Pin_Reset(string pin)
        {
            connector port = new connector();
            int pin_number = 0;

            string[] pin_detail = pin.Split('$');
            if (pin_detail.Length < 2)
                return result_Lib[(int)result_Status.pin_Wrong];
            try
            {
                port = (connector)Int32.Parse(Regex.Match(pin_detail[0], @"\d+").Value);
                pin_number = Int32.Parse(Regex.Match(pin_detail[1], @"\d+").Value);
            }
            catch
            {
                return result_Lib[(int)result_Status.pin_Wrong];
            }

            string[] pin_type_list = Pin_Library_Read_Out(port, pin_number, pin_data_type.type).Split('$');
            
            try
            {
                if (pin_type_list[0] == file_pin_type_library[(int)pin_type_lib.Analog_in])
                {
                    return result_Lib[(int)result_Status.pin_rst_success];
                }
                else if (pin_type_list[0] == file_pin_type_library[(int)pin_type_lib.Analog_out])
                {
                    using (Task v_out = new Task())
                    {
                        v_out.AOChannels.CreateVoltageChannel(pin_translation(pin, PhysicalChannelTypes.AO), "", 0, 1, AOVoltageUnits.Volts);
                        AnalogSingleChannelWriter vout = new AnalogSingleChannelWriter(v_out.Stream);
                        vout.WriteSingleSample(true, 0);
                    }
                }
                else if ((pin_type_list[0] == file_pin_type_library[(int)pin_type_lib.IO]) ||(pin_type_list[0] == file_pin_type_library[(int)pin_type_lib.Counter]))
                {
                    if (pin_type_list[0] == file_pin_type_library[(int)pin_type_lib.Counter])
                        PWM_stop(pin);

                    using (Task IO_port = new Task())
                    {
                        IO_port.DOChannels.CreateChannel(pin_translation(pin, PhysicalChannelTypes.DOLine), "", ChannelLineGrouping.OneChannelForAllLines);
                        DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(IO_port.Stream);
                        writer.WriteSingleSampleSingleLine(true, false);

                        IO_port.DOChannels.CreateChannel(pin_translation(pin, PhysicalChannelTypes.DILine), "", ChannelLineGrouping.OneChannelForAllLines);
                        DigitalSingleChannelReader reader = new DigitalSingleChannelReader(IO_port.Stream);
                        UInt32 dummy = reader.ReadSingleSamplePortUInt32();
                    }
                }
            }
            catch (Exception ex)
            {
                return result_Lib[(int)result_Status.pin_rst_fail];
            }

                return "";
        }

        public void IO_pin_set(string pin, bool state)
        {
            try
            {
                string pin_hw_name = pin_translation(pin, PhysicalChannelTypes.DOLine);
                using (Task IO_Port = new Task())
                {
                    IO_Port.DOChannels.CreateChannel(pin_hw_name, "",
                        ChannelLineGrouping.OneChannelForEachLine);
                    DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(IO_Port.Stream);
                    writer.WriteSingleSampleSingleLine(true, state);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IO_pin_Read(string pin)
        {
            try
            {
                string pin_hw_name = pin_translation(pin, PhysicalChannelTypes.DOLine);
                using (Task IO_port = new Task())
                {
                    IO_port.DOChannels.CreateChannel(pin_hw_name, "",
                            ChannelLineGrouping.OneChannelForEachLine);

                    DigitalSingleChannelReader reader = new DigitalSingleChannelReader(IO_port.Stream);
                    bool result = reader.ReadSingleSampleSingleLine();
                    for (int cnt = 0; cnt < 10; cnt++)
                    {
                        result = reader.ReadSingleSampleSingleLine();
                    }

                    return result;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IO_Pin_Reset()
        {
            try
            {
                string[] port_list = DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.DOPort, PhysicalChannelAccess.External);

                //Set All pin into GND Status
                foreach (string port in port_list)
                {
                    using (Task IO_port = new Task())
                    {
                        IO_port.DOChannels.CreateChannel(port, "", ChannelLineGrouping.OneChannelForAllLines);
                        DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(IO_port.Stream);
                        writer.WriteSingleSamplePort(true, 0);
                        
                    }
                }
                
                port_list = DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.DIPort, PhysicalChannelAccess.External);
                foreach (string port in port_list)
                {
                    using (Task IO_port = new Task())
                    {
                        DigitalSingleChannelReader reader = new DigitalSingleChannelReader(IO_port.Stream);
                        UInt32 dummy = reader.ReadSingleSamplePortUInt32();
                    }
                }
            }
            catch (Exception ex)
            {
                return result_Lib[(int)result_Status.port_rst_fail];
            }

            return result_Lib[(int)result_Status.port_rst_success];
        }
    }
}
