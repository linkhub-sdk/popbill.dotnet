using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class Statement
    {
        [DataMember]
        public String memo;
        [DataMember]
        public String sendNum;
        [DataMember]
        public String receiveNum;
        [DataMember]
        public int? itemCode;
        [DataMember]
        public String mgtKey;
        [DataMember]
        public String invoiceNum;
        [DataMember]
        public String formCode;
        [DataMember]
        public String writeDate;
        [DataMember]
        public String taxType;

        [DataMember]
        public String senderCorpNum;
        [DataMember]
        public String senderTaxRegID;
        [DataMember]
        public String senderCorpName;
        [DataMember]
        public String senderCEOName;
        [DataMember]
        public String senderAddr;
        [DataMember]
        public String senderBizClass;
        [DataMember]
        public String senderBizType;
        [DataMember]
        public String senderContactName;
        [DataMember]
        public String senderDeptName;
        [DataMember]
        public String senderTEL;
        [DataMember]
        public String senderHP;
        [DataMember]
        public String senderEmail;
        [DataMember]
        public String senderFAX;

        [DataMember]
        public String receiverCorpNum;
        [DataMember]
        public String receiverTaxRegID;
        [DataMember]
        public String receiverCorpName;
        [DataMember]
        public String receiverCEOName;
        [DataMember]
        public String receiverAddr;
        [DataMember]
        public String receiverBizClass;
        [DataMember]
        public String receiverBizType;
        [DataMember]
        public String receiverContactName;
        [DataMember]
        public String receiverDeptName;
        [DataMember]
        public String receiverTEL;
        [DataMember]
        public String receiverHP;
        [DataMember]
        public String receiverEmail;
        [DataMember]
        public String receiverFAX;

        [DataMember]
        public String taxTotal;
        [DataMember]
        public String supplyCostTotal;
        [DataMember]
        public String totalAmount;
        [DataMember]
        public String purposeType;
        [DataMember]
        public String serialNum;
        [DataMember]
        public String remark1;
        [DataMember]
        public String remark2;
        [DataMember]
        public String remark3;
        [DataMember]
        public bool? businessLicenseYN;
        [DataMember]
        public bool? bankBookYN;
        [DataMember]
        public bool? faxsendYN;
        [DataMember]
        public bool? smssendYN;
        [DataMember]
        public bool? autoacceptYN;

        [DataMember]
        public String emailSubject;

        [DataMember] public List<StatementDetail> detailList;
        [DataMember] public propertyBag propertyBag;

    }
}
