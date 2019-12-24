using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.EasyFin
{
    public class EasyFinBankSummary
    {
        [DataMember]
        public long? count;

        [DataMember]
        public long? cntAccIn;

        [DataMember]
        public long? cntAccOut;

        [DataMember]
        public long? totalAccIn;

        [DataMember]
        public long? totalAccOut;
    }
}