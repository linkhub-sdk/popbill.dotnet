using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class STMTSearchResult
    {
        [DataMember]
        public int code;
        [DataMember]
        public int total;
        [DataMember]
        public int perPage;
        [DataMember]
        public int pageNum;
        [DataMember]
        public int pageCount;
        [DataMember]
        public String message;
        [DataMember]
        public List<StatementInfo> list;
    }
}
