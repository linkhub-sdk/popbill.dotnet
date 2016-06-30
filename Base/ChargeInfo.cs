using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class ChargeInfo
    {
        [DataMember]
        public String unitCost;
        [DataMember]
        public String chargeMethod;
        [DataMember]
        public String rateSystem;
    }
}
