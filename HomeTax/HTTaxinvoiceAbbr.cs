using System;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTTaxinvoiceAbbr
    {
        [DataMember] public string ntsconfirmNum;
        [DataMember] public string writeDate;
        [DataMember] public string issueDate;
        [DataMember] public string sendDate;
        [DataMember] public string taxType;
        [DataMember] public string purposeType;
        [DataMember] public string supplyCostTotal;
        [DataMember] public string taxTotal;
        [DataMember] public string totalAmount;
        [DataMember] public string remark1;

        [DataMember] public bool? modifyYN;
        [DataMember] public string orgNTSConfirmNum;

        [DataMember] public string purchaseDate;
        [DataMember] public string itemName;
        [DataMember] public string spec;
        [DataMember] public string qty;
        [DataMember] public string unitCost;
        [DataMember] public string supplyCost;
        [DataMember] public string tax;
        [DataMember] public string remark;
        
        [DataMember] public string invoicerCorpNum;
        [DataMember] public string invoicerTaxRegID;
        [DataMember] public string invoicerCorpName;
        [DataMember] public string invoicerCEOName;
        [DataMember] public string invoicerEmail;
        [DataMember] public string invoiceeCorpNum;
        [DataMember] public string invoiceeType;
        [DataMember] public string invoiceeTaxRegID;
        [DataMember] public string invoiceeCorpName;
        [DataMember] public string invoiceeCEOName;
        [DataMember] public string invoiceeEmail1;
        [DataMember] public string invoiceeEmail2;
        [DataMember] public string trusteeCorpNum;
        [DataMember] public string trusteeTaxRegID;
        [DataMember] public string trusteeCorpName;
        [DataMember] public string trusteeCEOName;
        [DataMember] public string trusteeEmail;

        /*
         * 매입 / 매출 구분필드 추가
         * - 2017/08/28
         */
        [DataMember]
        public string invoiceType;
    }
}