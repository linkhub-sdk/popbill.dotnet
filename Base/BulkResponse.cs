using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class BulkResponse
    {
        [DataMember]
        public long code;
        [DataMember]
        public string message;
        [DataMember]
        public string receiptID;
    }
}