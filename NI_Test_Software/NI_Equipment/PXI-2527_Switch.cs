using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using NationalInstruments;
using NationalInstruments.ModularInstruments.NISwitch;
using NationalInstruments.ModularInstruments.SystemServices.DeviceServices;
using NI_Test_Software.Utility;

namespace NI_Test_Software.NI_Equipment.PXI2527_Switch
{
    public class PXI2527_Switch
    {
        NISwitch switchSession;
        Dictionary<string, int> path_dict = new Dictionary<string, int>();
        General_Tools tools = new General_Tools();

        public PXI2527_Switch()
        {
            string ResourceName = "";
            string TopologyName = "";
            ModularInstrumentsSystem modularInstrumentsSystem = new ModularInstrumentsSystem("NI-SWITCH");

            foreach (DeviceInfo device in modularInstrumentsSystem.DeviceCollection)
            {
                ResourceName = device.Name;
            }

            PropertyInfo[] properties = typeof(SwitchDeviceTopology).GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (prop.GetValue(typeof(SwitchDeviceTopology), null).ToString() == "2527/Independent")
                {
                    TopologyName = "2527/Independent";
                    break;
                }
            }

            if (TopologyName == "")
            {
                MessageBox.Show("Topology Name Setup Fail");
                return;
            }

            try
            {
                switchSession = new NISwitch(ResourceName, TopologyName, false, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("Hardware Switch Setup Fail");
                return;
            }

            path_dict.Add("com0", 1);
            path_dict.Add("ch15", 2);
            path_dict.Add("ch31", 3);
            path_dict.Add("com1", 4);
        }

        ~PXI2527_Switch()
        {
            path_reset();
        }

        public void path_reset()
        {
            if (switchSession != null)
            {
                try
                {
                    switchSession.Path.DisconnectAll(); //Reset when the Session is closed
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        public void path(string src, string dst, bool connection)
        {
            try
            {
                int first = 0;
                int last = 0;
                int cnt = 0;
                string[] route_list = { src, dst };
                string[] chk_route_list = { "", "" };

                Action<string, string> path_action;
                SwitchPathCapability chk_condition = new SwitchPathCapability();

                if (connection == true)
                {
                    path_action = switchSession.Path.Connect;
                    chk_condition = SwitchPathCapability.Available;
                }
                else
                {
                    path_action = switchSession.Path.Disconnect;
                    chk_condition = SwitchPathCapability.Exists;
                }

                foreach (string pin in route_list)
                {
                    if (pin.Contains("ch"))
                    {
                        string chk = pin.Replace("ch", "");
                        if (!tools.string_number_check(chk))
                        {
                            MessageBox.Show("Source Channel Error");
                            return;
                        }

                        if (Convert.ToInt32(chk) <= 15)
                        {
                            chk_route_list[cnt] = "ch15";
                            if (switchSession.Path.CanConnect(pin, "pcom0") == chk_condition)
                            {
                                path_action(pin, "pcom0");
                                switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                            }

                        }
                        else
                        {
                            chk_route_list[cnt] = "ch31";
                            if (switchSession.Path.CanConnect(pin, "pcom1") == chk_condition)
                            {
                                path_action(pin, "pcom1");
                                switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                            }
                        }
                    }
                    else
                    {
                        if (pin.Contains("com1"))
                        {
                            if (switchSession.Path.CanConnect("pcom1plus", "com1plus") == chk_condition)
                            {
                                path_action("pcom1plus", "com1plus");
                                switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                            }

                            if (switchSession.Path.CanConnect("pcom1minus", "com1mins") == chk_condition)
                            {
                                path_action("pcom1minus", "com1mins");
                                switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                                chk_route_list[cnt] = pin;
                            }
                        }
                        else if(pin.Contains("com0"))
                            chk_route_list[cnt] = pin;
                        
                    }
                    cnt++;
                }

                //Convert the name for internal Routing
                if (path_dict[chk_route_list[0]] > path_dict[chk_route_list[1]])
                {
                    first = path_dict[chk_route_list[1]];
                    last = path_dict[chk_route_list[0]];
                    string temp = route_list[0];
                    route_list[0] = route_list[1];
                    route_list[1] = temp;
                }
                else
                {
                    first = path_dict[chk_route_list[0]];
                    last = path_dict[chk_route_list[1]];
                }

                //Setup internal Routing
                switch (first)
                {
                    case 1:
                        if (switchSession.Path.CanConnect("com0", "icom0") == chk_condition)
                        {
                            path_action("com0", "icom0");
                            switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                        }

                        if (switchSession.Path.CanConnect("icom0plus", "pcom0plus") == chk_condition)
                        {
                            path_action("icom0plus", "pcom0plus");
                            switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                        }

                        if (switchSession.Path.CanConnect("icom0minus", "pcom0minus") == chk_condition)
                        {
                            path_action("icom0minus", "pcom0minus");
                            switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                        }
                        goto case 2;
                    case 2:
                        if (last < 3)
                            break;
                        else if (switchSession.Path.CanConnect("pcom0", "pcom1") == chk_condition)
                        {
                            path_action("pcom0", "pcom1");
                            switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
                            
                        }
                        goto case 3;
                    case 3:
                        if (last < 4)
                            break;
            //            else if (switchSession.Path.CanConnect("pcom0", "pcom1") == chk_condition)
            //            {
            //                path_action("pcom0", "pcom1");
            //                switchSession.Path.WaitForDebounce(new PrecisionTimeSpan(5));
            //            }
                        goto case 4;
                    case 4:

                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Channel Name Error");
                throw ex;
            }
        }
    }
}
