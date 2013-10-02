using Ivi.DCPwr;
using Ivi.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.ModularInstruments.SystemServices.DeviceServices;

namespace NI_Test_Software.NI_Equipment
{
    class NI_Common_Utilities
    {
        public decimal time_delay = 0;
        
        PrecisionTimeSpan SourceDelay
        {
            get
            {
                return new PrecisionTimeSpan(this.time_delay);
            }
        }

    }
}
