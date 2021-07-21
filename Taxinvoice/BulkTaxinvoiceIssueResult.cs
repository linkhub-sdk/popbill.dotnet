using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class BulkTaxinvoiceIssueResult
    {
        [DataMember]
        public string invoicerMgtKey;
        [DataMember]
        public string trusteeMgtKey;
        [DataMember]
        public long code;
        [DataMember]
        public string issueDT;
        [DataMember]
        public string ntsconfirmNum;
    }
}