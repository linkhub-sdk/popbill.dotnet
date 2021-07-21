using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Kakao
{
    public enum KakaoType { ATS, FTS, FMS };

    public class KakaoService : BaseService
    { 
        public KakaoService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("153");
            this.AddScope("154");
            this.AddScope("155");
        }

        public ChargeInfo GetChargeInfo(String CorpNum, KakaoType msgType)
        {
            return GetChargeInfo(CorpNum, msgType, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, KakaoType msgType, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/KakaoTalk/ChargeInfo?Type=" + msgType.ToString(), CorpNum, UserID);
            return response;
        }

        public Single GetUnitCost(String CorpNum, KakaoType msgType)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/KakaoTalk/UnitCost?Type=" + msgType.ToString(), CorpNum, null);

            return response.unitCost;
        }

        public String GetURL(String CorpNum, String UserID, String TOGO)
        {
            String uri = "/KakaoTalk/?TG=";

            if (TOGO == "SENDER") uri = "/Message/?TG=";

            URLResponse response = httpget<URLResponse>(uri + TOGO, CorpNum, UserID);

            return response.url;
        }

        public String GetPlusFriendMgtURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/KakaoTalk/?TG=PLUSFRIEND", CorpNum, UserID);

            return response.url;
        }

        public String GetSenderNumberMgtURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/Message/?TG=SENDER", CorpNum, UserID);

            return response.url;
        }

        public String GetATSTemplateMgtURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/KakaoTalk/?TG=TEMPLATE", CorpNum, UserID);

            return response.url;
        }

        public ATSTemplate GetATSTemplate(String CorpNum, String templateCode)
        {
            return GetATSTemplate(CorpNum, templateCode, null);
        }

        public ATSTemplate GetATSTemplate(String CorpNum, String templateCode, String UserID)
        {
            if (string.IsNullOrEmpty(templateCode)) throw new PopbillException(-99999999, "템플릿코드가 입력되지 않았습니다.");

            return httpget<ATSTemplate>("/KakaoTalk/GetATSTemplate/" + templateCode, CorpNum, UserID);
        }

        public String GetSentListURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/KakaoTalk/?TG=BOX", CorpNum, UserID);

            return response.url;
        }

        public List<SenderNumber> GetSenderNumberList(String CorpNum, String UserID)
        {
            return httpget<List<SenderNumber>>("/Message/SenderNumber", CorpNum, UserID);
        }

        public List<ATSTemplate> ListATSTemplate(String CorpNum)
        {
            return ListATSTemplate(CorpNum, null);
        }

        public List<ATSTemplate> ListATSTemplate(String CorpNum, String UserID)
        {
            return httpget<List<ATSTemplate>>("/KakaoTalk/ListATSTemplate", CorpNum, UserID);
        }

        public List<PlusFriend> ListPlusFriendID(String CorpNum)
        {
            return ListPlusFriendID(CorpNum, null);
        }

        public List<PlusFriend> ListPlusFriendID(String CorpNum, String UserID)
        {
            return httpget<List<PlusFriend>>("/KakaoTalk/ListPlusFriendID", CorpNum, UserID);
        }

        public Response CancelReserve(String CorpNum, String receiptNum)
        {
            return CancelReserve(CorpNum, receiptNum, null);
        }

        public Response CancelReserve(String CorpNum, String receiptNum, String UserID)
        {
            if (String.IsNullOrEmpty(receiptNum)) throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");

            return httpget<Response>("/KakaoTalk/" + receiptNum + "/Cancel", CorpNum, UserID);
        }

        public Response CancelReserveRN(String CorpNum, String requestNum)
        {
            return CancelReserveRN(CorpNum, requestNum, null);
        }

        public Response CancelReserveRN(String CorpNum, String requestNum, String UserID)
        {
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호(requestNum)가 입력되지 않았습니다.");

            return httpget<Response>("/KakaoTalk//Cancel/" + requestNum, CorpNum, UserID);
        }


        public KakaoSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, String[] Item, String ReserveYN, bool? SenderYN, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, SDate, EDate, State, Item, ReserveYN, SenderYN, Order, Page, PerPage, null, null);
        }

        public KakaoSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, String[] Item, String ReserveYN, bool? SenderYN, String Order, int Page, int PerPage, String UserID)
        {
            return Search(CorpNum, SDate, EDate, State, Item, ReserveYN, SenderYN, Order, Page, PerPage, UserID, null);
        }

        public KakaoSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, String[] Item, String ReserveYN, bool? SenderYN, String Order, int Page, int PerPage, String UserID, String QString)
        {
            if (String.IsNullOrEmpty(SDate)) throw new PopbillException(-99999999, "시작일자가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(EDate)) throw new PopbillException(-99999999, "종료일자가 입력되지 않았습니다.");


            String uri = "/KakaoTalk/Search";
            uri += "?SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);
            uri += "&Item=" + String.Join(",", Item);

            uri += "&ReserveYN=" + ReserveYN;
            if ((bool)SenderYN) uri += "&SenderYN=1";
            if (QString != null) uri += "&QString=" + QString;

            uri += "&Order=" + Order;
            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();

            return httpget<KakaoSearchResult>(uri, CorpNum, UserID);
        }

        public KakaoSentResult GetMessages(String CorpNum, String receiptNum)
        {
            return GetMessages(CorpNum, receiptNum, null);
        }

        public KakaoSentResult GetMessages(String CorpNum, String receiptNum, String UserID)
        {
            if (String.IsNullOrEmpty(receiptNum)) throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");

            return httpget<KakaoSentResult>("/KakaoTalk/" + receiptNum, CorpNum, UserID);
        }

        public KakaoSentResult GetMessagesRN(String CorpNum, String requestNum)
        {
            return GetMessagesRN(CorpNum, requestNum, null);
        }

        public KakaoSentResult GetMessagesRN(String CorpNum, String requestNum, String UserID)
        {
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호(requestNum)가 입력되지 않았습니다.");

            return httpget<KakaoSentResult>("/KakaoTalk/Get/" + requestNum, CorpNum, UserID);
        }

        /////////////////////////////////////////////// RequestNum 미포함 //////////////////////////////////////////////////////////////
        // 단건 전송
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg)
        {
            if (String.IsNullOrEmpty(receiveNum)) throw new PopbillException(-99999999, "수신번호가 입력되지 않았습니다.");

            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, messages, null, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg, List<KakaoButton> buttons)
        {
            if (String.IsNullOrEmpty(receiveNum)) throw new PopbillException(-99999999, "수신번호가 입력되지 않았습니다.");

            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, messages, null, null, buttons);
        }

        // 대량전송
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, receivers, null, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, receivers, null, null, buttons);
        }

        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, receivers, UserID, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, receivers, UserID, null, buttons);
        }

        // 동보 대량전송
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers)
        {
            return SendATS(CorpNum, templateCode, snd, content, altContent, altSendType, sndDT, receivers, null, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, content, altContent, altSendType, sndDT, receivers, null, null, buttons);
        }

        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID)
        {
            return SendATS(CorpNum, templateCode, snd, content, altContent, altSendType, sndDT, receivers, UserID, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, content, altContent, altSendType, sndDT, receivers, UserID, null, buttons);
        }



        ////////////////////////////////////////  RequestNum 포함 ///////////////////////////////////////////////////////////////////////////////

        // 단건 전송
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg, String requestNum)
        {
            if (String.IsNullOrEmpty(receiveNum)) throw new PopbillException(-99999999, "수신번호가 입력되지 않았습니다.");

            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, messages, null, requestNum, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg, String requestNum, List<KakaoButton> buttons)
        {
            if (String.IsNullOrEmpty(receiveNum)) throw new PopbillException(-99999999, "수신번호가 입력되지 않았습니다.");

            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, messages, null, requestNum, buttons);
        }

        // 대량전송
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, receivers, UserID, requestNum, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, altSendType, sndDT, receivers, UserID, requestNum, buttons);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum)
        {
            return SendATS(CorpNum, templateCode, snd, content, altContent, altSendType, sndDT, receivers, UserID, requestNum, null);
        }

        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum, List<KakaoButton> buttons)
        {
            if (String.IsNullOrEmpty(templateCode)) throw new PopbillException(-99999999, "알림톡 템플릿 코드가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(snd)) throw new PopbillException(-99999999, "발신번호가 입력되지 않았습니다.");

            ATSSendRequest request = new ATSSendRequest();

            request.templateCode = templateCode;
            request.snd = snd;
            request.content = content;
            request.altContent = altContent;
            request.altSendType = altSendType;
            request.sndDT = sndDT == null ? null : sndDT.Value.ToString("yyyyMMddHHmmss");
            request.msgs = receivers;
            request.requestNum = requestNum;
            request.btns = buttons == null ? null : buttons;
            
            String PostData = toJsonString(request);
            ReceiptResponse response = httppost<ReceiptResponse>("/ATS", CorpNum, UserID, PostData, null);

            return response.receiptNum;
        }





        // 친구톡 단건전송
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFTS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, messages, buttons, null);
        }

        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons, String UserID)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFTS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, messages, buttons, UserID, null);
        }

        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons, String UserID, String requestNum)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFTS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, messages, buttons, UserID, requestNum);
        }

        
        // 동일내용 대량전송
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendFTS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, receivers, buttons, null, null);
        }

        public String SendFTS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID)
        {
            return SendFTS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, receivers, buttons, UserID, null);
        }


        public String SendFTS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID, String requestNum)
        {
            return SendFTS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, receivers, buttons, UserID, requestNum);
        }

        // 개별내용 동보전송
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendFTS(CorpNum, plusFriendID, snd, content, altContent, altSendType, adsYN, sndDT, receivers, buttons, null, null);
        }


        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID)
        {
            return SendFTS(CorpNum, plusFriendID, snd, content, altContent, altSendType, adsYN, sndDT, receivers, buttons, UserID, null);
        }

        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID, String requestNum)
        {
            if (String.IsNullOrEmpty(plusFriendID)) throw new PopbillException(-99999999, "플러스친구 아이디가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(snd)) throw new PopbillException(-99999999, "발신번호가 입력되지 않았습니다.");

            FTSSendRequest request = new FTSSendRequest();

            request.plusFriendID = plusFriendID;
            request.snd = snd;
            request.content = content;
            request.altContent = altContent;
            request.altSendType = altSendType;
            request.adsYN = adsYN;
            request.requestNum = requestNum;
            request.sndDT = sndDT == null ? null : sndDT.Value.ToString("yyyyMMddHHmmss");
            request.msgs = receivers;
            request.btns = buttons;

            String PostDate = toJsonString(request);
            ReceiptResponse response = httppost<ReceiptResponse>("/FTS", CorpNum, UserID, PostDate, null);

            return response.receiptNum;
        }




        // 친구톡 이미지 단건전송
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons, String fmsfilepath, String imageURL)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFMS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, messages, buttons, fmsfilepath, imageURL, null);
        }

        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFMS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, messages, buttons, fmsfilepath, imageURL, UserID);
        }


        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID, String requestNum)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFMS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, messages, buttons, fmsfilepath, imageURL, UserID, requestNum);
        }


        // 동일내용 대량전송
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL)
        {
            return SendFMS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, null);
        }

        public String SendFMS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID)
        {
            return SendFMS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, UserID);
        }

        public String SendFMS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID, String requestNum)
        {
            return SendFMS(CorpNum, plusFriendID, snd, null, null, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, UserID, requestNum);
        }

        // 개별내용 동보전송
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL)
        {
            return SendFMS(CorpNum, plusFriendID, snd, content, altContent, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, null);
        }
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID)
        {
            return SendFMS(CorpNum, plusFriendID, snd, content, altContent, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, UserID, null);
        }

        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID, String requestNum)
        {
            if (String.IsNullOrEmpty(plusFriendID)) throw new PopbillException(-99999999, "플러스친구 아이디가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(snd)) throw new PopbillException(-99999999, "발신번호가 입력되지 않았습니다.");

            FTSSendRequest request = new FTSSendRequest();

            request.plusFriendID = plusFriendID;
            request.snd = snd;
            request.content = content;
            request.altContent = altContent;
            request.altSendType = altSendType;
            request.adsYN = adsYN;
            request.requestNum = requestNum;
            request.sndDT = sndDT == null ? null : sndDT.Value.ToString("yyyyMMddHHmmss");
            request.msgs = receivers;
            request.btns = buttons;
            request.imageURL = imageURL;

            String PostDate = toJsonString(request);

            List<UploadFile> UploadFiles = new List<UploadFile>();

            try
            {
                UploadFile uf = new UploadFile();

                uf.FieldName = "file";
                uf.FileName = System.IO.Path.GetFileName(fmsfilepath);
                uf.FileData = new FileStream(fmsfilepath, FileMode.Open, FileAccess.Read);

                UploadFiles.Add(uf);
            }
            catch (Exception fe)
            {
                throw new PopbillException(-99999999, fe.Message);
            }
            
            

            ReceiptResponse response = httppostFile<ReceiptResponse>("/FMS", CorpNum, UserID, PostDate, UploadFiles, null);

            return response.receiptNum;
        }


        [DataContract]
        private class FTSSendRequest
        {
            [DataMember]
            public String plusFriendID = null;
            [DataMember]
            public String snd = null;
            [DataMember]
            public String content = null;
            [DataMember]
            public String altContent = null;
            [DataMember]
            public String altSendType = null;
            [DataMember]
            public String sndDT = null;
            [DataMember]
            public bool? adsYN = false;
            [DataMember]
            public String imageURL = null;
            [DataMember]
            public String requestNum = null;
            [DataMember]
            public List<KakaoButton> btns = null;
            [DataMember]
            public List<KakaoReceiver> msgs = null;
        }


        [DataContract]
        private class ATSSendRequest
        {
            [DataMember]
            public String templateCode = null;
            [DataMember]
            public String snd = null;
            [DataMember]
            public String content = null;
            [DataMember]
            public String altContent = null;
            [DataMember]
            public String altSendType = null;
            [DataMember]
            public String sndDT = null;
            [DataMember]
            public String requestNum = null;
            [DataMember]
            public List<KakaoButton> btns = null;
            [DataMember]
            public List<KakaoReceiver> msgs;
        }

        [DataContract]
        public class ReceiptResponse
        {
            [DataMember]
            public String receiptNum = null;
        }
        


        

    }
}
