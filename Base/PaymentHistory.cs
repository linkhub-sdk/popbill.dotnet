using System;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Popbill
{
    [DataContract]
    public class PaymentHistoryResult
    {
        [DataMember]
        public long code;
        
        [DataMember]
        public long total;

        [DataMember]
        public long perPage;

        [DataMember]
        public long pageNum;

        [DataMember]
        public long pageCount;

        [DataMember]
        public List<PaymentHistory> list;
    }
}
