using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class StatementDetail
    {
        [DataMember]
        public int? serialNum;
        [DataMember]
        public String purchaseDT;
        [DataMember]
        public String itemName;
        [DataMember]
        public String spec;
        [DataMember]
        public String unit;
        [DataMember]
        public String qty;
        [DataMember]
        public String unitCost;
        [DataMember]
        public String supplyCost;
        [DataMember]
        public String tax;
        [DataMember]
        public String remark;
        [DataMember]
        public String spare1;
        [DataMember]
        public String spare2;
        [DataMember]
        public String spare3;
        [DataMember]
        public String spare4;
        [DataMember]
        public String spare5;
    }
}
