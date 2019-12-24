using System;
using System.Text;
using System.Runtime.Serialization;


namespace Popbill.EasyFin
{
    public class EasyFinBankJobState
    {

        [DataMember]
        public string jobID;

        [DataMember]
        public int? jobState;

        [DataMember]
        public string startDate;

        [DataMember]
        public string endDate;

        [DataMember]
        public long? errorCode;

        [DataMember]
        public string errorReason;

        [DataMember]
        public string jobStartDT;

        [DataMember]
        public string jobEndDT;

        [DataMember]
        public string regDT;

    }
}
