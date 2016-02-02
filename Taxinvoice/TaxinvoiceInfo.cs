using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class TaxinvoiceInfo
    {
        [DataMember] public String itemKey;
        [DataMember] public String taxType;
        [DataMember] public String writeDate;
        [DataMember] public String regDT;

        [DataMember] public String invoicerCorpName;
        [DataMember] public String invoicerCorpNum;
        [DataMember] public String invoicerMgtKey;
        [DataMember] public String invoiceeCorpName;
        [DataMember] public String invoiceeCorpNum;
        [DataMember] public String invoiceeMgtKey;
        [DataMember] public String trusteeCorpName;
        [DataMember] public String trusteeCorpNum;
        [DataMember] public String trusteeMgtKey;

        [DataMember] public String supplyCostTotal;
        [DataMember] public String taxTotal;
        [DataMember] public String purposeType;
        [DataMember] public int? modifyCode;
        [DataMember] public String issueType;

        [DataMember] public String issueDT;
        [DataMember] public String preIssueDT;

        [DataMember] public int stateCode;
        [DataMember] public String stateDT;
        [DataMember] public bool? lateIssueYN;
        [DataMember] public bool? openYN;
        [DataMember] public String openDT;
        [DataMember] public String ntsresult;
        [DataMember] public String ntsconfirmNum;
        [DataMember] public String ntssendDT;
        [DataMember] public String ntsresultDT;
        [DataMember] public String ntssendErrCode;

        [DataMember] public String stateMemo;
    }
}
