using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class UseHistory
    {
        [DataMember]
        public String itemCode;

        [DataMember]
        public String txType;

        [DataMember]
        public String txPoint;

        [DataMember]
        public String balance;

        [DataMember]
        public String txDT;

        [DataMember]
        public String userID;

        [DataMember]
        public String userName;
    }
}
