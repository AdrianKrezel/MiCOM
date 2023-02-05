using System.Collections.Generic;

namespace MySample.Micom
{
    /// <summary>
    /// Micom device interface that that allows to get all events and convert to save and display
    /// </summary>
    /// <returns>Micoms device interface</returns>
   public interface IMicom
    { 
        List<DisturbanceEvent> AllEvents 
        { get; }
        List<ushort[]> AllEventsRawValues 
        { get; set; }

        void GetAllEvents();
        void GetEventInfo(ushort[] singleEvent, out string eventType, out string eventMeaning);
        string GetEventTime(ushort[] singleEvent);
        bool IsAcknowledged(ushort[] singleEvent);
        void SaveToTextFile(ushort fieldNumber);
    }
}
