using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
    [DataContract]
    public class BulkCashbillSubmit
    {
        [DataMember]
        public List<Cashbill> cashbills;
    }
}