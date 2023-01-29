using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySample
{
    internal class DisturbanceEvent
    {
        public ushort EventCode
        {
            get { return eventCode; }
            set { eventCode = value; }
        }
        public String EventDescription
        {
            get { return eventDescription; }
            set { eventDescription = value; }
        }
        public ushort EventAdditionalValue
        {
            get { return eventAdditionalValue; }
            set { eventAdditionalValue = value; }
        }
        public DateTime EventDateTime
        {
            get { return eventDateTime; }
            set { eventDateTime = value; }
        }


        private ushort eventCode, eventAdditionalValue;
        private DateTime eventDateTime;
        private String eventDescription;
    }
}
