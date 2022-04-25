using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class TaxinvoiceCertificate
    {
        [DataMember]
        public String regDT;
        [DataMember]
        public String expireDT;
        [DataMember]
        public String issuerDN;
        [DataMember]
        public String subjectDN;
        [DataMember]
        public String issuerName;
        [DataMember]
        public String oid;
        [DataMember]
        public String regContactName;
        [DataMember]
        public String regContactID;
    }
}
