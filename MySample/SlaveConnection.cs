using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace MySample
{
    /// <summary>
    /// Class used for device connection to MiCOM P122 and getting 
    /// raw values from Modbus registers
    /// </summary>
    internal class SlaveConnection
    {
        public List<ushort[]> AllEventsRawValues { get { return allEventsRawValues; } }

        private String ipAddress;
        private ushort port, slaveId;
        private List<ushort[]> allEventsRawValues = new List<ushort[]>();
        private ModbusIpMaster master;

        public SlaveConnection(SlaveAddress slaveAddress)
        {
            this.ipAddress = slaveAddress.IpAddress;
            this.port = slaveAddress.Port;
            this.slaveId = slaveAddress.SlaveId;

            master = Connect();
        }

        /// <summary>
        /// Connects to slave device
        /// </summary>
        /// <returns></returns>
        private ModbusIpMaster Connect()
        {
            try
            {
                TcpClient client = new TcpClient(ipAddress, port);
                this.master = ModbusIpMaster.CreateIp(client);
            }
            catch (Exception e)
            {
                Console.WriteLine("LOG | Device.cs: Error during connection to slave device /n" + e.Message);
            }
            return master;
        }


        /// <summary>
        /// Gets all of events from MiCOM P122 device as registers values
        /// </summary>
        /// <param name="startAddr">1st Modbus register to be read</param>
        /// <param name="length">Number of consecutive registers to be read</param>
        /// <returns>List of events as "rare" values</returns>
        public List<ushort[]> GetEventsRawRegistersValues(ushort startAddr = 13569, ushort length = 9)
        {
            ushort i = 0;
            while (true)
            {
                ushort[] ev = this.master.ReadHoldingRegisters(slaveAddress: (byte)slaveId, startAddress: (ushort)(startAddr + i), numberOfPoints: length);

                if (ev[0] != 0) allEventsRawValues.Add(ev); // event found -> add event to list of events
                else break;                        // finised -> no more events found
                i++;
            }
            return this.allEventsRawValues;
        }
        
    }
}

