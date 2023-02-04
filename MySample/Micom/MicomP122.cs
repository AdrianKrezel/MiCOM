using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Schema;
using System.IO;
using static log4net.Appender.RollingFileAppender;

namespace MySample.Micom
{
    /// <summary>
    /// Class to decode all off the disturbance events for MiCOM P122
    /// </summary>
    public class MicomP122 : IMicom
    {
        public List<ushort[]> AllEventsRawValues { get { return allEventsRawValues; } set { allEventsRawValues = value; } }
        public List<DisturbanceEvent> AllEvents { get { return allEvents; } }

        private string eventType;
        private string eventMeaning;
        private DisturbanceEvent de;
        private List<ushort[]> allEventsRawValues;
        private List<DisturbanceEvent> allEvents;

        private String[] allEventsToDisplay_CSV;
        private String[] allEventsToDisplay_TXT;

        /// <summary>
        /// Creates instance of MiCOM device
        /// </summary>
        /// <param name="allEventsRawValues">All the events that was get from device</param>
        public MicomP122(List<ushort[]> allEventsRawValues)
        {
            this.allEventsRawValues = allEventsRawValues;
            de = new DisturbanceEvent();
            allEvents = new List<DisturbanceEvent>();
            allEventsToDisplay_CSV = new String[allEventsRawValues.Count + 1];
            allEventsToDisplay_TXT = new String[allEventsRawValues.Count + 1];
            GetAllEvents();
        }

