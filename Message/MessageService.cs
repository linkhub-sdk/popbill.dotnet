using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    public enum MessageType { SMS, LMS, XMS, MMS };

    public class MessageService : BaseService
    {
        public MessageService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("150");
            this.AddScope("151");
            this.AddScope("152");
        }

        public ChargeInfo GetChargeInfo(String CorpNum, MessageType msgType)
        {
            return GetChargeInfo(CorpNum, msgType, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, MessageType msgType, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/Message/ChargeInfo?Type="+msgType.ToString(), CorpNum, UserID);
            return response;
        }

        public Single GetUnitCost(String CorpNum,MessageType msgType)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/Message/UnitCost?Type=" + msgType.ToString(), CorpNum, null);

            return response.unitCost;
        }

        public String GetURL(String CorpNum, String UserID, String TOGO)
        {
            URLResponse response = httpget<URLResponse>("/Message/?TG=" + TOGO, CorpNum, UserID);

            return response.url;
        }

        public String GetSentListURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/Message/?TG=BOX", CorpNum, UserID);

            return response.url;
        }

        public String GetSenderNumberMgtURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/Message/?TG=SENDER", CorpNum, UserID);

            return response.url;
        }

        public String SendSMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.SMS, CorpNum, sendNum, senderName, null, content, messages, reserveDT, UserID, null, false);
        }

        public String SendSMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.SMS, CorpNum, sendNum, senderName, null, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendSMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, null, false);
        }

        public String SendSMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendSMS(String CorpNum, String sendNum, String content, List<Message> messages, DateTime? reserveDT, String UserID)
        {
            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, null, false);
        }

        public String SendSMS(String CorpNum, String sendNum, String content, List<Message> messages, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendSMS(String CorpNum, List<Message> messages , DateTime? reserveDT, String UserID)
        {
            return sendMessage(MessageType.SMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, null, false);
        }

        public String SendSMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return sendMessage(MessageType.SMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, null, adsYN);
        }



        public String SendSMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.SMS, CorpNum, sendNum, senderName, null, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendSMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.SMS, CorpNum, sendNum, senderName, null, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendSMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendSMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String content, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);
            
            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendSMS(String CorpNum, String sendNum, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum)
        {
            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, requestNum, false);
        }


        public String SendSMS(String CorpNum, String sendNum, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            return sendMessage(MessageType.SMS, CorpNum, sendNum, null, null, content, messages, reserveDT, UserID, requestNum, adsYN);
        }


        public String SendSMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum)
        {
            return sendMessage(MessageType.SMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendSMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            return sendMessage(MessageType.SMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, requestNum, adsYN);
        }








        public String SendLMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, null, false);
        }

        public String SendLMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendLMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, false);
        }

        public String SendLMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendLMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID)
        {
            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, false);
        }

        public String SendLMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, adsYN);
        }


        public String SendLMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID)
        {
            return sendMessage(MessageType.LMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, null, false);
        }

        public String SendLMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return sendMessage(MessageType.LMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, null, adsYN);
        }




        public String SendLMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendLMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendLMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendLMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendLMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum)
        {
            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendLMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            return sendMessage(MessageType.LMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendLMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum)
        {
            return sendMessage(MessageType.LMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendLMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            return sendMessage(MessageType.LMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, requestNum, adsYN);
        }



        public String SendXMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, null, false);
        }

        public String SendXMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendXMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, false);
        }

        public String SendXMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendXMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID)
        {
            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, false);
        }


        public String SendXMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendXMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID)
        {
            return sendMessage(MessageType.XMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, null, false);
        }

        public String SendXMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return sendMessage(MessageType.XMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, null, adsYN);
        }

        public String SendXMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendXMS(String CorpNum, String sendNum, String senderName, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, senderName, subject, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendXMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendXMS(String CorpNum, String sendNum, String receiveNum, String receiveName, String subject, String content, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendXMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum)
        {
            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendXMS(String CorpNum, String sendNum, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            return sendMessage(MessageType.XMS, CorpNum, sendNum, null, subject, content, messages, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendXMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum)
        {
            return sendMessage(MessageType.XMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, requestNum, false);
        }

        public String SendXMS(String CorpNum, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            return sendMessage(MessageType.XMS, CorpNum, null, null, null, null, messages, reserveDT, UserID, requestNum, adsYN);
        }


        public String SendMMS(String CorpNum, String sender, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, null, false);
        }

        public String SendMMS(String CorpNum, String sender, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, null, adsYN);
        }


        public String SendMMS(String CorpNum, String sender, String senderName, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, senderName, subject, content, messages, mmsfilepath, reserveDT, UserID, null, false);
        }

        public String SendMMS(String CorpNum, String sender, String senderName, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, senderName, subject, content, messages, mmsfilepath, reserveDT, UserID, null, adsYN);
        }

        public String SendMMS(String CorpNum, String sender, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID)
        {
            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, null, false);
        }

        public String SendMMS(String CorpNum, String sender, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, null, adsYN);
        }



        public String SendMMS(String CorpNum, String sender, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, requestNum, false);
        }

        public String SendMMS(String CorpNum, String sender, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendMMS(String CorpNum, String sender, String senderName, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, senderName, subject, content, messages, mmsfilepath, reserveDT, UserID, requestNum, false);
        }

        public String SendMMS(String CorpNum, String sender, String senderName, String receiveNum, String receiveName, String subject, String content, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            List<Message> messages = new List<Message>();
            Message msg = new Message();
            msg.receiveNum = receiveNum;
            msg.receiveName = receiveName;
            messages.Add(msg);

            return SendMMS(CorpNum, sender, senderName, subject, content, messages, mmsfilepath, reserveDT, UserID, requestNum, adsYN);
        }

        public String SendMMS(String CorpNum, String sender, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum)
        {
            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, requestNum, false);
        }

        public String SendMMS(String CorpNum, String sender, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            return SendMMS(CorpNum, sender, null, subject, content, messages, mmsfilepath, reserveDT, UserID, requestNum, adsYN);
        }


        public String SendMMS(String CorpNum, String sender, String senderName, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID)
        {
            return SendMMS(CorpNum, sender, senderName, subject, content, messages, mmsfilepath, reserveDT, UserID, null, false);
        }

        public String SendMMS(String CorpNum, String sender, String senderName, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID, Boolean adsYN)
        {
            return SendMMS(CorpNum, sender, senderName, subject, content, messages, mmsfilepath, reserveDT, UserID, null, adsYN);
        }

        public String SendMMS(String CorpNum, String sender, String senderName, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum)
        {
            return SendMMS(CorpNum, sender, senderName, subject, content, messages, mmsfilepath, reserveDT, UserID, requestNum, false);
        }

        public String SendMMS(String CorpNum, String sender, String senderName, String subject, String content, List<Message> messages, String mmsfilepath, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            if (messages == null || messages.Count == 0) throw new PopbillException(-99999999, "전송할 메시지가 입력되지 않았습니다.");

            sendRequest request = new sendRequest();

            request.snd = sender;
            request.sndnm = senderName;
            request.subject = subject;
            request.content = content;
            request.requestNum = requestNum;
            request.sndDT = reserveDT == null ? null : reserveDT.Value.ToString("yyyyMMddHHmmss");
            request.adsYN = adsYN;

            request.msgs = messages;

            String PostData = toJsonString(request);

            List<UploadFile> UploadFiles = new List<UploadFile>();

            UploadFile uf = new UploadFile();

            uf.FieldName = "file";
            uf.FileName = System.IO.Path.GetFileName(mmsfilepath);
            uf.FileData = new FileStream(mmsfilepath, FileMode.Open, FileAccess.Read);

            UploadFiles.Add(uf);

            ReceiptResponse response = httppostFile<ReceiptResponse>("/MMS", CorpNum, UserID, PostData, UploadFiles, null);

            return response.receiptNum;
        }

        public List<MessageResult> GetMessageResult(String CorpNum, String receiptNum)
        {
            if (String.IsNullOrEmpty(receiptNum)) throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");

            return httpget<List<MessageResult>>("/Message/" + receiptNum, CorpNum, null);
        }

        public List<MessageResult> GetMessageResultRN(String CorpNum, String requestNum)
        {
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호(requestNum)가 입력되지 않았습니다.");

            return httpget<List<MessageResult>>("/Message/Get/" + requestNum, CorpNum, null);
        }

        public Response CancelReserve(String CorpNum, String receiptNum)
        {
            return CancelReserve(CorpNum, receiptNum, "");
        }

        public Response CancelReserve(String CorpNum, String receiptNum, String UserID)
        {
            if (String.IsNullOrEmpty(receiptNum)) throw new PopbillException(-99999999, "접수번호가 입력되지 않았습니다.");

            return httpget<Response>("/Message/" + receiptNum + "/Cancel", CorpNum, UserID);
        }

        public Response CancelReserveRN(String CorpNum, String receiptNum)
        {
            return CancelReserveRN(CorpNum, receiptNum, "");
        }

        public Response CancelReserveRN(String CorpNum, String requestNum, String UserID)
        {
            if (String.IsNullOrEmpty(requestNum)) throw new PopbillException(-99999999, "요청번호(requestNum)가 입력되지 않았습니다.");

            return httpget<Response>("/Message/Cancel/"+requestNum, CorpNum, UserID);
        }

        private String sendMessage(MessageType msgType, String CorpNum, String sender, String senderName, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum)
        {
            return sendMessage(msgType, CorpNum, sender, senderName, subject, content, messages, reserveDT, UserID, requestNum, false);
        }


        private String sendMessage(MessageType msgType, String CorpNum, String sender, String senderName, String subject, String content, List<Message> messages, DateTime? reserveDT, String UserID, String requestNum, Boolean adsYN)
        {
            if (messages == null || messages.Count == 0) throw new PopbillException(-99999999, "전송할 메시지가 입력되지 않았습니다.");

            sendRequest request = new sendRequest();

            request.snd = sender;
            request.sndnm = senderName;
            request.subject = subject;
            request.content = content;
            request.requestNum = requestNum;
            request.sndDT = reserveDT == null ? null : reserveDT.Value.ToString("yyyyMMddHHmmss");
            request.adsYN = adsYN;

            request.msgs = messages;
            
            String PostData = toJsonString(request);

            ReceiptResponse response = httppost<ReceiptResponse>("/" + msgType.ToString(), CorpNum, UserID, PostData, null);

            return response.receiptNum;
        }


        public MSGSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, String[] Item, bool? ReserveYN, bool? SenderYN, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, SDate, EDate, State, Item, ReserveYN, SenderYN, Order, Page, PerPage, null);
        }

        public MSGSearchResult Search(String CorpNum, String SDate, String EDate, String[] State, String[] Item, bool? ReserveYN, bool? SenderYN, String Order, int Page, int PerPage, String QString)
        {
            if (String.IsNullOrEmpty(SDate)) throw new PopbillException(-99999999, "시작일자가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(EDate)) throw new PopbillException(-99999999, "종료일자가 입력되지 않았습니다.");


            String uri = "/Message/Search";
            uri += "?SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);
            uri += "&Item=" + String.Join(",", Item);

            if ((bool)ReserveYN) uri += "&ReserveYN=1";
            if ((bool)SenderYN) uri += "&SenderYN=1";
            if (QString != null) uri += "&QString=" + QString;

            uri += "&Order=" + Order;
            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();

            return httpget<MSGSearchResult>(uri, CorpNum, null);
        }

        public List<AutoDeny> GetAutoDenyList(String CorpNum)
        {
            return httpget<List<AutoDeny>>("/Message/Denied", CorpNum, null);
        }

        public List<SenderNumber> GetSenderNumberList(String CorpNum)
        {
            return GetSenderNumberList(CorpNum, null);
        }

        public List<SenderNumber> GetSenderNumberList(String CorpNum, String UserID)
        {
            return httpget<List<SenderNumber>>("/Message/SenderNumber", CorpNum, UserID);
        }

        public List<MessageState> GetStates(String CorpNum, List<String> ReciptNumList, String UserID)
        {
            if (ReciptNumList == null || ReciptNumList.Count == 0) throw new PopbillException(-99999999, "문자전송 접수번호가 입력되지 않았습니다.");
            String PostData = toJsonString(ReciptNumList);
            return httppost<List<MessageState>>("/Message/States", CorpNum, UserID, PostData, null);
        }


        [DataContract]
        private class sendRequest
        {
            [DataMember]
            public String snd = null;
            [DataMember]
            public String sndnm = null;
            [DataMember]
            public String subject = null;
            [DataMember]
            public String content = null;
            [DataMember]
            public String sndDT = null;
            [DataMember]
            public String requestNum = null;
            [DataMember]
            public List<Message> msgs;
            [DataMember]
            public Boolean adsYN = false;
        }

        [DataContract]
        public class ReceiptResponse
        {
            [DataMember]
            public String receiptNum;
        }
    }
}
