using System;
using System.Collections.Generic;
using System.Net;

namespace MySample
{
    // <summary>
    // Main class of the application
    // </summary>
    internal class Driver
    {
        static void Main(String[] args)
        {
            try
            {   
                //1. Select MiCOM (ModbusTCP over RTU device)
                SlaveAddress sa = new SlaveAddress(device: 0);
                Console.WriteLine("1.OK");

                //2. Get connection to selected MiCOM device
                SlaveConnection sc = new SlaveConnection(slaveAddress: sa);
                Console.WriteLine("2.OK");

                //3. Get list of all events from single selected MiCOM as Modbus register values
                List<ushort[]> allEventsRawValues = sc.GetEventsRawRegistersValues();
                Console.WriteLine("3.OK");

                //4. Single event interpretation
                Micom m = new MicomP122(allEventsRawValues); // default device
                switch (sa.MicomModel)
                {
                    case MicomModel.P122: m = new MicomP122(allEventsRawValues); break;
                    case MicomModel.P123: m = new MicomP122(allEventsRawValues); break;
                        //case MicomModel.P922: m = new MicomP922(eventsValues); break;
                }

                m.GetEventCode();

                foreach (ushort[] item in allEventsRawValues)
                {

                    //Console.WriteLine($"Event: {m.EventCode}; {m.EventDescription}; {m.EventType}; "); 
                }

                Console.WriteLine("4.OK");




                // RESULT PRINTING
                Console.WriteLine("\nNETWORK:------------------------------");
                Console.WriteLine($"IP address = {sa.IpAddress}");
                Console.WriteLine($"Port = {sa.Port}");
                Console.WriteLine($"ID = {sa.SlaveId}");
                Console.WriteLine($"Model = {sa.MicomModel}");
                Console.WriteLine("\n\nFinished. Press any key to finish...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LOG | Exception {0}", ex);
                Console.WriteLine("Press any key to finish...");
                Console.ReadLine();
            }
        }
    }
}
