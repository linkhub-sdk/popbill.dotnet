using System;
using System.Runtime.Serialization;

namespace Popbill.Closedown
{
    [DataContract]
    public class CorpState
    {
        [DataMember]
        public String corpNum;
        [DataMember]
        public String type;
        [DataMember]
        public String state;
        [DataMember]
        public String stateDate;
        [DataMember]
        public String checkDate;

        /*
         * 과세유형 전환일자 - typeDate 필드 추가 (2017/08/16)
         */
        [DataMember]
        public String typeDate;

        [DataMember]
        public String taxType;
    }
}
