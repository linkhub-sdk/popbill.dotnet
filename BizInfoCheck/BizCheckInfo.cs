using System;
using System.Runtime.Serialization;

namespace Popbill.BizInfoCheck
{
    [DataContract]
    public class BizCheckInfo
    {
        [DataMember]
        public string corpNum;

        [DataMember]
        public string checkDT;

        [DataMember]
        public string corpName;

        [DataMember]
        public int? corpCode;

        [DataMember]
        public int? corpScaleCode;

        [DataMember]
        public int? personCorpCode;

        [DataMember]
        public int? headOfficeCode;

        [DataMember]
        public string industryCode;

        [DataMember]
        public string companyRegNum;

        [DataMember]
        public string establishDate;

        [DataMember]
        public int? establishCode;

        [DataMember]
        public string ceoname;

        [DataMember]
        public int? workPlaceCode;

        [DataMember]
        public int? addrCode;

        [DataMember]
        public string zipCode;

        [DataMember]
        public string addr;

        [DataMember]
        public string addrDetail;

        [DataMember]
        public string enAddr;

        [DataMember]
        public string bizClass;

        [DataMember]
        public string bizType;

        [DataMember]
        public int? result;

        [DataMember]
        public string resultMessage;

        [DataMember]
        public int? closeDownTaxType;

        [DataMember]
        public string closeDownTaxTypeDate;

        [DataMember]
        public int? closeDownState;

        [DataMember]
        public string closeDownStateDate;

    }
}