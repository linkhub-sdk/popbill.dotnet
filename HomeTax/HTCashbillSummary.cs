using System;
using System.Text;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTCashbillSummary
    {
        [DataMember]
        public long? count;
        [DataMember]
        public long? supplyCostTotal;
        [DataMember]
        public long? taxTotal;
        [DataMember]
        public long? amountTotal;
        [DataMember]
        public long? serviceFeeTotal;
    }
}
