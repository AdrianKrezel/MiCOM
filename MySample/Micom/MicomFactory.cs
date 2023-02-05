using System.Collections.Generic;

namespace MySample.Micom
{
    /// <summary>
    /// Class for selecting Micom model
    /// </summary>
    /// <returns>Instance of MiCOM device</returns>
    public class MicomFactory
    {
        /// <summary>
        /// Factory Pattern for Micom devices disturbance events interperation
        /// </summary>
        /// <param name="model">MiCOM model selected</param>
        /// <param name="allEventsRawValues">Raw values that were read from MiCOM</param>
        /// <returns>Micom device object</returns>
        public IMicom GetMicom(MicomModel model, List<ushort[]> allEventsRawValues )
        {
            switch(model)
            {
                case MicomModel.P122: return new MicomP122(allEventsRawValues);
                case MicomModel.P123: return new MicomP122(allEventsRawValues); 
                //case MicomModel.P922: return new MicomP922(allEventsRawValues);
                default: return new MicomP122(allEventsRawValues: allEventsRawValues);
            }
        }
    }
}
