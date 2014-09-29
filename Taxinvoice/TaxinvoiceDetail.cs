using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class TaxinvoiceDetail
    {
        [DataMember] public int? serialNum;
        [DataMember] public String purchaseDT;
        [DataMember] public String itemName;
        [DataMember] public String spec;
        [DataMember] public String qty;
        [DataMember] public String unitCost;
        [DataMember] public String supplyCost;
        [DataMember] public String tax;
        [DataMember] public String remark;
    }
}
