using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTTaxinvoice
    {
        [DataMember]
        public string writeDate;
        [DataMember]
        public string issueDT;
        [DataMember]
        public int? invoiceType;
        [DataMember]
        public string taxType;
        [DataMember]
        public string taxTotal;
        [DataMember]
        public string supplyCostTotal;
        [DataMember]
        public string totalAmount;
        [DataMember]
        public string purposeType;
        [DataMember]
        public string serialNum;
        [DataMember]
        public string cash;
        [DataMember]
        public string chkBill;
        [DataMember]
        public string credit;
        [DataMember]
        public string note;
        [DataMember]
        public string remark1;
        [DataMember]
        public string remark2;
        [DataMember]
        public string remark3;
        [DataMember]
        public string ntsconfirmNum;

        [DataMember]
        public int? modifyCode;
        [DataMember]
        public string orgNTSConfirmNum;

        [DataMember]
        public string invoicerCorpNum;
        [DataMember]
        public string invoicerMgtKey;
        [DataMember]
        public string invoicerTaxRegID;
        [DataMember]
        public string invoicerCorpName;
        [DataMember]
        public string invoicerCEOName;
        [DataMember]
        public string invoicerAddr;
        [DataMember]
        public string invoicerBizType;
        [DataMember]
        public string invoicerBizClass;
        [DataMember]
        public string invoicerContactName;
        [DataMember]
        public string invoicerDeptName;
        [DataMember]
        public string invoicerTEL;
        [DataMember]
        public string invoicerEmail;


        [DataMember]
        public string invoiceeCorpNum;
        [DataMember]
        public string invoiceeType;
        [DataMember]
        public string invoiceeMgtKey;
        [DataMember]
        public string invoiceeTaxRegID;
        [DataMember]
        public string invoiceeCorpName;
        [DataMember]
        public string invoiceeCEOName;
        [DataMember]
        public string invoiceeAddr;
        [DataMember]
        public string invoiceeBizType;
        [DataMember]
        public string invoiceeBizClass;
        [DataMember]
        public string invoiceeContactName1;
        [DataMember]
        public string invoiceeDeptName1;
        [DataMember]
        public string invoiceeTEL1;
        [DataMember]
        public string invoiceeEmail1;
        [DataMember]
        public string invoiceeContactName2;
        [DataMember]
        public string invoiceeDeptName2;
        [DataMember]
        public string invoiceeTEL2;
        [DataMember]
        public string invoiceeEmail2;


        [DataMember]
        public string trusteeCorpNum;
        [DataMember]
        public string trusteeMgtKey;
        [DataMember]
        public string trusteeTaxRegID;
        [DataMember]
        public string trusteeCorpName;
        [DataMember]
        public string trusteeCEOName;
        [DataMember]
        public string trusteeAddr;
        [DataMember]
        public string trusteeBizType;
        [DataMember]
        public string trusteeBizClass;
        [DataMember]
        public string trusteeContactName;
        [DataMember]
        public string trusteeDeptName;
        [DataMember]
        public string trusteeTEL;
        [DataMember]
        public string trusteeEmail;


        [DataMember]
        public List<HTTaxinvoiceDetail> detailList;


    }
}