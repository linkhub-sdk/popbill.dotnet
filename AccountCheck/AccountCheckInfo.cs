using System;
using System.Runtime.Serialization;

namespace Popbill.AccountCheck
{
    [DataContract]
    public class AccountCheckInfo
    {
        [DataMember]
        public String bankCode;
        [DataMember]
        public String accountNumber;
        [DataMember]
        public String accountName;
        [DataMember]
        public String checkDate;
        [DataMember]
        public String resultCode;
        [DataMember]
        public String resultMessage;
    }
}
