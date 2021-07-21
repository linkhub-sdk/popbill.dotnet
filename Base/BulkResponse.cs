using System;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class BulkResponse
    {
        [DataMember]
        public long code;
        [DataMember]
        public String message;
        [DataMember]
        public String receiptID;
    }
}