using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    [DataContract]
    public class SenderNumber
    {
        [DataMember]
        public string number;
        [DataMember]
        public bool? representYN;
        [DataMember]
        public int? state;
        [DataMember]
        public string memo;

    }
}
