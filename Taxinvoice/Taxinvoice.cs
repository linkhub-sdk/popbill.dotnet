using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class Taxinvoice
    {
        [DataMember] public int closeDownState;
        [DataMember] public String closeDownStateDate;

        [DataMember] public String memo;
        [DataMember] public String emailSubject;
        [DataMember] public String dealInvoiceMgtKey;
        [DataMember] public bool? forceIssue;
        [DataMember] public bool? writeSpecification;

        [DataMember]
        public String writeDate;
        [DataMember]
        public String chargeDirection;
        [DataMember] 
        public String issueType;
        [DataMember] 
        public String issueTiming;
        [DataMember] public String taxType;
        [DataMember] public String invoicerCorpNum;
        [DataMember] public String invoicerMgtKey;
        [DataMember] public String invoicerTaxRegID;
        [DataMember] public String invoicerCorpName;
        [DataMember] public String invoicerCEOName;
        [DataMember] public String invoicerAddr;
        [DataMember] public String invoicerBizClass;
        [DataMember] public String invoicerBizType;
        [DataMember] public String invoicerContactName;
        [DataMember] public String invoicerDeptName;
        [DataMember] public String invoicerTEL;
        [DataMember] public String invoicerHP;
        [DataMember] public String invoicerEmail;
        [DataMember] public bool? invoicerSMSSendYN;
        [DataMember] public String invoiceeCorpNum;
        [DataMember] public String invoiceeType;
        [DataMember] public String invoiceeMgtKey;
        [DataMember] public String invoiceeTaxRegID;
        [DataMember] public String invoiceeCorpName;
        [DataMember] public String invoiceeCEOName;
        [DataMember] public String invoiceeAddr;
        [DataMember] public String invoiceeBizType;
        [DataMember] public String invoiceeBizClass;
        [DataMember] public String invoiceeContactName1;
        [DataMember] public String invoiceeDeptName1;
        [DataMember] public String invoiceeTEL1;
        [DataMember] public String invoiceeHP1;
        [DataMember] public String invoiceeEmail1;
        [DataMember] public String invoiceeContactName2;
        [DataMember] public String invoiceeDeptName2;
        [DataMember] public String invoiceeTEL2;
        [DataMember] public String invoiceeHP2;
        [DataMember] public String invoiceeEmail2;
        [DataMember]
        public bool? invoiceeSMSSendYN;
        [DataMember] public String trusteeCorpNum;
        [DataMember] public String trusteeMgtKey;
        [DataMember] public String trusteeTaxRegID;
        [DataMember] public String trusteeCorpName;
        [DataMember] public String trusteeCEOName;
        [DataMember] public String trusteeAddr;
        [DataMember] public String trusteeBizType;
        [DataMember] public String trusteeBizClass;
        [DataMember] public String trusteeContactName;
        [DataMember] public String trusteeDeptName;
        [DataMember] public String trusteeTEL;
        [DataMember] public String trusteeHP;
        [DataMember] public String trusteeEmail;
        [DataMember]
        public bool? trusteeSMSSendYN;
        [DataMember] public String taxTotal;
        [DataMember] public String supplyCostTotal;
        [DataMember] public String totalAmount;
        [DataMember] public int? modifyCode;
        [DataMember] public String orgNTSConfirmNum;
        [DataMember] public String purposeType;
        [DataMember] public String serialNum;
        [DataMember] public String cash;
        [DataMember] public String chkBill;
        [DataMember] public String credit;
        [DataMember] public String note;
        [DataMember] public String remark1;
        [DataMember] public String remark2;
        [DataMember] public String remark3;
        [DataMember]
        public int? kwon;
        [DataMember]
        public int? ho;
        [DataMember]
        public bool? businessLicenseYN;
        [DataMember]
        public bool? bankBookYN;
        [DataMember]
        public bool? faxsendYN;
        [DataMember] public String faxreceiveNum;
        [DataMember] public String ntsconfirmNum;
        [DataMember] public List<TaxinvoiceDetail> detailList;
        [DataMember] public List<TaxinvoiceAddContact> addContactList;
        [DataMember] public String originalTaxinvoiceKey;
    }
}
