using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapHelper
{
    class DataTypes
    {
       public string outputData;

        public DataTypes(string type)
        {
            this.outputData=convertDataType(type);
        }

        String convertDataType(string inputType)
        {
            switch(inputType)
            {
                case "AN": return "JString";
                case "N0": return "JInteger";
                case "ID": return "JMappedSet";
                case "DT": return "JDate";
                case "R": return "JDouble";
                case "N2": return "JDouble";
                default: return "JString;";
            }
        
        }
    }
}
