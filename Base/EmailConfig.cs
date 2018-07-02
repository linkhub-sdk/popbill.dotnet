using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class EmailConfig
    {

        [DataMember]
        public String emailType;
        [DataMember]
        public bool? sendYN;
    }
}
