using System;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class BulkTaxinvoiceIssueResult
    {
        [DataMember]
        public String invoicerMgtKey;
        [DataMember]
        public String trusteeMgtKey;
        [DataMember]
        public long code;
        [DataMember]
        public String issueDT;
        [DataMember]
        public String ntsconfirmNum;
    }
}