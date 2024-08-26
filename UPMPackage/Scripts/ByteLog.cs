using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBrewSDK
{
    [Serializable]
    public class ByteLog
    {
        public string category;
        public Dictionary<string, string> externalData;
    }
}
