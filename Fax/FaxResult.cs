using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Fax
{
    [DataContract]
    public class FaxResult
    {
        [DataMember(Name = "state")]
        private int? _state = null;
        [DataMember(Name = "result")]
        private int? _result = null;
        [DataMember(Name = "title")]
        private String _title = null;

        [DataMember(Name = "sendState")]
        private int? _sendState = null;
        [DataMember(Name = "convState")]
        private int? _convState = null;
        [DataMember(Name = "sendNum")]
        private string _sendNum = null;
        [DataMember(Name = "senderName")]
        private string _senderName = null;
        [DataMember(Name = "receiveNum")]
        private string _receiveNum = null;
        [DataMember(Name = "receiveName")]
        private string _receiveName = null;
        [DataMember(Name = "sendPageCnt")]
        private int? _sendPageCnt = null;
        [DataMember(Name = "successPageCnt")]
        private int? _successPageCnt = null;
        [DataMember(Name = "failPageCnt")]
        private int? _failPageCnt = null;
        [DataMember(Name = "refundPageCnt")]
        private int? _refundPageCnt = null;
        [DataMember(Name = "cancelPageCnt")]
        private int? _cancelPageCnt = null;
        [DataMember(Name = "reserveDT")]
        private String _reserveDT = null;
        [DataMember(Name = "sendDT")]
        private String _sendDT = null;
        [DataMember(Name = "resultDT")]
        private String _resultDT = null;
        [DataMember(Name = "sendResult")]
        private int? _sendResult = null;
        [DataMember(Name = "fileNames")]
        private string[] _fileNames = null;
        [DataMember(Name = "receiptDT")]
        private String _receiptDT = null;

        [DataMember(Name = "requestNum")]
        private String _requestNum = null;
        [DataMember(Name = "receiptNum")]
        private String _receiptNum = null;

        public int? result { get { return _result; } }
        public int? state { get { return _state; } }
        public string title { get { return _title; } }

        public int? sendState { get { return _sendState; } }
        public int? convState { get { return _convState; } }
        public string sendNum { get { return _sendNum; } }
        public string senderName { get { return _senderName; } }
        public string receiveNum { get { return _receiveNum; } }
        public string receiveName { get { return _receiveName; } }
        public int? sendPageCnt { get { return _sendPageCnt; } }
        public int? successPageCnt { get { return _successPageCnt; } }
        public int? failPageCnt { get { return _failPageCnt; } }
        public int? refundPageCnt { get { return _refundPageCnt; } }
        public int? cancelPageCnt { get { return _cancelPageCnt; } }
        public string reserveDT { get { return _reserveDT; } }
        public string sendDT { get { return _sendDT; } }
        public string resultDT { get { return _resultDT; } }
        public string receiptDT { get { return _receiptDT; } }
        public string[] fileNames { get { return _fileNames; } }
      
        public int? sendResult {get { return _sendResult; } }

        public string requestNum { get { return _requestNum; } }
        public string receiptNUm { get { return _receiptNum; } }
    }
}
