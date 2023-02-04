using MySample.Micom;
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
            Console.WriteLine("START MiCOM CONNECTION---------------------------------------------------------------------------------------------------------");
            try
            {
                //1. Select MiCOM (ModbusTCP over RTU device)
                SlaveDevice sd = new SlaveDevice(selected: 0); // Localhost ModSim for application tests

                //2. Get connection to selected MiCOM device and get data
                SlaveConnection sc = new SlaveConnection(slaveAddress: sd);
                List<ushort[]> aerv = sc.GetData();  
                
                //3. Convert downloaded data
                var micomFactory = new MicomFactory();
                IMicom micom = micomFactory.GetMicom(model:sd.MicomModel, allEventsRawValues:aerv);

                //4. Save data to text file
                micom.SaveToTextFile(sd.SlaveId);

                //LOGS
                Console.WriteLine("\nDEVICE INFORMATION:-----------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"R6kV-R8-MiCOM-field: {sd.SlaveId}");
                Console.WriteLine($"   IP address = {sd.IpAddress}");
                Console.WriteLine($"   Port = {sd.Port}");
                Console.WriteLine($"   Slave ID = {sd.SlaveId}");
                Console.WriteLine($"   Model = {sd.MicomModel}");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("\nFinished. Press ENTER key to finish...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"LOG | ERROR: {e.Message}");
                Console.WriteLine("Press ENTER key to finish...");
                Console.ReadLine();
            }
        }
    }
}
