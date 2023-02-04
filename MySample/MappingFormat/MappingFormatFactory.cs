using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySample.MappingFormat
{
    public class MappingFormatFactory
    {
        public IMappingFormat GetMappingFormat(MicomModel model)
        {
            switch (model)
            {
                case MicomModel.P122: return new MappingFormatMicomP122(); 
                case MicomModel.P123: return new MappingFormatMicomP122(); 
                case MicomModel.P922: return new MappingFormatMicomP922(); 
                default: return new MappingFormatMicomP122(); 
            }
        }
    }
}
