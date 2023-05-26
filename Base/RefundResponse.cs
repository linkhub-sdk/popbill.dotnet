using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class RefundResponse
    {
        [DataMember]
        public long code;

        [DataMember]
        public String message;

        [DataMember]
        public String refundCode;
    }
}
