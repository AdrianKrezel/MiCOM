using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;

namespace MySample
{
    /// <summary>
    /// Class used for device connection to MiCOM P122 and getting 
    /// raw values from Modbus registers
    /// </summary>
    internal class SlaveConnection
    {
        public TcpClient Client 
        { 
            get { return client; } 
            set { client = value; } 
        }

        private String ipAddress;
        private ushort port, slaveId;
        private ModbusIpMaster master;
        private TcpClient client;
        private List<ushort[]> allEventsRawValues = new List<ushort[]>();

        /// <summary>
        /// Creates instance of Modbus connection to slave device and gets data from registers
        /// </summary>
        /// <param name="slaveAddress">Parameter that contains IP, port and ID information</param>
        public SlaveConnection(SlaveDevice slaveAddress)
        {
            this.ipAddress = slaveAddress.IpAddress;
            this.port = slaveAddress.Port;
            this.slaveId = slaveAddress.SlaveId;
            master = Connect();
        }

        /// <summary>
        /// Connects to slave device
        /// </summary>
        /// <returns>Modbus Master connection</returns>
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
        /// <param name="startRegister">1st Modbus register to be read</param>
        /// <param name="length">Number of consecutive registers to be read</param>
        /// <returns>List of events as raw values</returns>
        public List<ushort[]> GetData(SlaveDevice sd) //MiCOM P122!!
        {
            ushort startRegister=0, length=0;

            switch (sd.MicomModel)
            {
                case MicomModel.P122: startRegister=13569; length = 9; break;
                case MicomModel.P123: startRegister = 13569; length = 9; break;
                //case MicomModel.P922: startRegister = 999999; length = 999999; break;
            }


            ushort i = 0;
            while (true)
            {
                try
                {
                    ushort[] ev = this.master.ReadHoldingRegisters(slaveAddress: (byte)slaveId, startAddress: (ushort)(startRegister + i), numberOfPoints: length);
                    if (ev[0] == 0) break;            // finished -> no more events found
                    else allEventsRawValues.Add(ev);  // event found -> add event to the list of events                       
                    i++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"LOG | SlaveConnection.cs: {e.Message}");
                    break;
                }
            }

            return this.allEventsRawValues;
        }
    }
}

