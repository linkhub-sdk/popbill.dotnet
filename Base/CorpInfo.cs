using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class CorpInfo
    {
        [DataMember]
        public String ceoname;
        [DataMember]
        public String corpName;
        [DataMember]
        public String addr;
        [DataMember]
        public String bizType;
        [DataMember]
        public String bizClass;
    }
}
