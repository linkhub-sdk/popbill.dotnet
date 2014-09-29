using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class TaxinvoiceLog
    {
        [DataMember] public int docLogType;
        [DataMember] public String log;
        [DataMember] public String procType;
        [DataMember] public String procCorpName;
        [DataMember] public String procContactName;
        [DataMember] public String procMemo;
        [DataMember] public String regDT;
        [DataMember] public String ip;
    }
}
