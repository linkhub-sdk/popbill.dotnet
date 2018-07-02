using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class TIEmailConfig
    {

        [DataMember]
        public String emailType;
        [DataMember]
        public bool? sendYN;
    }
}
