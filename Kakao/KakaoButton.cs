using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Kakao
{
    [DataContract]
    public class KakaoButton
    {
        [DataMember]
        public string n;
        [DataMember]
        public string t;
        [DataMember]
        public string u1;
        [DataMember]
        public string u2;
    }
}
