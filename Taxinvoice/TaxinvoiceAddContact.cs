using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class TaxinvoiceAddContact
    {
        [DataMember]
        public int? serialNum;
        [DataMember]
        public String email;
        [DataMember]
        public String contactName;
    }
}
