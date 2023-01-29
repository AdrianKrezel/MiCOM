using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace MySample
{
    // <summary>
    // Interpretation of values read from Modbus registers as events 
    // </summary>
    internal class MicomP122 : Micom
    {
        public ushort EventCode
        {
            get { return eventCode; }
            set { eventCode = value; }
        }
        public String EventDescription
        {
            get { return eventDescription; }
        }
        public String EventType
        {
            get { return eventType; }
        }
        public ushort EventModbusAddress
        {
            get { return eventModbusAddress; }
        }
        public List<ushort[]> EventModbusAddressList
        {
            get { return eventModbusAddressList; }
        }

        private ushort eventCode, eventModbusAddress;
        private String eventDescription, eventType;
        private List<ushort[]> eventModbusAddressList;

        public MicomP122(List<ushort[]> eventsValues)
        {
            foreach(ushort[] ev in eventsValues)
            {
                ev[0]
            }


            this.eventCode = EventCode;
            GetEventCode();
        }

        /// <summary>
        /// Method that encode basic event description
        /// </summary>
        public override void GetEventCode()
        {
            switch (eventCode)
            {
                case 0: eventDescription = "No event"; eventType = ""; break;
                case 1: eventDescription = "Control close order (remote & HMI)"; eventType = "F9a"; eventModbusAddress = 20; break;
                case 2: eventDescription = "Control trip order (remote & HMI)"; eventType = "F9a"; eventModbusAddress = 20; break;
                case 3: eventDescription = "Disturbance recording start"; eventType = "F74"; eventModbusAddress = 1; break;
                case 4: eventDescription = "Trip output delatch"; eventType = "F9a"; eventModbusAddress = 20; break;
                case 5: eventDescription = "Setting change"; eventType = "Address"; eventModbusAddress = 1; break;
                case 6: eventDescription = "Remote thermal reset"; eventType = "F9a"; eventModbusAddress = 1; break;
                case 7: eventDescription = "Maintenance Mode"; eventType = "F9a"; eventModbusAddress = 65; break;
                case 8: eventDescription = "Control relay in maintenance mode"; eventType = "F39"; eventModbusAddress = 20; break;
                case 9: eventDescription = "I>"; eventType = "F17"; eventModbusAddress = 21; break;
                case 10: eventDescription = "I>>"; eventType = "F17"; eventModbusAddress = 22; break;
                case 11: eventDescription = "I>>>"; eventType = "F17"; eventModbusAddress = 23; break;
                case 12: eventDescription = "IE>"; eventType = "F16"; eventModbusAddress = 24; break;
                case 13: eventDescription = "IE>>"; eventType = "F16"; eventModbusAddress = 25; break;
                case 14: eventDescription = "IE>>>"; eventType = "F16"; eventModbusAddress = 26; break;
                case 15: eventDescription = "Thermal overload alarm"; eventType = "F37"; eventModbusAddress = 33; break;
                case 16: eventDescription = "Thermal overload threshold"; eventType = "F37"; eventModbusAddress = 33; break;
                case 17: eventDescription = "tI>"; eventType = "F17"; eventModbusAddress = 21; break;
                case 18: eventDescription = "tI>>"; eventType = "F17"; eventModbusAddress = 22; break;
                case 19: eventDescription = "tI>>>"; eventType = "F17"; eventModbusAddress = 23; break;
                case 20: eventDescription = "tIE>"; eventType = "F16"; eventModbusAddress = 24; break;
                case 21: eventDescription = "tIE>>"; eventType = "F16"; eventModbusAddress = 25; break;
                case 22: eventDescription = "tIE>>>"; eventType = "F16"; eventModbusAddress = 26; break;
                case 23: eventDescription = "tI<"; eventType = "F16"; eventModbusAddress = 34; break;
                case 24: eventDescription = "Broken conductor"; eventType = "F38"; eventModbusAddress = 36; break;
                case 25: eventDescription = "tAux 1"; eventType = "F38"; eventModbusAddress = 36; break;
                case 26: eventDescription = "tAux 2"; eventType = "F38"; eventModbusAddress = 36; break;
                case 27: eventDescription = "CB failure"; eventType = "F38"; eventModbusAddress = 36; break;
                case 28: eventDescription = "Selective logic 1"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 29: eventDescription = "Selective logic 2"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 30: eventDescription = "Blocking logic 1"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 31: eventDescription = "Blocking logic 2"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 32: eventDescription = "Setting group change"; eventType = "1 or 2"; eventModbusAddress = 18; break;
                case 33: eventDescription = "52a"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 34: eventDescription = "52b"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 35: eventDescription = "Acknowledgement of the output relay latched, by logic input,"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 36: eventDescription = "SF6"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 37: eventDescription = "Cold load start"; eventType = "F20a"; eventModbusAddress = 18; break;
                case 38: eventDescription = "Change of input logic state"; eventType = "F12"; eventModbusAddress = 17; break;
                case 39: eventDescription = "Thermal overload trip"; eventType = "F37"; eventModbusAddress = 20; break;
                case 40: eventDescription = "tI> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 41: eventDescription = "tI>> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 42: eventDescription = "tI>>> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 43: eventDescription = "tIE> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 44: eventDescription = "tIE>> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 45: eventDescription = "tIE>>> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 46: eventDescription = "tI< trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 47: eventDescription = "Broken conductor trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 48: eventDescription = "tAux 1 trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 49: eventDescription = "tAux 2 trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 50: eventDescription = "Output relays command"; eventType = "F39"; eventModbusAddress = 20; break;
                case 51: eventDescription = "Front panel single alarm acknowl."; eventType = ""; eventModbusAddress = 1; break;
                case 52: eventDescription = "Front panel all alarms acknowledge"; eventType = ""; eventModbusAddress = 1; break;
                case 53: eventDescription = "Remote single alarm acknowledge"; eventType = ""; eventModbusAddress = 1; break;
                case 54: eventDescription = "Remote all alarms acknowledge"; eventType = ""; eventModbusAddress = 1; break;
                case 55: eventDescription = "Major material alarm"; eventType = "F45"; eventModbusAddress = 16; break;
                case 56: eventDescription = "Minor material alarm"; eventType = "F45"; eventModbusAddress = 16; break;
                case 57: eventDescription = "I2>"; eventType = "F16"; eventModbusAddress = 35; break;
                case 58: eventDescription = "tI2>"; eventType = "F16"; eventModbusAddress = 35; break;
                case 59: eventDescription = "Operation time"; eventType = "F43"; eventModbusAddress = 41; break;
                case 60: eventDescription = "Operation numbers"; eventType = "F43"; eventModbusAddress = 41; break;
                case 61: eventDescription = "Sum of switched square amps"; eventType = "F43"; eventModbusAddress = 41; break;
                case 62: eventDescription = "Trip circuit supervision"; eventType = "F43"; eventModbusAddress = 41; break;
                case 63: eventDescription = "Closing time"; eventType = "F43"; eventModbusAddress = 41; break;
                case 64: eventDescription = "Reclose successful"; eventType = "F43"; eventModbusAddress = 41; break;
                case 65: eventDescription = "Recloser final trip"; eventType = "F43"; eventModbusAddress = 41; break;
                case 66: eventDescription = "Recloser settings error or configuration error"; eventType = "F43"; eventModbusAddress = 41; break;
                case 67: eventDescription = "I2> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 68: eventDescription = "General Starting (IEC103)"; eventType = "F1"; eventModbusAddress = 10; break;
                case 69: eventDescription = "Recloser active (IEC103)"; eventType = "F43"; eventModbusAddress = 41; break;
                case 70: eventDescription = "CB Closed by autoreclosure (IEC103)"; eventType = ""; eventModbusAddress = 1; break;
                case 71: eventDescription = "Relays latching"; eventType = "F13"; eventModbusAddress = 47; break;
                case 72: eventDescription = "External CB failure"; eventType = "F20b"; eventModbusAddress = 43; break;
                case 73: eventDescription = "I<"; eventType = "F16"; eventModbusAddress = 34; break;
                case 74: eventDescription = "I2>>"; eventType = "F16"; eventModbusAddress = 35; break;
                case 75: eventDescription = "tI2>>"; eventType = "F16"; eventModbusAddress = 35; break;
                case 76: eventDescription = "I2>> Trip"; eventType = "F16"; eventModbusAddress = 20; break;
                case 77: eventDescription = "Reserved"; eventType = ""; eventModbusAddress = 1; break;
                case 78: eventDescription = "Latching Trip Relay (RL1)"; eventType = "F22"; eventModbusAddress = 1; break;
                case 79: eventDescription = "tAux 3"; eventType = "F38"; eventModbusAddress = 36; break;
                case 80: eventDescription = "tAux 3 trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 81: eventDescription = "tAux 4"; eventType = "F38"; eventModbusAddress = 36; break;
                case 82: eventDescription = "tAux 4 trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 83: eventDescription = "t Reset I>"; eventType = "F17"; eventModbusAddress = 21; break;
                case 84: eventDescription = "t Reset I>>"; eventType = "F17"; eventModbusAddress = 22; break;
                case 85: eventDescription = "t Reset IE>"; eventType = "F16"; eventModbusAddress = 24; break;
                case 86: eventDescription = "t Reset IE>>"; eventType = "F16"; eventModbusAddress = 25; break;
                case 87: eventDescription = "t Reset I2>"; eventType = "F16"; eventModbusAddress = 35; break;
                case 88: eventDescription = "TRIP Breaker Failure"; eventType = "F13"; eventModbusAddress = 20; break;
                case 89: eventDescription = "t BF / Ext. Breaker Failure"; eventType = "F38"; eventModbusAddress = 36; break;
                case 90: eventDescription = "Manual Close (input)"; eventType = "F20b"; eventModbusAddress = 43; break;
                case 91: eventDescription = "t SOTF"; eventType = "F54"; eventModbusAddress = 113; break;
                case 92: eventDescription = "t SOTF trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 93: eventDescription = "Local Mode (IEC 103)"; eventType = "F20b"; eventModbusAddress = 43; break;
                case 94: eventDescription = "Reset leds (IEC103)"; eventType = ""; eventModbusAddress = 1; break;
                case 95: eventDescription = "Recloser internal locked"; eventType = "F43"; eventModbusAddress = 41; break;
                case 96: eventDescription = "Recloser in progress"; eventType = "F43"; eventModbusAddress = 41; break;
                case 97: eventDescription = "Synchronization > 10s"; eventType = "F23"; eventModbusAddress = 1; break;
                case 98: eventDescription = "Inrush blocking"; eventType = "F38"; eventModbusAddress = 1; break;
                case 99: eventDescription = "tEquation A"; eventType = "F61"; eventModbusAddress = 114; break;
                case 100: eventDescription = "tEquation B"; eventType = "F61"; eventModbusAddress = 114; break;
                case 101: eventDescription = "tEquation C"; eventType = "F61"; eventModbusAddress = 114; break;
                case 102: eventDescription = "tEquation D"; eventType = "F61"; eventModbusAddress = 114; break;
                case 103: eventDescription = "tEquation E"; eventType = "F61"; eventModbusAddress = 114; break;
                case 104: eventDescription = "tEquation F"; eventType = "F61"; eventModbusAddress = 114; break;
                case 105: eventDescription = "tEquation G"; eventType = "F61"; eventModbusAddress = 114; break;
                case 106: eventDescription = "tEquation H"; eventType = "F61"; eventModbusAddress = 114; break;
                case 107: eventDescription = "tEquation A trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 108: eventDescription = "tEquation B trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 109: eventDescription = "tEquation C trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 110: eventDescription = "tEquation D trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 111: eventDescription = "tEquation E trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 112: eventDescription = "tEquation F trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 113: eventDescription = "tEquation G trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 114: eventDescription = "tEquation H trip"; eventType = "F13"; eventModbusAddress = 1; break;
                case 115: eventDescription = "CB activity Operation time"; eventType = "F43"; eventModbusAddress = 41; break;
                case 116: eventDescription = "Ie_d>"; eventType = "F16"; eventModbusAddress = 116; break;
                case 117: eventDescription = "tIe_d>"; eventType = "F16"; eventModbusAddress = 116; break;
                case 118: eventDescription = "tIe_d> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 119: eventDescription = "t Reset Ie_d>"; eventType = "F16"; eventModbusAddress = 116; break;
                case 120: eventDescription = "tAux 5"; eventType = "F38"; eventModbusAddress = 36; break;
                case 121: eventDescription = "tAux 5 trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 122: eventDescription = "Do not use"; eventType = ""; eventModbusAddress = 1; break;
                case 123: eventDescription = "Recloser external locked"; eventType = "F43"; eventModbusAddress = 41; break;
                case 124: eventDescription = "Hardware alarm with main power supply"; eventType = "F2 unit mV"; eventModbusAddress = 16; break;
                case 125: eventDescription = "Hardware alarm with -3.3v power supply"; eventType = "F2 unit mV"; eventModbusAddress = 57; break;
                case 126: eventDescription = "Hardware alarm with 5.0v power supply"; eventType = "F2 unit mV"; eventModbusAddress = 57; break;
                case 127: eventDescription = "Hardware alarm with 3.3v power supply"; eventType = "F2 unit mV"; eventModbusAddress = 57; break;
                case 128: eventDescription = "Hardware alarm with 12v power supply"; eventType = "F2 unit mV"; eventModbusAddress = 57; break;
                case 129: eventDescription = "Hardware alarm with 1.3v power supply"; eventType = "F2 unit mV"; eventModbusAddress = 57; break;
                case 130: eventDescription = "Hardware alarm with 0 v power supply"; eventType = "F2 unit mV"; eventModbusAddress = 57; break;
                case 131: eventDescription = "Hardware alarm with transformer 1"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 132: eventDescription = "Hardware alarm with transformer 2"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 133: eventDescription = "Hardware alarm with transformer 3"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 134: eventDescription = "Hardware alarm with transformer 4"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 135: eventDescription = "Hardware alarm with transformer 5"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 136: eventDescription = "Hardware alarm with transformer 6"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 137: eventDescription = "Hardware alarm with transformer 7"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 138: eventDescription = "Hardware alarm with transformer 8"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 139: eventDescription = "Hardware alarm with transformer 9"; eventType = "F2 unit CAN"; eventModbusAddress = 58; break;
                case 140: eventDescription = "Ie_d>>"; eventType = "F16"; eventModbusAddress = 134; break;
                case 141: eventDescription = "tIe_d>>"; eventType = "F16"; eventModbusAddress = 134; break;
                case 142: eventDescription = "tIe_d>> trip"; eventType = "F13"; eventModbusAddress = 20; break;
                case 143: eventDescription = "t Reset Ie_d>>"; eventType = "F16"; eventModbusAddress = 134; break;
                case 144: eventDescription = "Alarm with cortec mismatch"; eventType = ""; eventModbusAddress = 1; break;
                default:
                    Console.WriteLine("LOG | MicomP122.cs: Event code: {0} not supported", eventCode);
                    break;
            }
        }

        //TODO
        /// <summary>
        /// Method that gets the time that event occured
        /// </summary>
          /*
           
        public override void GetEventTime()
        {
            throw new NotImplementedException();
        }
      
        //TODO
        /// <summary>
        /// Gets more details from additional Modbus register
        /// </summary>
        public override void GetEventAdditionalInfo()
        {
            throw new NotImplementedException();
        }

        //TODO
        /// <summary>
        /// Gets ifnormation if the event was confirmed by user
        /// </summary>
        public override void GetEventConfirmedInfo()
        {
            throw new NotImplementedException();
        }
        */
    }
}
