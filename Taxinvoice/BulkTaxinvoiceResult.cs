using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class BulkTaxinvoiceResult
    {
        [DataMember]
        public long code;
        [DataMember]
        public String message;
        [DataMember]
        public String receiptID;
        [DataMember]
        public String receiptDT;
        [DataMember]
        public String submitID;
        [DataMember]
        public long submitCount;
        [DataMember]
        public long successCount;
        [DataMember]
        public long failCount;
        [DataMember]
        public long txState;
        [DataMember]
        public String txStartDT;
        [DataMember]
        public String txEndDT;
        [DataMember]
        public long txResultCode;
        [DataMember]
        public List<BulkTaxinvoiceIssueResult> issueResult;
    }
}