using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
    [DataContract]
    public class Cashbill
    {
        [DataMember]
        public string memo;
        [DataMember]
        public string mgtKey;
        [DataMember]
        public string tradeDate;
        [DataMember]
        public string tradeUsage;
        [DataMember]
        public string tradeOpt;
        [DataMember]
        public string tradeType;

        [DataMember]
        public string taxationType;
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
        public string franchiseCEOName;
        [DataMember]
        public string franchiseAddr;
        [DataMember]
        public string franchiseTEL;

        [DataMember]
        public string identityNum;
        [DataMember]
        public string customerName;
        [DataMember]
        public string itemName;
        [DataMember]
        public string orderNumber;

        [DataMember]
        public string email;
        [DataMember]
        public string hp;
        [DataMember]
        public string fax;
        [DataMember]
        public bool? smssendYN;
        [DataMember]
        public bool? faxsendYN;

        [DataMember]
        public string confirmNum;

        [DataMember]
        public string orgConfirmNum;

        [DataMember]
        public string orgTradeDate;

        [DataMember]
        public int cancelType;

        [DataMember]
        public string emailSubject;
    }
}
