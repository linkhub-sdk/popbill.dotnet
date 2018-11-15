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

        public String GetSentListURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/FAX/?TG=BOX", CorpNum, UserID);

            return response.url;
        }

        public String GetSenderNumberMgtURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/FAX/?TG=SENDER", CorpNum, UserID);

            return response.url;
        }

        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID, String title)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, title, null);
        }



        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID, bool adsYN)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID, bool adsYN, String title)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, null);
        }


        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID, bool adsYN, String title, String requestNum)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, requestNum);
        }
        

        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, false, null, null); 
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID, String title)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, false, title, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID, bool adsYN)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, adsYN, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String filePath, DateTime? reserveDT, String UserID, bool adsYN, String title)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, adsYN, title, null);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, String filePath, DateTime? reserveDT, String UserID)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, String filePath, DateTime? reserveDT, String UserID, String title)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, title, null);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, String filePath, DateTime? reserveDT, String UserID, bool adsYN, String title, String requestNum)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, requestNum);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, String filePath, DateTime? reserveDT, String UserID, bool adsYN, String title)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, null);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, String filePath, DateTime? reserveDT, String UserID, bool adsYN)
        {
            List<String> filePaths = new List<string>();
            filePaths.Add(filePath);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, null, null);
        }


        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID, String title)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, false, title, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, false, null, null);
        }


        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, adsYN, null, null);
        }


        public string SendFAX(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN, String title)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, adsYN, title, null);
        }



        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID, String title)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, title, null);
        }

        
        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, null, null);
        }
        
        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN, String title)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String receiveNum, String receiveName, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN, String title, String requestNum)
        {
            List<FaxReceiver> receivers = new List<FaxReceiver>();
            FaxReceiver receiver = new FaxReceiver();
            receiver.receiveName = receiveName;
            receiver.receiveNum = receiveNum;
            receivers.Add(receiver);

            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, requestNum);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, String title)
        {
            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, false, title, null);
        }



        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN)
        {
            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, null, null);
        }


        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN, String title, String requestNum)
        {
            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, requestNum);
        }

        public string SendFAX(String CorpNum, String sendNum, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN, String title)
        {
            return RequestFAX(CorpNum, sendNum, null, receivers, filePaths, reserveDT, UserID, adsYN, title, null);
        }




        public string SendFAX(String CorpNum, String sendNum, String senderName, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID)
        {
            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, false, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, String title)
        {
            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, false, title, null);
        }


        public string SendFAX(String CorpNum, String sendNum, String senderName, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN)
        {
            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, adsYN, null, null);
        }

        public string SendFAX(String CorpNum, String sendNum, String senderName, List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, bool adsYN, String title)
        {
            return RequestFAX(CorpNum, sendNum, senderName, receivers, filePaths, reserveDT, UserID, adsYN, title, null);
        }



        private string RequestFAX(String CorpNum, String sendNum, String senderName, 
            List<FaxReceiver> receivers, List<String> filePaths, DateTime? reserveDT, String UserID, Boolean adsYN, String title, String requestNum)
        {
            if (filePaths == null || filePaths.Count == 0) throw new PopbillException(-99999999, "전송할 파일정보가 입력되지 않았습니다.");
            if (receivers == null || receivers.Count == 0) throw new PopbillException(-99999999, "수신처 정보가 입력되지 않았습니다.");

            List<UploadFile> UploadFiles = new List<UploadFile>();

            foreach (String filePath in filePaths)
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
            request.requestNum = requestNum;
            request.fCnt = filePaths.Count;
            request.sndDT = reserveDT == null ? null : reserveDT.Value.ToString("yyyyMMddHHmmss");

            if (adsYN) request.adsYN = adsYN;

            request.rcvs = receivers;

            request.title = title;

            String PostData = toJsonString(request);

            ReceiptResponse response;
            response = httppostFile<ReceiptResponse>("/FAX", CorpNum, UserID, PostData, UploadFiles, null);

            return response.receiptNum;
        }

        public string ResendFAX(String CorpNum, String receiptNum, String sendNum, String senderName, String receiveNum, String receiveName, DateTime? reserveDT, String UserID)
        {
            List<FaxReceiver> receivers = null;

            if ((receiveNum.Length != 0) && (receiveName.Length != 0))
            {
                receivers = new List<FaxReceiver>();
                FaxReceiver receiver = new FaxReceiver();
                receiver.receiveName = receiveName;
                receiver.receiveNum = receiveNum;
                receivers.Add(receiver);
            }
            
            return ResendFAX(CorpNum, receiptNum, sendNum, senderName, receivers, reserveDT, UserID, null);
        }

        public string ResendFAX(String CorpNum, String receiptNum, String sendNum, String senderName, String receiveNum, String receiveName, DateTime? reserveDT, String UserID, String title)
        {
            List<FaxReceiver> receivers = null;

            if ((receiveNum.Length != 0) && (receiveName.Length != 0))
            {
                receivers = new List<FaxReceiver>();
                FaxReceiver receiver = new FaxReceiver();
                receiver.receiveName = receiveName;
                receiver.receiveNum = receiveNum;
                receivers.Add(receiver);
            }

            return ResendFAX(CorpNum, receiptNum, sendNum, senderName, receivers, reserveDT, UserID, title, null);
        }

        public string ResendFAX(String CorpNum, String receiptNum, String sendNum, String senderName, List<FaxReceiver> receivers, DateTime? reserveDT, String UserID)
        {
            return ResendFAX(CorpNum, receiptNum, sendNum, senderName, receivers, reserveDT, UserID, null, null);
        }


        public string ResendFAX(String CorpNum, String receiptNum, String sendNum, String senderName, List<FaxReceiver> receivers, DateTime? reserveDT, String UserID, String title)
        {
            return ResendFAX(CorpNum, receiptNum, sendNum, senderName, receivers, reserveDT, UserID, title, null);
        }


        public string ResendFAX(String CorpNum, String receiptNum, String sendNum, String senderName, List<FaxReceiver> receivers, DateTime? reserveDT, String UserID, String title, String requestNum)
        {
            if (receiptNum == "") throw new PopbillException(-99999999, "팩스접수번호(receiptNum)가 입력되지 않았습니다.");

            sendRequest request = new sendRequest();

            if (sendNum != "") request.snd = sendNum;
            if (senderName != "") request.sndnm = senderName;
            if (title != null) request.title = title;
            if (reserveDT != null) reserveDT.Value.ToString("yyyyMMddHHmmss");
            if (receivers != null) request.rcvs = receivers;
            

            String PostData = toJsonString(request);

            ReceiptResponse response;
            response = httppost<ReceiptResponse>("/FAX/" + receiptNum, CorpNum, UserID, PostData, "");

            return response.receiptNum;
        }



        public string ResendFAXRN(String CorpNum, String requestNum, String assignRequestNum, String sendNum, String senderName, String receiveNum, String receiveName, DateTime? reserveDT, String UserID, String title)
        {
            List<FaxReceiver> receivers = null;

            if ((receiveNum.Length != 0) && (receiveName.Length != 0))
            {
                receivers = new List<FaxReceiver>();
                FaxReceiver receiver = new FaxReceiver();
                receiver.receiveName = receiveName;
                receiver.receiveNum = receiveNum;
                receivers.Add(receiver);
            }

            return ResendFAXRN(CorpNum, requestNum, assignRequestNum, sendNum, senderName, receivers, reserveDT, UserID, title);
        }

        public string ResendFAXRN(String CorpNum, String requestNum, String assignRequestNum, String sendNum, String senderName, List<FaxReceiver> receivers, DateTime? reserveDT, String UserID, String title)
        {
            if (requestNum == "") throw new PopbillException(-99999999, "팩스요청번호(requestNum)가 입력되지 않았습니다.");

            sendRequest request = new sendRequest();

            if (sendNum != "") request.snd = sendNum;
            if (senderName != "") request.sndnm = senderName;
            if (assignRequestNum != "") request.requestNum = assignRequestNum;
            if (title != null) request.title = title;
            if (reserveDT != null) reserveDT.Value.ToString("yyyyMMddHHmmss");
            if (receivers != null) request.rcvs = receivers;


            String PostData = toJsonString(request);

            ReceiptResponse response;
            response = httppost<ReceiptResponse>("/FAX/Resend/" + requestNum, CorpNum, UserID, PostData, "");

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


        public List<FaxResult> GetFaxResultRN(String CorpNum, String requestNum)
        {
            return GetFaxResultRN(CorpNum, requestNum, null);
        }

        public List<FaxResult> GetFaxResultRN(String CorpNum, String requestNum, String UserID)
        {
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호(requestNum)가 입력되지 않았습니다.");

            return httpget<List<FaxResult>>("/FAX/Get/" + requestNum, CorpNum, UserID);
        }

        public Response CancelReserveRN(String CorpNum, String requestNum)
        {
            return CancelReserveRN(CorpNum, requestNum, null);
        }

        public Response CancelReserveRN(String CorpNum, String requestNum, String UserID)
        {
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호(requestNum)가 입력되지 않았습니다.");

            return httpget<Response>("/FAX/Cancel/" + requestNum, CorpNum, UserID);
        }

        public FAXSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, bool? ReserveYN, bool? SenderOnly, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, SDate, EDate, State, ReserveYN, SenderOnly, Order, Page, PerPage, null);

        }
        public FAXSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, bool? ReserveYN, bool? SenderOnly, String Order, int Page, int PerPage, String QString)
        {
            if (String.IsNullOrEmpty(SDate)) throw new PopbillException(-99999999, "시작일자가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(EDate)) throw new PopbillException(-99999999, "종료일자가 입력되지 않았습니다.");

            String uri = "/FAX/Search";
            uri += "?SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);

            if ((bool)ReserveYN) uri += "&ReserveYN=1";
            if ((bool)SenderOnly) uri += "&SenderOnly=1";

            if (QString != null) uri += "&QString=" + QString;

            uri += "&Order=" + Order;
            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();

            return httpget<FAXSearchResult>(uri, CorpNum, null);
        }

        public List<SenderNumber> GetSenderNumberList(String CorpNum)
        {
            return GetSenderNumberList(CorpNum, null);
        }

        public List<SenderNumber> GetSenderNumberList(String CorpNum, String UserID)
        {
            return httpget<List<SenderNumber>>("/FAX/SenderNumber", CorpNum, UserID);
        }

        public String GetPreviewURL(String CorpNum, String receiptNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/FAX/Preview/" + receiptNum, CorpNum, UserID);

            return response.url;
        }

        [DataContract]
        private class sendRequest
        {
            [DataMember]
            public String snd = null;
            [DataMember]
            public String sndnm = null;
            [DataMember]
            public String requestNum = null;
            [DataMember(IsRequired=false)]
            public bool? adsYN = null;
            [DataMember(IsRequired = false)]
            public String sndDT = null;
            [DataMember]
            public int fCnt = 0;
            [DataMember]
            public String title = null;
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
