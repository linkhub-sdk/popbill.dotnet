using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTCashbill
    {
        [DataMember]
        public string ntsconfirmNum;
        /*
         * 거래일자 추가
         * -2018/10/02
         */
        [DataMember]
        public string tradeDate;
        [DataMember]
        public string tradeDT;
        [DataMember]
        public string tradeType;
        [DataMember]
        public string tradeUsage;
        [DataMember]
        public string totalAmount;
        [DataMember]
        public string supplyCost;
        [DataMember]
        public string tax;
        [DataMember]
        public string serviceFee;
        /*
         * 매입/매출 구분 추가
         * -2017/08/28
         */
        [DataMember]
        public string invoiceType;

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