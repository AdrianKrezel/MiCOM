using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySample.Micom
{
   public interface IMicom
    { 
        List<DisturbanceEvent> AllEvents { get; }
        List<ushort[]> AllEventsRawValues { get; set; }

        void GetAllEvents();
        void GetEventInfo(ushort[] singleEvent, out string eventType, out string eventMeaning);
        string GetEventTime(ushort[] singleEvent);
        bool IsAcknowledged(ushort[] singleEvent);
        void SaveToTextFile(ushort fieldNumber);
    }
}
