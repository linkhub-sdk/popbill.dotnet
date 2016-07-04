using System;
using System.Text;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTTaxinvoiceJobState
    {
        [DataMember]
        public string jobID;
        [DataMember]
        public int? jobState;
        [DataMember]
        public string queryType;
        [DataMember]
        public string queryDateType;
        [DataMember]
        public string queryStDate;
        [DataMember]
        public string queryEnDate;
        [DataMember]
        public long? errorCode;
        [DataMember]
        public string errorReason;
        [DataMember]
        public string jobStartDT;
        [DataMember]
        public string jobEndDT;
        [DataMember]
        public long collectCount;
        [DataMember]
        public string regDT;
    }
}
