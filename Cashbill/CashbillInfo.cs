using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
    [DataContract]
    public class CashbillInfo
    {
        [DataMember]
        public string itemKey;
        [DataMember]
        public string mgtKey;
        [DataMember]
        public string tradeDate;
        [DataMember]
        public string tradeType;
        [DataMember]
        public string tradeUsage;
        [DataMember]
        public string tradeOpt;
        [DataMember]
        public string taxationType;
        [DataMember]
        public string totalAmount;
        [DataMember]
        public string issueDT;
        [DataMember]
        public string regDT;
        [DataMember]
        public string stateMemo;
        [DataMember]
        public int stateCode;
        [DataMember]
        public string stateDT;

        [DataMember]
        public string identityNum;
        [DataMember]
        public string itemName;
        [DataMember]
        public string customerName;

        [DataMember]
        public string confirmNum;
        [DataMember]
        public string orgConfirmNum;
        [DataMember]
        public string orgTradeDate;
        [DataMember]
        public string ntssendDT;
        [DataMember]
        public string ntsresult;
        [DataMember]
        public string ntsresultDT;
        [DataMember]
        public string ntsresultCode;
        [DataMember]
        public string ntsresultMessage;

        [DataMember]
        public bool? printYN;
    }
}
