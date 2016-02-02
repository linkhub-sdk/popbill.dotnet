using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class StatementInfo
    {
        [DataMember] 
        public int? itemCode;
        [DataMember] 
        public String itemKey;
        [DataMember] 
        public String invoiceNum;
        [DataMember]
        public String mgtKey;
        [DataMember] 
        public int? stateCode;
        [DataMember]
        public String taxType;
        [DataMember]
        public String purposeType;

        [DataMember]
        public String writeDate;

        [DataMember]
        public String senderCorpName;
        [DataMember]
        public String senderCorpNum;
        [DataMember]
        public bool? senderPrintYN;
        [DataMember]
        public String receiverCorpName;
        [DataMember]
        public String receiverCorpNum;
        [DataMember]
        public bool? receiverPrintYN;

        [DataMember]
        public String supplyCostTotal;
        [DataMember]
        public String taxTotal;

        [DataMember]
        public String issueDT;
        [DataMember]
        public String stateDT;
        [DataMember]
        public bool? openYN;
        [DataMember]
        public String openDT;
        [DataMember]
        public String stateMemo;
        [DataMember]
        public String regDT;

    }
}
