using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Fax
{
    [DataContract]
    public class FAXSearchResult
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
        public List<FaxResult> list;
    }
}
