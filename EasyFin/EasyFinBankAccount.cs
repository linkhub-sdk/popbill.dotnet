using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.EasyFin
{
    public class EasyFinBankAccount
    {
        [DataMember]
        public string accountNumber;

        [DataMember]
        public string bankCode;

        [DataMember]
        public string accountName;

        [DataMember]
        public string accountType;

        [DataMember]
        public int? state;

        [DataMember]
        public string regDT;

        [DataMember]
        public string memo;

        [DataMember]
        public string contractDT;

        [DataMember]
        public string useEndDate;
        
        [DataMember]
        public int? baseDate;
        
        [DataMember]
        public int? contractState;
        
        [DataMember]
        public bool? closeRequestYN;
        
        [DataMember]
        public bool? useRestrictYN;
        
        [DataMember]
        public bool? closeOnExpired;
        
        [DataMember]
        public bool? unPaidYN;

    }
}
