﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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

        public Response CheckSenderNumber(String CorpNum, String SenderNumber)
        {
            return CheckSenderNumber(CorpNum, SenderNumber, null);
        }

        public Response CheckSenderNumber(String CorpNum, String SenderNumber, String UserID)
        {
            if (SenderNumber == "") throw new PopbillException(-99999999, "발신번호가 입력되지 않았습니다.");

            return httpget<Response>("/KakaoTalk/CheckSenderNumber/" + SenderNumber, CorpNum, UserID);
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
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호가 입력되지 않았습니다.");

            return httpget<Response>("/KakaoTalk/Cancel/" + requestNum, CorpNum, UserID);
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

            String uri = "/KakaoTalk/Search";
            uri += "?SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);

            if (Item != null) uri += "&Item=" + String.Join(",", Item);
            if (ReserveYN != null && ReserveYN != "") uri += "&ReserveYN=" + ReserveYN;
            if (SenderYN != null)
            {
                if ((bool)SenderYN)
                    uri += "&SenderOnly=1";
                else
                    uri += "&SenderOnly=0";
            }
            if (Order != null && Order != "") uri += "&Order=" + Order;
            if (Page > 0) uri += "&Page=" + Page.ToString();
            if (PerPage > 0 && PerPage <= 1000) uri += "&PerPage=" + PerPage.ToString();
            if (QString != null && QString != "") uri += "&QString=" + HttpUtility.UrlEncode(QString);

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
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호가 입력되지 않았습니다.");

            return httpget<KakaoSentResult>("/KakaoTalk/Get/" + requestNum, CorpNum, UserID);
        }

        ///////////////////////////////// 알림톡 단건전송 /////////////////////////////////

        // 알림톡 단건전송
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

            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, messages, null, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg, List<KakaoButton> buttons)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, messages, null, null, buttons);
        }

        // 전송요청번호 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg, String requestNum)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, messages, null, requestNum, null);
        }

        // 버튼, 전송요청번호 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg, String requestNum, List<KakaoButton> buttons)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, messages, null, requestNum, buttons);
        }

        // 버튼, 전송요청번호, 대체문자제목 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSubject, String altSendType, DateTime? sndDT, String receiveNum, String receiveName, String msg, String altmsg, String requestNum, List<KakaoButton> buttons)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver messageObj = new KakaoReceiver();
            messageObj.rcv = receiveNum;
            messageObj.rcvnm = receiveName;
            messageObj.msg = msg;
            messageObj.altsjt = altSubject;
            messageObj.altmsg = altmsg;

            messages.Add(messageObj);

            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, messages, null, requestNum, buttons);
        }

        ///////////////////////////////// 알림톡 대량전송 /////////////////////////////////

        // 알림톡 대량전송
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, receivers, null, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, receivers, null, null, buttons);
        }

        // UserID 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, receivers, UserID, null, null);
        }

        // 버튼, UserID 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, receivers, UserID, null, buttons);
        }

        // UserID, 전송요청번호 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, receivers, UserID, requestNum, null);
        }

        // 버튼, UserID, 전송요청번호 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, null, null, null, altSendType, sndDT, receivers, UserID, requestNum, buttons);
        }


        ///////////////////////////////// 알림톡 동보전송 /////////////////////////////////

        // 알림톡 동보전송
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers)
        {
            return SendATS(CorpNum, templateCode, snd, content, null, altContent, altSendType, sndDT, receivers, null, null, null);
        }

        // 버튼 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, content, null, altContent, altSendType, sndDT, receivers, null, null, buttons);
        }

        // UserID 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID)
        {
            return SendATS(CorpNum, templateCode, snd, content, null, altContent, altSendType, sndDT, receivers, UserID, null, null);
        }

        // 버튼, UserID 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, content, null, altContent, altSendType, sndDT, receivers, UserID, null, buttons);
        }

        // UserID, 전송요청번호 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum)
        {
            return SendATS(CorpNum, templateCode, snd, content, null, altContent, altSendType, sndDT, receivers, UserID, requestNum, null);
        }

        // 버튼, UserID, 전송요청번호 추가
        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum, List<KakaoButton> buttons)
        {
            return SendATS(CorpNum, templateCode, snd, content, null, altContent, altSendType, sndDT, receivers, UserID, requestNum, buttons);
        }

        public String SendATS(String CorpNum, String templateCode, String snd, String content, String altSubject, String altContent, String altSendType, DateTime? sndDT, List<KakaoReceiver> receivers, String UserID, String requestNum, List<KakaoButton> buttons)
        {            
            ATSSendRequest request = new ATSSendRequest();

            request.templateCode = templateCode;
            request.snd = snd;
            request.content = content;
            request.altSubject = altSubject;
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




        ///////////////////////////////// 친구톡 단건전송 /////////////////////////////////

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

            return SendFTS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, null, null);
        }

        // UserID 추가
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

            return SendFTS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, UserID, null);
        }

        // UserID, 전송요청번호 추가
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

            return SendFTS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, UserID, requestNum);
        }

        // UserID, 전송요청번호, 대체문자제목 추가
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altSubject, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons, String UserID, String requestNum)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altsjt = altSubject;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFTS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, UserID, requestNum);
        }

        ///////////////////////////////// 친구톡 대량전송 /////////////////////////////////

        // 친구톡 대량전송
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendFTS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, receivers, buttons, null, null);
        }

        // UserID 추가
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID)
        {
            return SendFTS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, receivers, buttons, UserID, null);
        }

        // UserID, 전송요청번호 추가
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID, String requestNum)
        {
            return SendFTS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, receivers, buttons, UserID, requestNum);
        }

        ///////////////////////////////// 친구톡 동보전송 /////////////////////////////////

        // 친구톡 동보전송
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons)
        {
            return SendFTS(CorpNum, plusFriendID, snd, content, null, altContent, altSendType, adsYN, sndDT, receivers, buttons, null, null);
        }

        // UserID 추가
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID)
        {
            return SendFTS(CorpNum, plusFriendID, snd, content, null, altContent, altSendType, adsYN, sndDT, receivers, buttons, UserID, null);
        }

        // UserID, 전송요청번호 추가
        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID, String requestNum)
        {
            return SendFTS(CorpNum, plusFriendID, snd, content, null, altContent, altSendType, adsYN, sndDT, receivers, buttons, UserID, requestNum);
        }

        public String SendFTS(String CorpNum, String plusFriendID, String snd, String content, String altSubject, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String UserID, String requestNum)
        {
            if (String.IsNullOrEmpty(plusFriendID)) throw new PopbillException(-99999999, "검색용 아이디가 입력되지 않았습니다.");
            
            FTSSendRequest request = new FTSSendRequest();

            request.plusFriendID = plusFriendID;
            request.snd = snd;
            request.content = content;
            request.altSubject = altSubject;
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




        ///////////////////////////////// 친구톡 이미지 단건전송 /////////////////////////////////

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

            return SendFMS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, fmsfilepath, imageURL, null, null);
        }

        // UserID 추가
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

            return SendFMS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, fmsfilepath, imageURL, UserID, null);
        }

        // UserID, 전송요청번호 추가
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

            return SendFMS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, fmsfilepath, imageURL, UserID, requestNum);
        }

        // UserID, 전송요청번호, 대체문자제목 추가
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altSubject, String altContent, String altSendType, String receiverNum, String receiverName, bool adsYN, DateTime? sndDT,
            List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID, String requestNum)
        {
            List<KakaoReceiver> messages = new List<KakaoReceiver>();
            KakaoReceiver receiver = new KakaoReceiver();
            receiver.rcv = receiverNum;
            receiver.rcvnm = receiverName;
            receiver.msg = content;
            receiver.altsjt = altSubject;
            receiver.altmsg = altContent;

            messages.Add(receiver);

            return SendFMS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, messages, buttons, fmsfilepath, imageURL, UserID, requestNum);
        }

        ///////////////////////////////// 친구톡 이미지 대량전송 /////////////////////////////////

        // 친구톡 이미지 대량전송
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL)
        {
            return SendFMS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, null, null);
        }

        // UserID 추가
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID)
        {
            return SendFMS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, UserID, null);
        }

        // UserID, 전송요청번호 추가
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID, String requestNum)
        {
            return SendFMS(CorpNum, plusFriendID, snd, null, null, null, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, UserID, requestNum);
        }

        ///////////////////////////////// 친구톡 이미지 동보전송 /////////////////////////////////

        // 친구톡 이미지 동보전송
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL)
        {
            return SendFMS(CorpNum, plusFriendID, snd, content, null, altContent, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, null, null);
        }

        // UserID 추가
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID)
        {
            return SendFMS(CorpNum, plusFriendID, snd, content, null, altContent, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, UserID, null);
        }

        //UserID, 전송요청번호 추가
        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID, String requestNum)
        {
            return SendFMS(CorpNum, plusFriendID, snd, content, null, altContent, altSendType, adsYN, sndDT, receivers, buttons, fmsfilepath, imageURL, UserID, requestNum);
        }

        public String SendFMS(String CorpNum, String plusFriendID, String snd, String content, String altSubject, String altContent, String altSendType, bool adsYN, DateTime? sndDT, List<KakaoReceiver> receivers, List<KakaoButton> buttons, String fmsfilepath, String imageURL, String UserID, String requestNum)
        {
            if (String.IsNullOrEmpty(plusFriendID)) throw new PopbillException(-99999999, "검색용 아이디가 입력되지 않았습니다.");

            if (String.IsNullOrEmpty(fmsfilepath)) throw new PopbillException(-99999999, "이미지 파일 경로가 입력되지 않았습니다.");
            
            FTSSendRequest request = new FTSSendRequest();

            request.plusFriendID = plusFriendID;
            request.snd = snd;
            request.content = content;
            request.altSubject = altSubject;
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

        public Response CancelReservebyRCV(String CorpNum, String receiptNum, String receiveNum)
        {
            return CancelReservebyRCV(CorpNum, receiptNum, receiveNum, null);
        }
        
        public Response CancelReservebyRCV(String CorpNum, String receiptNum, String receiveNum, String UserID)
        {
            if (receiptNum == "" || receiptNum == null)
            {
                throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");
            }
            

            CancelReserveRequest request = new CancelReserveRequest();
            request.rcv = receiveNum;


            try
            {
                String PostData = toJsonString(request);
                return httppost<Response>("/KakaoTalk/" + receiptNum + "/Cancel", CorpNum, UserID, PostData, "");
            }
            catch(Exception fe){
                throw new PopbillException(-99999999, fe.Message);
            }
        }

        public Response CancelReserveRNbyRCV(String CorpNum, String requestNum, String receiveNum)
        {
            return CancelReserveRNbyRCV(CorpNum, requestNum, receiveNum, null);
        }
        
        public Response CancelReserveRNbyRCV(String CorpNum, String requestNum, String receiveNum, String UserID)
        {
            if (requestNum == "" || requestNum == null)
            {
                throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");
            }

            CancelReserveRequest request = new CancelReserveRequest();
            request.rcv = receiveNum;

            try
            {
                String PostData = toJsonString(request);
                return httppost<Response>("/KakaoTalk/Cancel/" + requestNum , CorpNum, UserID, PostData, "");
            }
            catch (Exception fe)
            {
                throw new PopbillException(-99999999, fe.Message);
            }
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
            public String altSubject = null;
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
            public String altSubject = null;
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

        [DataContract]
        public class CancelReserveRequest
        {
            [DataMember]
            public String rcv;
        }

    }
}
