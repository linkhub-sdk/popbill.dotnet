using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    [DataContract]
    public class MessageState
    {
        [DataMember(Name = "rNum")]
        private string _rNum = null;
        [DataMember(Name = "sn")]
        private string _sn = null;
        [DataMember(Name = "stat")]
        private string _stat = null;
        [DataMember(Name = "rlt")]
        private string _rlt = null;
        [DataMember(Name = "sDT")]
        private string _sDT = null;
        [DataMember(Name = "rDT")]
        private string _rDT = null;
        [DataMember(Name = "net")]
        private string _net = null;

        public string rNum { get { return _rNum; } }
        public string sn { get { return _sn; } }
        public string stat { get { return _stat; } }
        public string rlt { get { return _rlt; } }
        public string sDT { get { return _sDT; } }
        public string rDT { get { return _rDT; } }
        public string net { get { return _net; } }
    }
}
