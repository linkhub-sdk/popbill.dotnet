using System;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;


namespace Popbill.HomeTax
{
    [DataContract]
    public class HTCashbillSearch
    {
        [DataMember]
        public int? code;
        [DataMember]
        public int? total;
        [DataMember]
        public int? perPage;
        [DataMember]
        public int? pageNum;
        [DataMember]
        public int? pageCount;
        [DataMember]
        public String message;
        [DataMember]
        public List<HTCashbill> list;
    }
}