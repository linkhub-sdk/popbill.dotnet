using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Fax
{
    public class FaxService : BaseService
    {
        public FaxService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("160");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/FAX/ChargeInfo", CorpNum, UserID);

            return response;
        }


        public Single GetUnitCost(String CorpNum)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/FAX/UnitCost", CorpNum, null);

            return response.unitCost;
        }

        public String GetURL(String CorpNum, String UserID, String TOGO)
        {
            URLResponse response = httpget<URLResponse>("/FAX/?TG=" + TOGO, CorpNum, UserID);

            return response.url;
        }

        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);
            return SendFAX(CorpNum, sendNum, null, receiveNum, receiveName, filePaths, reserveDT, UserID);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);
            return SendFAX(CorpNum, sendNum, senderName, receiveNum, receiveName, filePaths, reserveDT, UserID);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, String filePath, DateTime? reserveDT, String UserID)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            return SendFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return SendFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID);
        }

        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return SendFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            return SendFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            if (filePaths == null || filePaths.Count == 0) throw new PopbillException(-99999999, "전송할 파일정보가 입력되지 않았습니다.");
            if (receivers == null || receivers.Count == 0) throw new PopbillException(-99999999, "수신처 정보가 입력되지 않았습니다.");

            List<UploadFile> UploadFiles = new List<UploadFile>();

            foreach(String filePath in filePaths)
            {
                UploadFile uf = new UploadFile();

                uf.FieldName = "file";
                uf.FileName = System.IO.Path.GetFileName(filePath);
                uf.FileData = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                UploadFiles.Add(uf);
            }
            sendRequest request = new sendRequest();

            request.snd = sendNum;
            request.sndnm = senderName;
            request.fCnt = filePaths.Count;
            request.sndDT = reserveDT == null ? null : reserveDT.Value.ToString("yyyyMMddHHmmss");
            request.rcvs = receivers;

            String PostData = toJsonString(request);

            ReceiptResponse response;
            response = httppostFile<ReceiptResponse>("/FAX", CorpNum, UserID, PostData, UploadFiles, null);

            return response.receiptNum;
        }

        public List<FaxResult> GetFaxResult(String CorpNum, String receiptNum)
        {
            if (String.IsNullOrEmpty(receiptNum)) throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");

            return httpget<List<FaxResult>>("/FAX/" + receiptNum, CorpNum, null);
        }

        public Response CancelReserve(String CorpNum, String receiptNum, String UserID)
        {
            if (String.IsNullOrEmpty(receiptNum)) throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");

            return httpget<Response>("/FAX/" + receiptNum + "/Cancel", CorpNum, UserID);
        }

        public FAXSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, bool? ReserveYN, bool? SenderOnly, String Order, int Page, int PerPage)
        {
            if (String.IsNullOrEmpty(SDate)) throw new PopbillException(-99999999, "시작일자가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(EDate)) throw new PopbillException(-99999999, "종료일자가 입력되지 않았습니다.");

            String uri = "/FAX/Search";
            uri += "?SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);

            if ((bool)ReserveYN) uri += "&ReserveYN=1";
            if ((bool)SenderOnly) uri += "&SenderOnly=1";

            uri += "&Order=" + Order;
            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();

            return httpget<FAXSearchResult>(uri, CorpNum, null);
        }


        [DataContract]
        private class sendRequest
        {
            [DataMember]
            public String snd = null;
            [DataMember]
            public String sndnm = null;
            [DataMember(IsRequired=false)]
            public String sndDT = null;
            [DataMember]
            public int fCnt = 0;
            [DataMember]
            public List<FaxReceiver> rcvs = null;
        }

        [DataContract]
        public class ReceiptResponse
        {
            [DataMember]
            public String receiptNum;
        }
    }
}
