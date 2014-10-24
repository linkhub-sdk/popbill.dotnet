using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Fax
{
    [DataContract]
    public class FaxReceiver
    {
        [DataMember(Name = "rcv")]
        public string receiveNum;
        [DataMember(Name = "rcvnm")]
        public string receiveName;
    }
}
