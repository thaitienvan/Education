using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEducation.Model
{
    public class MessageResult
    {
        public int ERRORCODE { get; set; }
        public string ERRORDESC { get; set; }
        public Object DATA { get; set; }
    }
}
