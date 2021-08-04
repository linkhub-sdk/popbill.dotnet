using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
 
    public class CashbillService : BaseService
    {
        public CashbillService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("140");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }


        public ChargeInfo GetChargeInfo(String CorpNum, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/Cashbill/ChargeInfo/", CorpNum, UserID);

            return response;
        }

        public Single GetUnitCost(String CorpNum)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/Cashbill?cfg=UNITCOST", CorpNum, null);

            return response.unitCost;
        }

        public String GetURL(String CorpNum, String UserID, String TOGO)
        {
            URLResponse response = httpget<URLResponse>("/Cashbill?TG=" + TOGO, CorpNum, UserID);

            return response.url;
        }

        public bool CheckMgtKeyInUse(String CorpNum, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            try
            {
                CashbillInfo response = httpget<CashbillInfo>("/Cashbill/" + MgtKey, CorpNum, null);

                return string.IsNullOrEmpty(response.itemKey) == false;
            }
            catch (PopbillException pe)
            {
                if (pe.code == -14000003) return false;

                throw pe;
            }

        }

        public Response Register(String CorpNum, Cashbill cashbill)
        {
            return Register(CorpNum, cashbill, null);
        }
        public Response Register(String CorpNum, Cashbill cashbill, String UserID)
        {
            if (cashbill == null) throw new PopbillException(-99999999, "현금영수증 정보가 입력되지 않았습니다.");

            String PostData = toJsonString(cashbill);

            return httppost<Response>("/Cashbill", CorpNum, UserID, PostData, null);
        }

        public Response Update(String CorpNum, String MgtKey, Cashbill cashbill, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            if (cashbill == null) throw new PopbillException(-99999999, "현금영수증 정보가 입력되지 않았습니다.");

            String PostData = toJsonString(cashbill);

            return httppost<Response>("/Cashbill/" + MgtKey , CorpNum, UserID, PostData, "PATCH");
        }

        public Response Delete(String CorpNum, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httppost<Response>("/Cashbill/" + MgtKey, CorpNum, UserID, null, "DELETE");
        }

        public CBIssueResponse Issue(String CorpNum, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;
         
            String PostData = toJsonString(request);

            return httppost<CBIssueResponse>("/Cashbill/" + MgtKey, CorpNum, UserID, PostData, "ISSUE");
        }

        public Response CancelIssue(String CorpNum, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Cashbill/" + MgtKey, CorpNum, UserID, PostData, "CANCELISSUE");
        }

        public Response SendEmail(String CorpNum, String MgtKey, String Receiver, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.receiver = Receiver;

            String PostData = toJsonString(request);

            return httppost<Response>("/Cashbill/" + MgtKey, CorpNum, UserID, PostData, "EMAIL");
        }

        public Response SendSMS(String CorpNum, String MgtKey,String Sender, String Receiver, String Contents, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.sender = Sender;
            request.receiver = Receiver;
            request.contents = Contents;

            String PostData = toJsonString(request);

            return httppost<Response>("/Cashbill/" + MgtKey, CorpNum, UserID, PostData, "SMS");
        }

        public Response SendFAX(String CorpNum, String MgtKey, String Sender, String Receiver, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.sender = Sender;
            request.receiver = Receiver;
         
            String PostData = toJsonString(request);

            return httppost<Response>("/Cashbill/" + MgtKey, CorpNum, UserID, PostData, "FAX");
        }

        public Cashbill GetDetailInfo(String CorpNum, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<Cashbill>("/Cashbill/" + MgtKey + "?Detail", CorpNum, null);
        }

        public CashbillInfo GetInfo(String CorpNum, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<CashbillInfo>("/Cashbill/" + MgtKey, CorpNum, null);
        }

        public List<CashbillInfo> GetInfos(String CorpNum, List<String> MgtKeyList)
        {
            if(MgtKeyList == null || MgtKeyList.Count == 0) throw new PopbillException(-99999999,"문서번호 목록이 입력되지 않았습니다.");

            String PostData = toJsonString(MgtKeyList);

            return httppost<List<CashbillInfo>>("/Cashbill/States", CorpNum, null, PostData, null);
        }

        public List<CashbillLog> GetLogs(String CorpNum, string MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<List<CashbillLog>>("/Cashbill/" + MgtKey + "/Logs", CorpNum, null);
        }

        public String GetPopUpURL(String CorpNum, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Cashbill/" + MgtKey + "?TG=POPUP", CorpNum, UserID);

            return response.url;

        }

        public string GetViewURL(String CorpNum, String MgtKey, String UserID)
        {
            if (string.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Cashbill/" + MgtKey + "?TG=VIEW", CorpNum, UserID);

            return response.url;
        }

        public String GetMailURL(String CorpNum, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Cashbill/" + MgtKey + "?TG=MAIL", CorpNum, UserID);

            return response.url;

        }
        public String GetPDFURL(String CorpNum, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Cashbill/" + MgtKey + "?TG=PDF", CorpNum, UserID);

            return response.url;

        }
        public byte[] GetPDF(String CorpNum, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            byte[] response = httpget<byte[]>("/Cashbill/" + MgtKey + "?PDF", CorpNum, UserID);

            return response;

        }
        public String GetPrintURL(String CorpNum,String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Cashbill/" + MgtKey + "?TG=PRINT", CorpNum, UserID);

            return response.url;

        }
        public String GetEPrintURL(String CorpNum, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Cashbill/" + MgtKey + "?TG=EPRINT", CorpNum, UserID);

            return response.url;

        }
        public String GetMassPrintURL(String CorpNum, List<String> MgtKeyList, String UserID)
        {
            if (MgtKeyList == null || MgtKeyList.Count == 0) throw new PopbillException(-99999999, "문서번호 목록이 입력되지 않았습니다.");

            String PostData = toJsonString(MgtKeyList);

            URLResponse response = httppost<URLResponse>("/Cashbill/Prints", CorpNum, UserID, PostData, null);

            return response.url;
        }

        public Response AssignMgtKey(String CorpNum, String ItemKey, String MgtKey)
        {
            return AssignMgtKey(CorpNum, ItemKey, MgtKey, null);
        }

        public Response AssignMgtKey(String CorpNum, String ItemKey, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "할당할 문서번호가 입력되지 않았습니다.");


            String PostData = "MgtKey=" + MgtKey;

            return httppost<Response>("/Cashbill/" + ItemKey, CorpNum, UserID, PostData, null, "application/x-www-form-urlencoded; charset=utf-8");
        }

        public CBIssueResponse RegistIssue(String CorpNum, Cashbill cashbill, String Memo)
        {
            return RegistIssue(CorpNum, cashbill, Memo, null);
        }

        public CBIssueResponse RegistIssue(String CorpNum, Cashbill cashbill, String Memo, String UserID)
        {
            return RegistIssue(CorpNum, cashbill, Memo, UserID, null);
        }

        public CBIssueResponse RegistIssue(String CorpNum, Cashbill cashbill, String Memo, String UserID, String EmailSubject)
        {
            if (cashbill == null) throw new PopbillException(-99999999, "현금영수증 정보가 입력되지 않았습니다.");

            cashbill.memo = Memo;

            if (EmailSubject != null) cashbill.emailSubject = EmailSubject;

            String PostData = toJsonString(cashbill);

            return httppost<CBIssueResponse>("/Cashbill", CorpNum, UserID, PostData, "ISSUE");
        }


        /*
         * 취소현금영수증 임시저장 기능 추가 (2017/08/16)
         */
        public Response RevokeRegister(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate)
        {
            return RevokeRegister(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, false, null, false, null, null, null, null, null);
        }

        public Response RevokeRegister(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate, bool smssendYN)
        {
            return RevokeRegister(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, smssendYN, null, false, null, null, null, null, null);
        }

        public Response RevokeRegister(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate, bool smssendYN, String UserID)
        {
            return RevokeRegister(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, smssendYN, UserID, false, null, null, null, null, null);
        }

        public Response RevokeRegister(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate, bool smssendYN, String UserID, 
            bool isPartCancel, int? cancelType, String supplyCost, String tax, String serviceFee, String totalAmount)
        {

            RevokeRequest request = new RevokeRequest();
            request.mgtKey = mgtKey;
            request.orgConfirmNum = orgConfirmNum;
            request.orgTradeDate = orgTradeDate;
            request.smssenYN = smssendYN;
            request.isPartCancel = isPartCancel;
            request.cancelType = cancelType;
            request.supplyCost = supplyCost;
            request.tax = tax;
            request.serviceFee = serviceFee;
            request.totalAmount = totalAmount;
            
            String PostData = toJsonString(request);

            return httppost<Response>("/Cashbill", CorpNum, UserID, PostData, "REVOKE");
        }

        /*
         * 취소현금영수증 즉시발행 기능 추가 (2017/08/16)
         */
        public CBIssueResponse RevokeRegistIssue(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate)
        {
            return RevokeRegistIssue(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, false, null, null, false, null, null, null, null, null);
        }

        public CBIssueResponse RevokeRegistIssue(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate, bool smssendYN)
        {
            return RevokeRegistIssue(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, smssendYN, null, null, false, null, null, null, null, null);
        }

        public CBIssueResponse RevokeRegistIssue(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate, bool smssendYN, String memo)
        {
            return RevokeRegistIssue(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, smssendYN, memo, null, false, null, null, null, null, null);
        }

        public CBIssueResponse RevokeRegistIssue(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate, bool smssendYN, String memo, String UserID)
        {
            return RevokeRegistIssue(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, smssendYN, memo, UserID, false, null, null, null, null, null);
        }

        public CBIssueResponse RevokeRegistIssue(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate,
            bool smssendYN, String memo, Boolean isPartCancel, int cancelType, String supplyCost,
            String tax, String serviceFee, String totalAmount)
        {
            return RevokeRegistIssue(CorpNum, mgtKey, orgConfirmNum, orgTradeDate, smssendYN, memo, null, false, null, null, null, null, null);
        }

        public CBIssueResponse RevokeRegistIssue(String CorpNum, String mgtKey, String orgConfirmNum, String orgTradeDate, 
            bool smssendYN, String memo, String UserID, Boolean isPartCancel, int? cancelType, String supplyCost, 
            String tax, String serviceFee, String totalAmount)
        {

            RevokeRequest request = new RevokeRequest();
            request.mgtKey = mgtKey;
            request.orgConfirmNum = orgConfirmNum;
            request.orgTradeDate = orgTradeDate;
            request.smssenYN = smssendYN;
            request.memo = memo;
            request.isPartCancel = isPartCancel;
            request.cancelType = cancelType;
            request.supplyCost = supplyCost;
            request.tax = tax;
            request.serviceFee = serviceFee;
            request.totalAmount = totalAmount;

            String PostData = toJsonString(request);

            return httppost<CBIssueResponse>("/Cashbill", CorpNum, UserID, PostData, "REVOKEISSUE");
        }
        
        public CBSearchResult Search(String CorpNum, String DType, String SDate, String EDate, String[] State, String[] TradeType, String[] TradeUsage, String[] TaxationType, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, DType, SDate, EDate, State, TradeType, TradeUsage, null, TaxationType, "", Order, Page, PerPage);
        }

        public CBSearchResult Search(String CorpNum, String DType, String SDate, String EDate, String[] State, String[] TradeType, String[] TradeUsage, String[] TradeOpt, String[] TaxationType, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, DType, SDate, EDate, State, TradeType, TradeUsage, TradeOpt, TaxationType, "", Order, Page, PerPage);
        }

        public CBSearchResult Search(String CorpNum, String DType, String SDate, String EDate, String[] State, String[] TradeType, String[] TradeUsage, String[] TaxationType, String QString, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, DType, SDate, EDate, State, TradeType, TradeUsage, null, TaxationType, QString, Order, Page, PerPage);
        }

        public CBSearchResult Search(String CorpNum, String DType, String SDate, String EDate, String[] State, String[] TradeType, String[] TradeUsage, String[] TradeOpt, String[] TaxationType, String QString, String Order, int Page, int PerPage)
        {
            if (String.IsNullOrEmpty(DType)) throw new PopbillException(-99999999, "검색일자 유형이 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(SDate)) throw new PopbillException(-99999999, "시작일자가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(EDate)) throw new PopbillException(-99999999, "종료일자가 입력되지 않았습니다.");

            String uri = "/Cashbill/Search";
            uri += "?DType=" + DType;
            uri += "&SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);
            uri += "&TradeType=" + String.Join(",", TradeType);
            uri += "&TradeUsage=" + String.Join(",", TradeUsage);
            if (TradeOpt != null) uri += "&TradeOpt=" + String.Join(",", TradeOpt);
            uri += "&TaxationType=" + String.Join(",", TaxationType);
            if (QString != "") uri += "&QString=" + QString;
            uri += "&Order=" + Order;
            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();

            return httpget<CBSearchResult>(uri, CorpNum, null);
        }

        public List<EmailConfig> ListEmailConfig(String CorpNum)
        {
            return ListEmailConfig(CorpNum, null);
        }

        public List<EmailConfig> ListEmailConfig(String CorpNum, String UserID)
        {
            return httpget<List<EmailConfig>>("/Cashbill/EmailSendConfig", CorpNum, UserID);
        }

        public Response UpdateEmailConfig(String CorpNum, String EmailType, bool SendYN)
        {
            return UpdateEmailConfig(CorpNum, EmailType, SendYN, null);
        }

        public Response UpdateEmailConfig(String CorpNum, String EmailType, bool SendYN, String UserID)
        {
            if (String.IsNullOrEmpty(EmailType)) throw new PopbillException(-99999999, "메일전송 타입이 입력되지 않았습니다.");

            String uri = "/Cashbill/EmailSendConfig?EmailType=" + EmailType + "&SendYN=" + SendYN;

            return httppost<Response>(uri, CorpNum, UserID, null, null);
        }

        [DataContract]
        private class MemoRequest
        {
            [DataMember]
            public String memo;
        }

        [DataContract]
        private class ResendRequest
        {
            [DataMember]
            public String receiver;
            [DataMember]
            public String sender = null;
            [DataMember]
            public String contents = null;
        }

        [DataContract]
        private class RevokeRequest
        {
            [DataMember]
            public String mgtKey;
            [DataMember]
            public String orgTradeDate;
            [DataMember]
            public String orgConfirmNum;
            [DataMember]
            public bool? smssenYN = false;
            [DataMember]
            public String memo;
            [DataMember]
            public bool? isPartCancel = false;
            [DataMember]
            public int? cancelType;
            [DataMember]
            public String supplyCost;
            [DataMember]
            public String tax;
            [DataMember]
            public String serviceFee;
            [DataMember]
            public String totalAmount;
        }

    }

}
