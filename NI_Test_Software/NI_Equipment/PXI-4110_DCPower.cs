using Ivi.DCPwr;
using Ivi.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using NationalInstruments.ModularInstruments.NIDCPower;
using NationalInstruments.ModularInstruments.SystemServices.DeviceServices;

namespace NI_Test_Software.NI_Equipment.PXI4110_DCPower
{
    class PXI4110_DCPower
    {
        int step_count = 0;
        public string  NIDC_Resource;
        static string[] NIDC_Err_Message = {
                                                "DC Power Self Test Fail",
                                                "DC Power Self Test Pass",
                                           };
        public enum err_msg
        { 
            Sel_Test_Fail = 0,
            Sel_Test_Pass,
        }

        static string[]  NIDC_Channel = {
                                            "0",    //6V Channel
                                            "1",    //+20V Channel
                                            "2",    //-20V Channel
                                        };
        public enum channel
        { 
            P06V = 0,
            P20V,
            M20V,
        }

        static decimal NIDC_Source_Delay = 2;
        
        public List<NIDCPower> Power_Control_Session = new List<NIDCPower>();
        

        public PXI4110_DCPower()
        {
            using (ModularInstrumentsSystem dcPowerDevices = new ModularInstrumentsSystem("NI-DCPower"))
            {
                foreach (DeviceInfo device in dcPowerDevices.DeviceCollection)
                {
                    NIDC_Resource = device.Name;
                }
            }

            Power_Control_Session.Add(new NIDCPower(NIDC_Resource, "0", true));
            Power_Control_Session.Add(new NIDCPower(NIDC_Resource, "1", true));
            Power_Control_Session.Add(new NIDCPower(NIDC_Resource, "2", true));
            
            for(int Channel_Name = 0; Channel_Name < 3; Channel_Name++)
            {
                Power_Control_Session[Channel_Name].Source.Mode = DCPowerSourceMode.SinglePoint;
                Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Source.Output.Function = DCPowerSourceOutputFunction.DCCurrent;
                Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Measurement.Sense = DCPowerMeasurementSense.Local;
                if(Channel_Name >= 2)
                    Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Source.Current.CurrentLevel = -0.02;
                else
                    Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Source.Current.CurrentLevel = 0.02;
                Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Source.Current.CurrentLevelAutorange = DCPowerSourceCurrentLevelAutorange.On;
                Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Source.Current.VoltageLimit = 0.02;
                Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Source.Current.VoltageLimitAutorange = DCPowerSourceVoltageLimitAutorange.On;
                Power_Control_Session[Channel_Name].Outputs[Channel_Name.ToString()].Source.SourceDelay = new NationalInstruments.PrecisionTimeSpan(0.05);
                Power_Control_Session[Channel_Name].Control.Initiate();
                Power_Control_Session[Channel_Name].Events.SourceCompleteEvent.WaitForEvent(new NationalInstruments.PrecisionTimeSpan(0.05));
                //Power_Control_Session[Channel_Name].DriverOperation.Warning += new EventHandler<Ivi.Driver.WarningEventArgs>( "Empty" );  //Setup an Event Handler for any DC Power Error
            }
            
        }

        ~PXI4110_DCPower()
        {
            string temp = "";
            Power_Control_Session[0].Close();   //Turn it Off when Session is closed
            Power_Control_Session[1].Close();   //
            Power_Control_Session[2].Close();   //
        }

        public string NIDC_Selftest()
        {
            SelfTestResult result;

            foreach (string Channel_Name in NIDC_Channel)
            {
                using (NIDCPower SelftestSession = new NIDCPower(NIDC_Resource, Channel_Name, true))
                {
                    try
                    {
                        SelftestSession.Utility.ResetWithDefaults();
                        result = SelftestSession.Utility.SelfTest();

                        if (SelftestSession.Utility.SelfTest().Code != 0)
                        {
                            MessageBox.Show(result.Message);
                            return Channel_Name + " " + NIDC_Err_Message[(int)err_msg.Sel_Test_Fail] ;
                        }
                        SelftestSession.Close();
                    }
                    catch (Exception e)
                    {
                        return Channel_Name + " " + NIDC_Err_Message[(int)err_msg.Sel_Test_Fail];
                    }
                }
            }

            return NIDC_Err_Message[(int)err_msg.Sel_Test_Pass];
        
        }

        public void NIDC_setup(string Channel_Name, double NIDC_Voltage, double NIDC_Current)
        {
            try
            {
                int Channel = Convert.ToInt32(Channel_Name);
                Power_Control_Session[Channel].Utility.Reset();  //Disable Output
                Power_Control_Session[Channel].Outputs[Channel_Name].Source.Current.CurrentLevel = NIDC_Current;    //Set Current Level
                Power_Control_Session[Channel].Outputs[Channel_Name].Source.Current.VoltageLimit = NIDC_Voltage;    //Set Voltage Level
                Power_Control_Session[Channel].Outputs[Channel_Name].Source.SourceDelay = new NationalInstruments.PrecisionTimeSpan(decimal.ToDouble(NIDC_Source_Delay));   //Set the Power Up Delay Time
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Power Channel Setup Fail "));
            }
        }

        public void NIDC_Power_ON(string Channel_Name)
        {
            try 
            {
                int Channel = Convert.ToInt32(Channel_Name);
                Power_Control_Session[Channel].Control.Initiate();
                Power_Control_Session[Channel].Events.SourceCompleteEvent.WaitForEvent(new NationalInstruments.PrecisionTimeSpan(decimal.ToDouble(NIDC_Source_Delay)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Power ON " + Channel_Name + " Fail" ));
            }
        }

        public void NIDC_Power_OFF(string Channel_Name)
        {
            try
            {
                int Channel = Convert.ToInt32(Channel_Name);
                Power_Control_Session[Channel].Utility.Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Power OFF " + Channel_Name + " Fail"));
            }
        }


        public DCPowerMeasureResult NIDC_Measurement_Result(string Channel_Name)
        {
            return Power_Control_Session[Convert.ToInt32(Channel_Name)].Measurement.Measure(Channel_Name);
        }
    }
}
