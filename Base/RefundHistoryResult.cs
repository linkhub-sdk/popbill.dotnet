using System;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Popbill
{
    [DataContract]
    public class RefundHistoryResult
    {
        [DataMember]
        public long code;
        
        [DataMember]
        public int total;

        [DataMember]
        public int perPage;

        [DataMember]
        public int pageNum;

        [DataMember]
        public int pageCount;

        [DataMember]
        public List<RefundHistory> list;
    }
}
