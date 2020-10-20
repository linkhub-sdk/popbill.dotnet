using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Kakao
{
    [DataContract]
    public class KakaoReceiver
    {
        [DataMember]
        public string rcv;
        [DataMember]
        public string rcvnm;
        [DataMember]
        public string msg;
        [DataMember]
        public string altmsg;
        [DataMember]
        public string interOPRefKey;
        [DataMember]
        public List<KakaoButton> btns;
    }
}
