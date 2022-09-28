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
        public string corpCode;

        [DataMember]
        public string corpScaleCode;

        [DataMember]
        public string personCorpCode;

        [DataMember]
        public string headOfficeCode;

        [DataMember]
        public string industryCode;

        [DataMember]
        public string companyRegNum;

        [DataMember]
        public string establishDate;

        [DataMember]
        public string establishCode;

        [DataMember]
        public string ceoname;

        [DataMember]
        public string workPlaceCode;

        [DataMember]
        public string addrCode;

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
        public string result;

        [DataMember]
        public string resultMessage;

        [DataMember]
        public string closeDownTaxType;

        [DataMember]
        public string closeDownTaxTypeDate;

        [DataMember]
        public string closeDownState;

        [DataMember]
        public string closeDownStateDate;

    }
}