        /// <summary>
        /// Saves proceeded data events to text file
        /// </summary>
        /// <param name="fieldNumber">Number of the field in the switchgear</param>
        public void SaveToTextFile(ushort fieldNumber)
        {
            ProcessToDisplay();

            //path
            DateTime dt = DateTime.Now;
            string displayTime = $"{dt.Year}{dt.Month}{dt.Day}_{dt.Hour}{dt.Minute}{dt.Second}";
            string fn = ((int)fieldNumber).ToString();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string pathcsv = $"{desktopPath}\\CSV_R8-MiCOM-field{fn}_{displayTime}.csv";
            string pathtxt = $"{desktopPath}\\TXT_R8-MiCOM-field{fn}_{displayTime}.txt";
            try
            {
                File.WriteAllLines(pathcsv, allEventsToDisplay_CSV);
                File.WriteAllLines(pathtxt, allEventsToDisplay_TXT);

                Console.WriteLine("\nEVENTS SAVED TO FILES:---------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"\nFile with events saved as *.TXT to:\n   {pathtxt}");
                Console.WriteLine($"\nFile with events saved as *.CSV to:\n   {pathcsv}");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nLOG | MicomP122.cs: Error during saving events to file\n" + e.Message);
            }
        }

        /// <summary>
        /// Prepare events data to be displayed for user
        /// </summary>
        private void ProcessToDisplay()
        {
            String[] header = { "No.", "EventTime", "EventCode", "EventMeaning", "EventType", "EventValue", "Acknowledged" };
            int[] columnWidth = { 5, 25, 12, 50, 12, 12, 20 };
            for (int i = 0; i < allEvents.Count; i++)
            {
                if (i == 0)//headers
                {
                    Console.WriteLine("\nMiCOM EVENTS:------------------------------------------------------------------------------------------------------------------");
                    for (int h = 0; h < header.Length; h++)
                    {
                        //CSV header
                        if (h < header.Length - 1)
                            allEventsToDisplay_CSV[i] += $"{header[h]};";
                        else
                            allEventsToDisplay_CSV[i] += $"{header[h]}";

                        //TXT header
                        allEventsToDisplay_TXT[i] += $"{header[h]}".PadRight(columnWidth[h]);                       
                    }
                }
                else//file data body
                {
                    string index = i.ToString("000");

                    //CSV body
                    allEventsToDisplay_CSV[i] = String.Concat(
                    $"{index};" +
                    $"{allEvents[i].EventTime};" +
                    $"{allEvents[i].EventCode};" +
                    $"{allEvents[i].EventMeaning};" +
                    $"{allEvents[i].EventType};" +
                    $"{allEvents[i].ModbusAssociatedValue};" +
                    $"{allEvents[i].Acknowledged}");

                    //TXT body
                    allEventsToDisplay_TXT[i] = String.Concat(
                    $"{index}".PadRight(columnWidth[0]) +
                    $"{allEvents[i].EventTime}".PadRight(columnWidth[1]) +
                    $"{allEvents[i].EventCode}".PadRight(columnWidth[2]) +
                    $"{allEvents[i].EventMeaning}".PadRight(columnWidth[3]) +
                    $"{allEvents[i].EventType}".PadRight(columnWidth[4]) +
                    $"{allEvents[i].ModbusAssociatedValue}".PadRight(columnWidth[5]) +
                    $"{allEvents[i].Acknowledged}".PadRight(columnWidth[6]));

                    Console.WriteLine(allEventsToDisplay_TXT[i]);
                }
            }
        }

        /// <summary>
        /// Decode all the events that was get from Micom device
        /// </summary>
        public void GetAllEvents()
        {

            for (int i = 0; i < allEventsRawValues.Count; i++)
            {
                DisturbanceEvent de = new DisturbanceEvent();

                GetEventInfo(ev: allEventsRawValues[i], out eventType, out eventMeaning);
                de.EventCode = allEventsRawValues[i][0];
                de.EventMeaning = eventMeaning;
                de.EventType = eventType;

                de.ModbusAddress = allEventsRawValues[i][2];
                de.EventTime = GetEventTime(allEventsRawValues[i]);
                de.ModbusAssociatedValue = allEventsRawValues[i][1];
                de.Acknowledged = IsAcknowledged(allEventsRawValues[i]);

                allEvents.Add(de);
            }
        }

        /// <summary>
        /// Gets the MiCOM event code
        /// </summary>
        /// <param name="ev">Single eventr</param>
        /// <returns>Code of the event</returns>
        public void GetEventInfo(ushort[] ev, out string eventType, out string eventMeaning)
        {
            switch (ev[0])
            {
                case 0:
                    eventMeaning = "No more events in MiCOM internal buffer";
                    eventType = "No event Type";
                    break;
                case 1:
                    eventMeaning = "Control close order (remote & HMI)";
                    eventType = "F9a";
                    //eventModbusAddress = 20;
                    break;
                case 2:
                    eventMeaning = "Control trip order (remote & HMI)";
                    eventType = "F9a";
                    //eventModbusAddress = 20;
                    break;
                case 3:
                    eventMeaning = "Disturbance recording start";
                    eventType = "F74";
                    //eventModbusAddress = 1;
                    break;
                case 4:
                    eventMeaning = "Trip output delatch";
                    eventType = "F9a";
                    //eventModbusAddress = 20;
                    break;
                case 5:
                    eventMeaning = "Setting change";
                    eventType = "Address";
                    //eventModbusAddress = 1;
                    break;
                case 6:
                    eventMeaning = "Remote thermal reset";
                    eventType = "F9a";
                    //eventModbusAddress = 1;
                    break;
                case 7:
                    eventMeaning = "Maintenance Mode";
                    eventType = "F9a";
                    //eventModbusAddress = 65;
                    break;
                case 8:
                    eventMeaning = "Control relay in maintenance mode";
                    eventType = "F39";
                    //eventModbusAddress = 20;
                    break;
                case 9:
                    eventMeaning = "I>";
                    eventType = "F17";
                    //eventModbusAddress = 21;
                    break;
                case 10:
                    eventMeaning = "I>>";
                    eventType = "F17";
                    //eventModbusAddress = 22;
                    break;
                case 11:
                    eventMeaning = "I>>>";
                    eventType = "F17";
                    //eventModbusAddress = 23;
                    break;
                case 12:
                    eventMeaning = "IE>";
                    eventType = "F16";
                    //eventModbusAddress = 24;
                    break;
                case 13:
                    eventMeaning = "IE>>";
                    eventType = "F16";
                    //eventModbusAddress = 25;
                    break;
                case 14:
                    eventMeaning = "IE>>>";
                    eventType = "F16";
                    //eventModbusAddress = 26;
                    break;
                case 15:
                    eventMeaning = "Thermal overload alarm";
                    eventType = "F37";
                    //eventModbusAddress = 33;
                    break;
                case 16:
                    eventMeaning = "Thermal overload threshold";
                    eventType = "F37";
                    //eventModbusAddress = 33;
                    break;
                case 17:
                    eventMeaning = "tI>";
                    eventType = "F17";
                    //eventModbusAddress = 21;
                    break;
                case 18:
                    eventMeaning = "tI>>";
                    eventType = "F17";
                    //eventModbusAddress = 22;
                    break;
                case 19:
                    eventMeaning = "tI>>>";
                    eventType = "F17";
                    //eventModbusAddress = 23;
                    break;
                case 20:
                    eventMeaning = "tIE>";
                    eventType = "F16";
                    //eventModbusAddress = 24;
                    break;
                case 21:
                    eventMeaning = "tIE>>";
                    eventType = "F16";
                    //eventModbusAddress = 25;
                    break;
                case 22:
                    eventMeaning = "tIE>>>";
                    eventType = "F16";
                    //eventModbusAddress = 26;
                    break;
                case 23:
                    eventMeaning = "tI<";
                    eventType = "F16";
                    //eventModbusAddress = 34;
                    break;
                case 24:
                    eventMeaning = "Broken conductor";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 25:
                    eventMeaning = "tAux 1";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 26:
                    eventMeaning = "tAux 2";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 27:
                    eventMeaning = "CB failure";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 28:
                    eventMeaning = "Selective logic 1";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 29:
                    eventMeaning = "Selective logic 2";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 30:
                    eventMeaning = "Blocking logic 1";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 31:
                    eventMeaning = "Blocking logic 2";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 32:
                    eventMeaning = "Setting group change";
                    eventType = "1 or 2";
                    //eventModbusAddress = 18;
                    break;
                case 33:
                    eventMeaning = "52a";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 34:
                    eventMeaning = "52b";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 35:
                    eventMeaning = "Acknowledgement of the output relay latched, by logic input,";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 36:
                    eventMeaning = "SF6";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 37:
                    eventMeaning = "Cold load start";
                    eventType = "F20a";
                    //eventModbusAddress = 18;
                    break;
                case 38:
                    eventMeaning = "Change of input logic state";
                    eventType = "F12";
                    //eventModbusAddress = 17;
                    break;
                case 39:
                    eventMeaning = "Thermal overload trip";
                    eventType = "F37";
                    //eventModbusAddress = 20;
                    break;
                case 40:
                    eventMeaning = "tI> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 41:
                    eventMeaning = "tI>> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 42:
                    eventMeaning = "tI>>> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 43:
                    eventMeaning = "tIE> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 44:
                    eventMeaning = "tIE>> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 45:
                    eventMeaning = "tIE>>> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 46:
                    eventMeaning = "tI< trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 47:
                    eventMeaning = "Broken conductor trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 48:
                    eventMeaning = "tAux 1 trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 49:
                    eventMeaning = "tAux 2 trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 50:
                    eventMeaning = "Output relays command";
                    eventType = "F39";
                    //eventModbusAddress = 20;
                    break;
                case 51:
                    eventMeaning = "Front panel single alarm acknowl.";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 52:
                    eventMeaning = "Front panel all alarms acknowledge";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 53:
                    eventMeaning = "Remote single alarm acknowledge";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 54:
                    eventMeaning = "Remote all alarms acknowledge";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 55:
                    eventMeaning = "Major material alarm";
                    eventType = "F45";
                    //eventModbusAddress = 16;
                    break;
                case 56:
                    eventMeaning = "Minor material alarm";
                    eventType = "F45";
                    //eventModbusAddress = 16;
                    break;
                case 57:
                    eventMeaning = "I2>";
                    eventType = "F16";
                    //eventModbusAddress = 35;
                    break;
                case 58:
                    eventMeaning = "tI2>";
                    eventType = "F16";
                    //eventModbusAddress = 35;
                    break;
                case 59:
                    eventMeaning = "Operation time";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 60:
                    eventMeaning = "Operation numbers";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 61:
                    eventMeaning = "Sum of switched square amps";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 62:
                    eventMeaning = "Trip circuit supervision";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 63:
                    eventMeaning = "Closing time";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 64:
                    eventMeaning = "Reclose successful";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 65:
                    eventMeaning = "Recloser final trip";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 66:
                    eventMeaning = "Recloser settings error or configuration error";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 67:
                    eventMeaning = "I2> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 68:
                    eventMeaning = "General Starting (IEC103)";
                    eventType = "F1";
                    //eventModbusAddress = 10;
                    break;
                case 69:
                    eventMeaning = "Recloser active (IEC103)";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 70:
                    eventMeaning = "CB Closed by autoreclosure (IEC103)";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 71:
                    eventMeaning = "Relays latching";
                    eventType = "F13";
                    //eventModbusAddress = 47;
                    break;
                case 72:
                    eventMeaning = "External CB failure";
                    eventType = "F20b";
                    //eventModbusAddress = 43;
                    break;
                case 73:
                    eventMeaning = "I<";
                    eventType = "F16";
                    //eventModbusAddress = 34;
                    break;
                case 74:
                    eventMeaning = "I2>>";
                    eventType = "F16";
                    //eventModbusAddress = 35;
                    break;
                case 75:
                    eventMeaning = "tI2>>";
                    eventType = "F16";
                    //eventModbusAddress = 35;
                    break;
                case 76:
                    eventMeaning = "I2>> Trip";
                    eventType = "F16";
                    //eventModbusAddress = 20;
                    break;
                case 77:
                    eventMeaning = "Reserved";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 78:
                    eventMeaning = "Latching Trip Relay (RL1)";
                    eventType = "F22";
                    //eventModbusAddress = 1;
                    break;
                case 79:
                    eventMeaning = "tAux 3";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 80:
                    eventMeaning = "tAux 3 trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 81:
                    eventMeaning = "tAux 4";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 82:
                    eventMeaning = "tAux 4 trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 83:
                    eventMeaning = "t Reset I>";
                    eventType = "F17";
                    //eventModbusAddress = 21;
                    break;
                case 84:
                    eventMeaning = "t Reset I>>";
                    eventType = "F17";
                    //eventModbusAddress = 22;
                    break;
                case 85:
                    eventMeaning = "t Reset IE>";
                    eventType = "F16";
                    //eventModbusAddress = 24;
                    break;
                case 86:
                    eventMeaning = "t Reset IE>>";
                    eventType = "F16";
                    //eventModbusAddress = 25;
                    break;
                case 87:
                    eventMeaning = "t Reset I2>";
                    eventType = "F16";
                    //eventModbusAddress = 35;
                    break;
                case 88:
                    eventMeaning = "TRIP Breaker Failure";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 89:
                    eventMeaning = "t BF / Ext. Breaker Failure";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 90:
                    eventMeaning = "Manual Close (input)";
                    eventType = "F20b";
                    //eventModbusAddress = 43;
                    break;
                case 91:
                    eventMeaning = "t SOTF";
                    eventType = "F54";
                    //eventModbusAddress = 113;
                    break;
                case 92:
                    eventMeaning = "t SOTF trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 93:
                    eventMeaning = "Local Mode (IEC 103)";
                    eventType = "F20b";
                    //eventModbusAddress = 43;
                    break;
                case 94:
                    eventMeaning = "Reset leds (IEC103)";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 95:
                    eventMeaning = "Recloser internal locked";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 96:
                    eventMeaning = "Recloser in progress";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 97:
                    eventMeaning = "Synchronization > 10s";
                    eventType = "F23";
                    //eventModbusAddress = 1;
                    break;
                case 98:
                    eventMeaning = "Inrush blocking";
                    eventType = "F38";
                    //eventModbusAddress = 1;
                    break;
                case 99:
                    eventMeaning = "tEquation A";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 100:
                    eventMeaning = "tEquation B";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 101:
                    eventMeaning = "tEquation C";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 102:
                    eventMeaning = "tEquation D";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 103:
                    eventMeaning = "tEquation E";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 104:
                    eventMeaning = "tEquation F";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 105:
                    eventMeaning = "tEquation G";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 106:
                    eventMeaning = "tEquation H";
                    eventType = "F61";
                    //eventModbusAddress = 114;
                    break;
                case 107:
                    eventMeaning = "tEquation A trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 108:
                    eventMeaning = "tEquation B trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 109:
                    eventMeaning = "tEquation C trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 110:
                    eventMeaning = "tEquation D trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 111:
                    eventMeaning = "tEquation E trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 112:
                    eventMeaning = "tEquation F trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 113:
                    eventMeaning = "tEquation G trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 114:
                    eventMeaning = "tEquation H trip";
                    eventType = "F13";
                    //eventModbusAddress = 1;
                    break;
                case 115:
                    eventMeaning = "CB activity Operation time";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 116:
                    eventMeaning = "Ie_d>";
                    eventType = "F16";
                    //eventModbusAddress = 116;
                    break;
                case 117:
                    eventMeaning = "tIe_d>";
                    eventType = "F16";
                    //eventModbusAddress = 116;
                    break;
                case 118:
                    eventMeaning = "tIe_d> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 119:
                    eventMeaning = "t Reset Ie_d>";
                    eventType = "F16";
                    //eventModbusAddress = 116;
                    break;
                case 120:
                    eventMeaning = "tAux 5";
                    eventType = "F38";
                    //eventModbusAddress = 36;
                    break;
                case 121:
                    eventMeaning = "tAux 5 trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 122:
                    eventMeaning = "Do not use";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                case 123:
                    eventMeaning = "Recloser external locked";
                    eventType = "F43";
                    //eventModbusAddress = 41;
                    break;
                case 124:
                    eventMeaning = "Hardware alarm with main power supply";
                    eventType = "F2 unit mV";
                    //eventModbusAddress = 16;
                    break;
                case 125:
                    eventMeaning = "Hardware alarm with -3.3v power supply";
                    eventType = "F2 unit mV";
                    //eventModbusAddress = 57;
                    break;
                case 126:
                    eventMeaning = "Hardware alarm with 5.0v power supply";
                    eventType = "F2 unit mV";
                    //eventModbusAddress = 57;
                    break;
                case 127:
                    eventMeaning = "Hardware alarm with 3.3v power supply";
                    eventType = "F2 unit mV";
                    //eventModbusAddress = 57;
                    break;
                case 128:
                    eventMeaning = "Hardware alarm with 12v power supply";
                    eventType = "F2 unit mV";
                    //eventModbusAddress = 57;
                    break;
                case 129:
                    eventMeaning = "Hardware alarm with 1.3v power supply";
                    eventType = "F2 unit mV";
                    //eventModbusAddress = 57;
                    break;
                case 130:
                    eventMeaning = "Hardware alarm with 0 v power supply";
                    eventType = "F2 unit mV";
                    //eventModbusAddress = 57;
                    break;
                case 131:
                    eventMeaning = "Hardware alarm with transformer 1";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 132:
                    eventMeaning = "Hardware alarm with transformer 2";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 133:
                    eventMeaning = "Hardware alarm with transformer 3";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 134:
                    eventMeaning = "Hardware alarm with transformer 4";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 135:
                    eventMeaning = "Hardware alarm with transformer 5";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 136:
                    eventMeaning = "Hardware alarm with transformer 6";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 137:
                    eventMeaning = "Hardware alarm with transformer 7";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 138:
                    eventMeaning = "Hardware alarm with transformer 8";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 139:
                    eventMeaning = "Hardware alarm with transformer 9";
                    eventType = "F2 unit CAN";
                    //eventModbusAddress = 58;
                    break;
                case 140:
                    eventMeaning = "Ie_d>>";
                    eventType = "F16";
                    //eventModbusAddress = 134;
                    break;
                case 141:
                    eventMeaning = "tIe_d>>";
                    eventType = "F16";
                    //eventModbusAddress = 134;
                    break;
                case 142:
                    eventMeaning = "tIe_d>> trip";
                    eventType = "F13";
                    //eventModbusAddress = 20;
                    break;
                case 143:
                    eventMeaning = "t Reset Ie_d>>";
                    eventType = "F16";
                    //eventModbusAddress = 134;
                    break;
                case 144:
                    eventMeaning = "Alarm with cortec mismatch";
                    eventType = "";
                    //eventModbusAddress = 1;
                    break;
                default:
                    eventMeaning = "EVENT CODE INCORRECT";
                    eventType = "EVENT CODE INCORRECT";
                    break;
            }
        }

        /// <summary>
        /// Gets the time that event has occured
        /// </summary>
        /// <param name="ev">Single event</param>
        /// <returns>Date and time that event occured</returns>
        public string GetEventTime(ushort[] ev)
        {
            string seconds = ((int)ev[5]).ToString("X2") + ((int)ev[4]).ToString("X2");
            string milliseconds = ((int)ev[6]).ToString("X4");

            int sec = int.Parse(seconds, System.Globalization.NumberStyles.HexNumber);
            int ms = int.Parse(milliseconds, System.Globalization.NumberStyles.HexNumber);

            TimeSpan time = TimeSpan.FromSeconds(sec);
            DateTime dateTime = DateTime.Today.Add(time);
            string displayTime = String.Concat(dateTime.ToString("yyyy-MM-dd HH:mm:ss:") + ms.ToString("000"));

            return displayTime;
        }

        /// <summary>
        /// Gets information if event was acknowledged by user
        /// </summary>
        /// <param name="ev">Single eventr</param>
        /// <returns>Information if event acknowledged</returns>
        public bool IsAcknowledged(ushort[] ev)
        {
            switch (ev[8])
            {
                case 0: return true;
                case 1: return false;
                default: return false;
            }
        }
    }
}
