using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class StatementLog
    {
        [DataMember]
        public int docLogType;
        [DataMember]
        public String log;
        [DataMember]
        public String procType;
        [DataMember]
        public String procName;
        [DataMember]
        public String procCorpName;
        [DataMember]
        public String procContactName;
        [DataMember]
        public String procMemo;
        [DataMember]
        public String regDT;
        [DataMember]
        public String ip;
    }
}
