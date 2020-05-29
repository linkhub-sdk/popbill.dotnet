using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.EasyFin
{
    [DataContract]
    public class EasyFinBankAccountForm
    {
        [DataMember]
        public String BankCode;

        [DataMember]
        public String AccountNumber;

        [DataMember]
        public String AccountPWD;

        [DataMember]
        public String AccountType;

        [DataMember]
        public String IdentityNumber;

        [DataMember]
        public String AccountName;

        [DataMember]
        public String BankID;

        [DataMember]
        public String FastID;

        [DataMember]
        public String FastPWD;

        [DataMember]
        public String Memo;

        [DataMember]
        public String UsePeriod;
    }

    
}
