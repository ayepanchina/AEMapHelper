using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapHelper
{
   public class Segment
    {
        public string Name;
        public List<element> elements;
        public Segment()
        {
            elements = new List<element> ();
        }
    }
    public struct element //структура данных-параметров
    {
        public string number; // содержит имя
        public string Value; // и значение
    }
}
