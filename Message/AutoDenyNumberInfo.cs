using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    [DataContract]
    public class AutoDenyNumberInfo
    {
        [DataMember]
        public string smsdenyNumber;
        [DataMember]
        public string regDT;
    }
}
