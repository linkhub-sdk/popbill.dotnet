using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTCashbill
    {
        [DataMember]
        public string ntsconfirmNum;
        [DataMember]
        public string tradeDT;
        [DataMember]
        public string tradeUsage;
        [DataMember]
        public string tradeType;
        [DataMember]
        public string supplyCost;
        [DataMember]
        public string tax;
        [DataMember]
        public string serviceFee;
        [DataMember]
        public string totalAmount;

        [DataMember]
        public string franchiseCorpNum;
        [DataMember]
        public string franchiseCorpName;
        [DataMember]
        public string franchiseCorpType;
        [DataMember]
        public string identityNum;
        [DataMember]
        public string identityNumType;
        [DataMember]
        public string customerName;
        [DataMember]
        public string cardOwnerName;
        [DataMember]
        public string deductionType;
    }

}