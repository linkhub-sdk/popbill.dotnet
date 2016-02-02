using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    [DataContract]
    public class AutoDeny
    {
        [DataMember]
        public string number;
        [DataMember]
        public string regDT;
    }
}
