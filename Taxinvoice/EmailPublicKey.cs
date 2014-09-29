using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class EmailPublicKey
    {
        [DataMember]
        public string confirmNum;
        [DataMember]
        public string email;
    }
}
