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
        [DataMember]
        public int? state;
        [DataMember]
        public int? result;
        [DataMember]
        public String title;
        [DataMember]
        public int? sendState;
        [DataMember]
        public int? convState;
        [DataMember]
        public string sendNum;
        [DataMember]
        public string senderName;
        [DataMember]
        public string receiveNum;
        [DataMember]
        public string receiveName;
        [DataMember]
        public int? sendPageCnt;
        [DataMember]
        public int? successPageCnt;
        [DataMember]
        public int? failPageCnt;
        [DataMember]
        public int? refundPageCnt;
        [DataMember]
        public int? cancelPageCnt;
        [DataMember]
        public String reserveDT;
        [DataMember]
        public String sendDT;
        [DataMember]
        public String resultDT;
        [DataMember]
        public int? sendResult;
        [DataMember]
        public string[] fileNames;
        [DataMember]
        public String receiptDT;
        [DataMember]
        public String requestNum;
        [DataMember]
        public String receiptNum;

        /* 과금페이지수, 변환파일용량
         * 2018-10-02 
         */
        [DataMember]
        public String chargePageCnt;
        [DataMember]
        public String tiffFileSize;
    }
}
