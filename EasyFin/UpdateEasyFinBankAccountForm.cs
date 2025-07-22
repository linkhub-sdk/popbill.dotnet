using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.EasyFin
{
    [DataContract]
    public class UpdateEasyFinBankAccountForm
    {

        [DataMember]
        public String AccountPWD;

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
    }

}
