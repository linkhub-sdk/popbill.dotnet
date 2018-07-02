using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class StatementEmailConfig
    {
        [DataMember]
        public String emailType;
        [DataMember]
        public bool? sendYN;
    }
}
