using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class JoinForm
    {
        [DataMember]
        public String LinkID;
        [DataMember]
        public String CorpNum;
        [DataMember]
        public String CEOName;
        [DataMember]
        public String CorpName;
        [DataMember]
        public String Addr;
        [DataMember]
        public String ZipCode;
        [DataMember]
        public String BizType;
        [DataMember]
        public String BizClass;
        [DataMember]
        public String ID;
        [DataMember]
        public String PWD;
        [DataMember]
        public String Password;
        [DataMember]
        public String ContactName;
        [DataMember]
        public String ContactTEL;
        [DataMember]
        public String ContactHP;
        [DataMember]
        public String ContactFAX;
        [DataMember]
        public String ContactEmail;

    }
}
