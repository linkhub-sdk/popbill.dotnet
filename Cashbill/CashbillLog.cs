using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
    [DataContract]
    public class CashbillLog
    {
        [DataMember] public int docLogType;
        [DataMember] public String log;
        [DataMember] public String procType;
        [DataMember] public String procMemo;
        [DataMember] public String regDT;
        [DataMember] public String ip;
    }
}
