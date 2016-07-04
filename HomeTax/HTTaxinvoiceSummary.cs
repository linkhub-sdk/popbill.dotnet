using System;
using System.Text;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTTaxinvoiceSummary
    {
        [DataMember]
        public int? count;
        [DataMember]
        public int? supplyCostTotal;
        [DataMember]
        public int? taxTotal;
        [DataMember]
        public int? amountTotal;
    }
}
