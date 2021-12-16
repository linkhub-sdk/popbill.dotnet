using System;
using System.Runtime.Serialization;

namespace Popbill.AccountCheck
{
    [DataContract]
    public class DepositorCheckInfo
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
        public String result;
        [DataMember]
        public String resultMessage;
        [DataMember]
        public String identityNumType;
        [DataMember]
        public String identityNum;
    }
}
