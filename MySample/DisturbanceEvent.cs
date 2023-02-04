using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;

namespace MySample
{
    /// <summary>
    /// Class that represents single disturbance event data structure
    /// </summary>
    public class DisturbanceEvent
    {
        public ushort EventCode { get { return eventCode; } set { eventCode = value; } }
        public string EventMeaning { get { return eventMeaning; } set { eventMeaning = value; } }
        public ushort ModbusAssociatedValue { get { return modbusAssociatedValue; } set { modbusAssociatedValue = value; } }
        public ushort ModbusAddress { get { return modbusAddress; } set { modbusAddress = value; } }
        public string EventTime { get { return eventTime; } set { eventTime = value; } }
        public bool Acknowledged { get { return acknowledged; } set { acknowledged = value; } }
        public String EventType { get { return eventType; } set { eventType = value; } }

        private ushort eventCode;               // No.1 Code of event
        private ushort modbusAssociatedValue;   // No.2 Additional value information
        private ushort modbusAddress;           // No.3 Additional value information Modbus address

        private string eventMeaning;            // No.1 (Event code description)
        private string eventType;               // Event data type
        private string eventTime;               // Time converted for "normal" format
        private bool acknowledged;

        public DisturbanceEvent() {
            this.eventCode = EventCode;
            this.eventMeaning = EventMeaning;
            this.modbusAssociatedValue = ModbusAssociatedValue;
            this.modbusAddress = ModbusAddress;
            this.eventTime = EventTime;
            this.acknowledged = Acknowledged;
            this.eventType = EventType;
        }
    }
}
