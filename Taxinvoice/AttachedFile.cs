using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class AttachedFile
    {
        [DataMember]
        public int serialNum;
        [DataMember]
        public String attachedFile;
        [DataMember]
        public String displayName;
        [DataMember]
        public String regDT;
    }
}
