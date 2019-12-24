using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Popbill.EasyFin
{
    [DataContract]
    public class EasyFinBankSearchDetail
    {
        [DataMember]
        public String tid;

        [DataMember]
        public String trdate;

        [DataMember]
        public long? trserial;

        [DataMember]
        public String trdt;

        [DataMember]
        public String accIn;

        [DataMember]
        public String accOut;

        [DataMember]
        public String balance;

        [DataMember]
        public String remark1;

        [DataMember]
        public String remark2;

        [DataMember]
        public String remark3;

        [DataMember]
        public String remark4;

        [DataMember]
        public String regDT;

        [DataMember]
        public String memo;

    }
}