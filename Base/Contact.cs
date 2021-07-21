using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class Contact
    {
        [DataMember]
        public String id;
        [DataMember]
        public String pwd;
        [DataMember]
        public String Password;
        [DataMember]
        public String personName;
        [DataMember]
        public String tel;
        [DataMember]
        public String hp;
        [DataMember]
        public String fax;
        [DataMember]
        public String email;
        [DataMember]
        public bool? searchAllAllowYN;
        [DataMember]
        public int? searchRole;
        [DataMember]
        public bool? mgrYN;
        [DataMember]
        public String regDT;
        [DataMember]
        public String state;
    }
}
