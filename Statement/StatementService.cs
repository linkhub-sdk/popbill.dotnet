using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Popbill;
using System.Linq;

namespace Popbill.Statement
{
    public class StatementService : BaseService
    {      

        public StatementService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("121");
            this.AddScope("122");
            this.AddScope("123");
            this.AddScope("124");
            this.AddScope("125");
            this.AddScope("126");
        }

        public ChargeInfo GetChargeInfo(String CorpNum, int itemCode)
        {
            return GetChargeInfo(CorpNum, itemCode, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, int itemCode, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/Statement/ChargeInfo/"+itemCode.ToString(), CorpNum, UserID);

            return response;
        }

        public Single GetUnitCost(String CorpNum, int itemCode)
        {

            UnitCostResponse response = httpget<UnitCostResponse>("/Statement/" + itemCode.ToString() + "?cfg=UNITCOST", CorpNum, null);
            return response.unitCost;
        }
             

        public String GetURL(String CorpNum, String UserID, String TOGO)
        {
            URLResponse response = httpget<URLResponse>("/Statement?TG=" + TOGO, CorpNum, UserID);

            return response.url;
        }

        public bool CheckMgtKeyInuse(String CorpNum, int itemCode, String mgtKey)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            try
            {
                StatementInfo response = httpget<StatementInfo>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, null);


                return string.IsNullOrEmpty(response.itemKey) == false;

            }
            catch (PopbillException pe)
            {
                if (pe.code == -12000004) return false;

                throw pe;
            }

        }


        public String JsonText(Statement statement)
        {
            return toJsonString(statement);
        }

        public Response Register(String CorpNum, Statement statement, String UserID)
        {
            if (statement == null) throw new PopbillException(-99999999, "명세서 정보가 입력되지 않았습니다.");


            String PostData = toJsonString(statement);


            return httppost<Response>("/Statement", CorpNum, UserID, PostData, null);
        }


        public Response Update(String CorpNum, int itemCode, String mgtKey, Statement statement, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            if (statement == null) throw new PopbillException(-99999999, "명세서 정보가 입력되지 않았습니다.");


            String PostData = toJsonString(statement);

            return httppost<Response>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, UserID, PostData, "PATCH");

        }

       

        public Response Delete(String CorpNum, int itemCode, String mgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httppost<Response>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, UserID, null, "DELETE");
        }


        public Response Issue(String CorpNum, int itemCode, String mgtKey, String memo, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, UserID, PostData, "ISSUE");
        }

        public Response CancelIssue(String CorpNum, int itemCode, String mgtKey, String memo, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, UserID, PostData, "CANCEL");
        }


        public Response SendEmail(String CorpNum, int itemCode, String mgtKey, String Receiver, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.receiver = Receiver;

            String PostData = toJsonString(request);

            return httppost<Response>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, UserID, PostData, "EMAIL");
        }

        public Response SendSMS(String CorpNum, int ItemCode, String mgtKey, String Sender, String Receiver, String Contents, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.sender = Sender;
            request.receiver = Receiver;
            request.contents = Contents;

            String PostData = toJsonString(request);

            return httppost<Response>("/Statement/" + ItemCode.ToString() + "/" + mgtKey, CorpNum, UserID, PostData, "SMS");
        }


        public Response SendFAX(String CorpNum, int itemCode, String mgtKey, String Sender, String Receiver, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.sender = Sender;
            request.receiver = Receiver;

            String PostData = toJsonString(request);

            return httppost<Response>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, UserID, PostData, "FAX");
        }

        public Statement GetDetailInfo(String CorpNum, int itemCode, String mgtKey)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");
            
            return httpget<Statement>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "?Detail", CorpNum, null);
        }

        public StatementInfo GetInfo(String CorpNum, int itemCode, String mgtKey)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<StatementInfo>("/Statement/" + itemCode.ToString() + "/" + mgtKey, CorpNum, null);
        }
        


        public List<StatementInfo> GetInfos(String CorpNum, int itemCode, List<String> mgtKeyList)
        {
            if (mgtKeyList == null || mgtKeyList.Count == 0) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            String PostData = toJsonString(mgtKeyList);

            return httppost<List<StatementInfo>>("/Statement/" + itemCode.ToString(), CorpNum, null, PostData, null);
        }

        public String GetPopUpURL(String CorpNum, int itemCode, String mgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "?TG=POPUP", CorpNum, UserID);

            return response.url;
        }

        public String GetViewURL(String CorpNum, int itemCode, String mgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "?TG=VIEW", CorpNum, UserID);

            return response.url;
        }

        public String GetPrintURL(String CorpNum, int itemCode, String mgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "?TG=PRINT", CorpNum, UserID);

            return response.url;
        }


        public String GetEPrintURL(String CorpNum, int itemCode, String mgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "?TG=EPRINT", CorpNum, UserID);

            return response.url;
        }



        public String GetMailURL(String CorpNum, int itemCode, String mgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "?TG=MAIL", CorpNum, UserID);

            return response.url;
        }

        public String GetMassPrintURL(String CorpNum, int itemCode, List<String> mgtKeyList, String UserID)
        {
            if (mgtKeyList == null || mgtKeyList.Count == 0) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            String PostData = toJsonString(mgtKeyList);

            URLResponse response = httppost<URLResponse>("/Statement/" + itemCode.ToString() + "?Print", CorpNum, UserID, PostData, null);

            return response.url;
        }

        public List<StatementLog> GetLogs(String CorpNum, int itemCode, String mgtKey)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<List<StatementLog>>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "/Logs", CorpNum, null);
        }

        public Response AttachFile(String CorpNum, int itemCode, String mgtKey, String FilePath, String UserID)
        {
            if (String.IsNullOrEmpty(mgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(FilePath)) throw new PopbillException(-99999999, "파일경로가 입력되지 않았습니다.");

            List<UploadFile> files = new List<UploadFile>();

            UploadFile file = new UploadFile();

            file.FieldName = "Filedata";
            file.FileName = System.IO.Path.GetFileName(FilePath);
            file.FileData = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

            files.Add(file);

            return httppostFile<Response>("/Statement/" + itemCode.ToString() + "/" + mgtKey + "/Files", CorpNum, UserID, null, files, null);
        }


        public List<AttachedFile> GetFiles(String CorpNum, int itemCode, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<List<AttachedFile>>("/Statement/" + itemCode.ToString() + "/" + MgtKey + "/Files", CorpNum, null);
        }


        public Response DeleteFile(String CorpNum, int itemCode, String MgtKey, String FileID, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(FileID)) throw new PopbillException(-99999999, "파일 아이디가 입력되지 않았습니다.");

            return httppost<Response>("/Statement/" + itemCode.ToString() + "/" + MgtKey + "/Files/" + FileID, CorpNum, UserID, null, "DELETE");
        }

        public String FAXSend(String CorpNum, Statement statement, String SendNum, String ReceiveNum, String UserID)
        {
            if (statement == null) throw new PopbillException(-99999999, "명세서 정보가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(SendNum)) throw new PopbillException(-99999999, "발신번호가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(ReceiveNum)) throw new PopbillException(-99999999, "수신번호가 입력되지 않았습니다.");
            
            statement.sendNum = SendNum;
            statement.receiveNum = ReceiveNum;

            String PostData = toJsonString(statement);
            ReceiptResponse response = null;

            response = httppost<ReceiptResponse>("/Statement", CorpNum, UserID, PostData, "FAX");
            
            return response.receiptNum;
        }

        public STMIssueResponse RegistIssue(String CorpNum, Statement statement, String Memo)
        {
            return RegistIssue(CorpNum, statement, Memo, null);
        }

        public STMIssueResponse RegistIssue(String CorpNum, Statement statement, String Memo, String UserID)
        {
            return RegistIssue(CorpNum, statement, Memo, UserID, null);
        }

        public STMIssueResponse RegistIssue(String CorpNum, Statement statement, String Memo, String UserID, String EmailSubject)
        {
            if (statement == null) throw new PopbillException(-99999999, "명세서 정보가 입력되지 않았습니다.");

            statement.memo = Memo;

            if (EmailSubject != null) statement.emailSubject = EmailSubject;

            String PostData = toJsonString(statement);

            return httppost<STMIssueResponse>("/Statement", CorpNum, UserID, PostData, "ISSUE");
        }


        public DocSearchResult Search(String CorpNum, String DType, String SDate, String EDate, String[] State, int[] ItemCode, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, DType, SDate, EDate, State, ItemCode, "", Order, Page, PerPage);
        }

        public DocSearchResult Search(String CorpNum, String DType, String SDate, String EDate, String[] State, int[] ItemCode, String QString, String Order, int Page, int PerPage)
        {
            if (String.IsNullOrEmpty(DType)) throw new PopbillException(-99999999, "검색일자 유형이 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(SDate)) throw new PopbillException(-99999999, "시작일자가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(EDate)) throw new PopbillException(-99999999, "종료일자가 입력되지 않았습니다.");

            String uri = "/Statement/Search";
            uri += "?DType=" + DType;
            uri += "&SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);
            
            String[] ItemCodeArr = Array.ConvertAll(ItemCode, x => x.ToString());
            uri += "&ItemCode=" + String.Join(",", ItemCodeArr);
            uri += "&QString=" + QString;
            uri += "&Order=" + Order;
            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();

            return httpget<DocSearchResult>(uri, CorpNum, null);
        }


        public Response AttachStatement(String CorpNum, int itemCode, String mgtKey, int SubItemCode, String SubMgtKey)
        {
            String uri = "/Statement/" + itemCode.ToString() + "/" + mgtKey + "/AttachStmt";

            DocRequest request = new DocRequest();
            request.ItemCode = SubItemCode;
            request.MgtKey = SubMgtKey;

            String PostData = toJsonString(request);

            return httppost<Response>(uri, CorpNum, null, PostData, null);

        }

        public Response DetachStatement(String CorpNum, int itemCode, String mgtKey, int SubItemCode, String SubMgtKey)
        {
            String uri = "/Statement/" + itemCode.ToString() + "/" + mgtKey + "/DetachStmt";

            DocRequest request = new DocRequest();
            request.ItemCode = SubItemCode;
            request.MgtKey = SubMgtKey;

            String PostData = toJsonString(request);

            return httppost<Response>(uri, CorpNum, null, PostData, null);

        }

        public List<EmailConfig> ListEmailConfig(String CorpNum)
        {
            return ListEmailConfig(CorpNum, null);
        }

        public List<EmailConfig> ListEmailConfig(String CorpNum, String UserID)
        {
            return httpget<List<EmailConfig>>("/Statement/EmailSendConfig", CorpNum, UserID);
        }

        public Response UpdateEmailConfig(String CorpNum, String EmailType, bool SendYN)
        {
            return UpdateEmailConfig(CorpNum, EmailType, SendYN, null);
        }

        public Response UpdateEmailConfig(String CorpNum, String EmailType, bool SendYN, String UserID)
        {
            if (String.IsNullOrEmpty(EmailType)) throw new PopbillException(-99999999, "메일전송 타입이 입력되지 않았습니다.");

            String uri = "/Statement/EmailSendConfig?EmailType=" + EmailType + "&SendYN=" + SendYN;

            return httppost<Response>(uri, CorpNum, UserID, null, null);
        }

        public String GetSealURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/?TG=SEAL", CorpNum, UserID);

            return response.url;
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
        public class ReceiptResponse
        {
            [DataMember]
            public String receiptNum;
        }

        [DataContract]
        private class DocRequest
        {
            [DataMember]
            public int ItemCode;
            [DataMember]
            public String MgtKey;
        }
    }
}
