using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
    [DataContract]
    public class CBEmailConfig
    {

        [DataMember]
        public String emailType;
        [DataMember]
        public bool? sendYN;
    }
}
