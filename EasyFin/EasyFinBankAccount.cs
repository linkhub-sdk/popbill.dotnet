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

    }
}
