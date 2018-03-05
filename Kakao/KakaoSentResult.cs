using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Kakao
{
    [DataContract]
    public class KakaoSentResult
    {
        [DataMember]
        public string contentType;
        [DataMember]
        public string templateCode;
        [DataMember]
        public string plusFriendID;
        [DataMember]
        public string sendNum;
        [DataMember]
        public string altContent;
        [DataMember]
        public string altSendType;
        [DataMember]
        public string reserveDT;
        [DataMember]
        public string adsYN;
        [DataMember]
        public string imageURL;
        [DataMember]
        public string sendCnt;
        [DataMember]
        public string failCnt;
        [DataMember]
        public string altCnt;
        [DataMember]
        public string cancelCnt;
        [DataMember]
        public string successCnt;

        [DataMember]
        public List<KakaoSentDetail> msgs;
        [DataMember]
        public List<KakaoButton> btns;
    }
}
