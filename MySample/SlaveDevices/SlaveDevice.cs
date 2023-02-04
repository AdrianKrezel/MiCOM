using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace MySample
{
    /// <summary>
    /// Class that include list of all MiCOM devices installed in switchgears
    /// withs it's IP addresses, ports, device IDs and device model
    /// </summary>
    internal class SlaveDevice
    {
        public String IpAddress { get { return ipAddress; } }
        public ushort Port { get { return port; } }
        public ushort SlaveId { get { return slaveId; } }
        public ushort Selected { set { selected = value; } }
        public MicomModel MicomModel { get { return mm; } }

        private String ipAddress;
        private ushort port, slaveId, selected;
        private MicomModel mm;

        /// <summary>
        /// Creates slave device with IP, port, ID and models selected
        /// </summary>
        /// <param name="selected">MiCOM slave device to be read</param>
        public SlaveDevice(ushort selected)
        {
            this.selected = selected;
            GetAddress(this.selected);
        }

        /// <summary>
        /// Assigns IP address, TCP port numder and Modbus ID
        /// </summary>
        /// <param name="selected"> Field number </param>
        public void GetAddress(ushort selected)
        {
            try
            {
                switch (selected)
                {
                    case 0:  ipAddress = "127.0.0.1";   port = 502; slaveId = 1;  mm = MicomModel.P122; break; //TEST localhost ModSim
                    case 1:  ipAddress = "10.20.61.9";  port = 502; slaveId = 1;  mm = MicomModel.P122; break; //R6kV R8 p01
                    case 3:  ipAddress = "10.20.61.9";  port = 502; slaveId = 3;  mm = MicomModel.P122; break; //R6kV R8 p03
                    case 5:  ipAddress = "10.20.61.9";  port = 502; slaveId = 5;  mm = MicomModel.P122; break; //R6kV R8 p05
                    case 7:  ipAddress = "10.20.61.9";  port = 502; slaveId = 7;  mm = MicomModel.P122; break; //R6kV R8 p07
                    case 9:  ipAddress = "10.20.61.10"; port = 502; slaveId = 9;  mm = MicomModel.P122; break; //R6kV R8 p09
                    case 11: ipAddress = "10.20.61.10"; port = 502; slaveId = 11; mm = MicomModel.P122; break; //R6kV R8 p11
                    case 13: ipAddress = "10.20.61.10"; port = 502; slaveId = 13; mm = MicomModel.P122; break; //R6kV R8 p13
                    case 12: ipAddress = "10.20.61.10"; port = 502; slaveId = 12; mm = MicomModel.P122; break; //R6kV R8 p12
                    case 25: ipAddress = "10.20.61.11"; port = 502; slaveId = 25; mm = MicomModel.P122; break; //R6kV R8 p25
                    case 2:  ipAddress = "10.20.61.12"; port = 502; slaveId = 2;  mm = MicomModel.P122; break; //R6kV R8 p02
                    case 14: ipAddress = "10.20.61.12"; port = 502; slaveId = 14; mm = MicomModel.P122; break; //R6kV R8 p14
                    case 6:  ipAddress = "10.20.61.12"; port = 502; slaveId = 6;  mm = MicomModel.P122; break; //R6kV R8 p06
                    case 10: ipAddress = "10.20.61.12"; port = 502; slaveId = 10; mm = MicomModel.P122; break; //R6kV R8 p10
                    case 8:  ipAddress = "10.20.61.13"; port = 502; slaveId = 8;  mm = MicomModel.P122; break; //R6kV R8 p08
                    case 4:  ipAddress = "10.20.61.13"; port = 502; slaveId = 4;  mm = MicomModel.P122; break; //R6kV R8 p04
                    case 24: ipAddress = "10.20.61.14"; port = 502; slaveId = 24; mm = MicomModel.P122; break; //R6kV R8 p24
                    case 23: ipAddress = "10.20.61.11"; port = 502; slaveId = 23; mm = MicomModel.P922; break; //R6kV R8 p23
                    case 27: ipAddress = "10.20.61.11"; port = 502; slaveId = 27; mm = MicomModel.P922; break; //R6kV R8 p27
                }
            }
            catch (Exception e) 
            { 
                Console.WriteLine($"LOG | SlaveAddress.cs: {e.Message}"); 
            }
        }
    }
}

