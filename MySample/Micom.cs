using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySample
{
    /// <summary>
    /// Parent class for MiCOM devices
    /// Parameters from table, document: P12x_EN_T_Fc6__V13.pdf page 312:
    /// <param name="EventCode">Code of the event</para>
    /// <param name="EventDescription">Description ot the event</param>
    /// <param name="EventModbusAddress">Modbus address that more details about event</param>
    /// <param name="EventType">Event data format - from manual</param>
    /// 
    /// <param name="EventModbusAddressList">Modbus address that more details about event</param>
    /// </summary>
    internal abstract class Micom
    {
        public ushort EventCode { get; set; }
        public String EventDescription { get; }
        public String EventType { get; }
        public ushort EventModbusAddress { get; }
        public List<ushort[]> EventModbusAddressList { get; }

        private ushort eventCode, eventModbusAddress;
        private String eventDescription, eventType;
        private List<ushort[]> eventModbusAddressList;

        /// <summary>
        /// Method that encode basic event description
        /// </summary>
        public abstract void GetEventCode();
        /*
        /// <summary>
        /// Method that gets the time that event occured
        /// </summary>
        public abstract void GetEventTime();

        /// <summary>
        /// Gets more details from additional Modbus register
        /// </summary>
        public abstract void GetEventAdditionalInfo();

        /// <summary>
        /// Gets ifnormation if the event was confirmed by user
        /// </summary>
        public abstract void GetEventConfirmedInfo();
        */
    }
}
