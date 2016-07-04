using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.HomeTax
{
    public class HTFlatRate
    {
        [DataMember]
        public String referenceID;
        [DataMember]
        public String contractDT;
        [DataMember]
        public String useEndDate;
        [DataMember]
        public int? baseDate;
        [DataMember]
        public int? state;
        [DataMember]
        public bool? closeRequestYN;
        [DataMember]
        public bool? useRestrictYN;
        [DataMember]
        public bool? closeOnExpired;
        [DataMember]
        public bool? unPaidYN;
    }
}
