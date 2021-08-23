using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Popbill;

namespace Popbill.Taxinvoice
{
    public enum MgtKeyType {SELL , BUY , TRUSTEE};

    public class TaxinvoiceService : BaseService
    {
        public TaxinvoiceService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("110");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/Taxinvoice/ChargeInfo", CorpNum, UserID);

            return response;
        }

        public Single GetUnitCost(String CorpNum)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/Taxinvoice?cfg=UNITCOST", CorpNum, null);

            return response.unitCost;
        }

        public DateTime GetCertificateExpireDate(String CorpNum)
        {
            CertResponse response = httpget<CertResponse>("/Taxinvoice?cfg=CERT", CorpNum, null);

            return DateTime.ParseExact( response.certificateExpiration,"yyyyMMddHHmmss",null);
        }

        public String GetURL(String CorpNum, String UserID, String TOGO)
        {
            URLResponse response = httpget<URLResponse>("/Taxinvoice?TG=" + TOGO, CorpNum, UserID);

            return response.url;
        }

        public bool CheckMgtKeyInUse(String CorpNum, MgtKeyType KeyType, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            try
            {
                TaxinvoiceInfo response = httpget<TaxinvoiceInfo>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, null);

                return string.IsNullOrEmpty(response.itemKey) == false;
            }
            catch (PopbillException pe)
            {
                if (pe.code == -11000005) return false;

                throw pe;
            }

        }

        public List<EmailPublicKey> GetEmailPublicKeys(String CorpNum)
        {
            return httpget<List<EmailPublicKey>>("/Taxinvoice/EmailPublicKeys", CorpNum, null);
        }

        public Response Register(String CorpNum, Taxinvoice taxinvoice)
        {
            return Register(CorpNum, taxinvoice, null);
        }
        public Response Register(String CorpNum, Taxinvoice taxinvoice, String UserID)
        {
            return Register(CorpNum, taxinvoice, null, false);
        }

        public Response Register(String CorpNum, Taxinvoice taxinvoice, String UserID  , bool writeSpecification )
        {
            if (taxinvoice == null) throw new PopbillException(-99999999, "세금계산서 정보가 입력되지 않았습니다.");

            if (writeSpecification)
            {
                taxinvoice.writeSpecification = writeSpecification;
            }
            String PostData = toJsonString(taxinvoice);

            return httppost<Response>("/Taxinvoice", CorpNum, UserID, PostData, null);
        }

        public Response Update(String CorpNum, MgtKeyType KeyType, String MgtKey, Taxinvoice taxinvoice, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            if (taxinvoice == null) throw new PopbillException(-99999999, "세금계산서 정보가 입력되지 않았습니다.");

            String PostData = toJsonString(taxinvoice);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey , CorpNum, UserID, PostData, "PATCH");
        }

        public Response Delete(String CorpNum, MgtKeyType KeyType, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, "", null, "DELETE");
        }

        public Response Delete(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, null, "DELETE");
        }

        public Response Send(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String EmailSubject, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;
            request.emailSubject = EmailSubject;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "SEND");
        }

        public Response CancelSend(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "CANCELSEND");
        }

        public Response Accept(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "ACCEPT");
        }

        public Response Deny(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "DENY");
        }
        public IssueResponse Issue(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            return Issue(CorpNum, KeyType, MgtKey, Memo, null, false, UserID);
        }

        public IssueResponse Issue(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, bool ForceIssue, String UserID)
        {
            return Issue(CorpNum, KeyType, MgtKey, Memo, null, ForceIssue, UserID);
        }

        public IssueResponse Issue(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String EmailSubject, bool ForceIssue, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            IssueRequest request = new IssueRequest();

            request.memo = Memo;
            request.emailSubject = EmailSubject;
            request.forceIssue = ForceIssue;

            String PostData = toJsonString(request);

            return httppost<IssueResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "ISSUE");
        }

        public Response CancelIssue(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "CANCELISSUE");
        }

        public Response RegistRequest(String CorpNum, Taxinvoice taxinvoice)
        {
            return RegistRequest(CorpNum, taxinvoice, "", "");
        }

        public Response RegistRequest(String CorpNum, Taxinvoice taxinvoice, String Memo)
        {
            return RegistRequest(CorpNum, taxinvoice, Memo, "");
        }

        public Response RegistRequest(String CorpNum, Taxinvoice taxinvoice, String Memo, String UserID)
        {
            taxinvoice.memo = Memo;
            
            String PostData = toJsonString(taxinvoice);

            return httppost<Response>("/Taxinvoice", CorpNum, UserID, PostData, "REQUEST");
        }

        public Response Request(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "REQUEST");
        }

        public Response Refuse(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "REFUSE");
        }

        public Response CancelRequest(String CorpNum, MgtKeyType KeyType, String MgtKey, String Memo, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            MemoRequest request = new MemoRequest();

            request.memo = Memo;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "CANCELREQUEST");
        }

        public Response SendToNTS(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

          
            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, null, "NTS");
        }

        public BulkResponse BulkSubmit(String CorpNum, String SubmitID, List<Taxinvoice> taxinvoiceList, bool ForceIssue)
        {
            return BulkSubmit(CorpNum, SubmitID, taxinvoiceList, ForceIssue, null);
        }

        public BulkResponse BulkSubmit(String CorpNum, String SubmitID, List<Taxinvoice> taxinvoiceList, bool ForceIssue, String UserID)
        {
            if (string.IsNullOrEmpty(SubmitID)) throw new PopbillException(-99999999, "제출아이디(SubmitID)가 입력되지 않았습니다.");
            if (taxinvoiceList == null || taxinvoiceList.Count <= 0) throw new PopbillException(-99999999, "세금계산서 정보가 입력되지 않았습니다.");

            BulkTaxinvoiceSubmit tx = new BulkTaxinvoiceSubmit();
            tx.forceIssue = ForceIssue;
            tx.invoices = taxinvoiceList;
            
            String PostData = toJsonString(tx);

            return httpBulkPost<BulkResponse>("/Taxinvoice/", CorpNum, SubmitID, PostData, UserID, "BULKISSUE");

        }

        public Response SendEmail(String CorpNum, MgtKeyType KeyType, String MgtKey, String Receiver, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.receiver = Receiver;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "EMAIL");
        }

        public Response SendSMS(String CorpNum, MgtKeyType KeyType, String MgtKey,String Sender, String Receiver, String Contents, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.sender = Sender;
            request.receiver = Receiver;
            request.contents = Contents;

            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "SMS");
        }

        public Response SendFAX(String CorpNum, MgtKeyType KeyType, String MgtKey, String Sender, String Receiver, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            ResendRequest request = new ResendRequest();

            request.sender = Sender;
            request.receiver = Receiver;
         
            String PostData = toJsonString(request);

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, UserID, PostData, "FAX");
        }

        public Taxinvoice GetDetailInfo(String CorpNum, MgtKeyType KeyType, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<Taxinvoice>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?Detail", CorpNum,null);
        }

        public TaxinvoiceInfo GetInfo(String CorpNum, MgtKeyType KeyType, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<TaxinvoiceInfo>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey, CorpNum, null);
        }

        public List<TaxinvoiceInfo> GetInfos(String CorpNum, MgtKeyType KeyType , List<String> MgtKeyList)
        {
            if(MgtKeyList == null || MgtKeyList.Count == 0) throw new PopbillException(-99999999,"문서번호 목록이 입력되지 않았습니다.");

            String PostData = toJsonString(MgtKeyList);

            return httppost<List<TaxinvoiceInfo>>("/Taxinvoice/" +  KeyType.ToString(),CorpNum,null,PostData,null);
        }

        public BulkTaxinvoiceResult GetBulkResult(String CorpNum, String SubmitID)
        {
            return GetBulkResult(CorpNum, SubmitID, null);
        }
        public BulkTaxinvoiceResult GetBulkResult(String CorpNum, String SubmitID, String UserID)
        {
            if (string.IsNullOrEmpty(SubmitID)) throw new PopbillException(-99999999, "제출아이디(SubmitID)가 입력되지 않았습니다.");

            return httpget<BulkTaxinvoiceResult>("/Taxinvoice/BULK/" + SubmitID + "/State", CorpNum, UserID);
        }

        public List<TaxinvoiceLog> GetLogs(String CorpNum, MgtKeyType KeyType ,String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget< List<TaxinvoiceLog>>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "/Logs", CorpNum, null);
        }
        public String GetPopUpURL(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?TG=POPUP", CorpNum, UserID);

            return response.url;

        }
        public String GetViewURL(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?TG=VIEW", CorpNum, UserID);

            return response.url;

        }
        public String GetMailURL(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?TG=MAIL", CorpNum, UserID);

            return response.url;

        }
        public String GetPDFURL(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?TG=PDF", CorpNum, UserID);

            return response.url;

        }
        public byte[] GetPDF(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            byte[] response = httpget<byte[]>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?PDF", CorpNum, UserID);

            return response;

        }
        public String GetPrintURL(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?TG=PRINT", CorpNum, UserID);

            return response.url;

        }
        public String GetOldPrintURL(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?TG=PRINTOLD", CorpNum, UserID);

            return response.url;

        }
        public String GetEPrintURL(String CorpNum, MgtKeyType KeyType, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            URLResponse response = httpget<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "?TG=EPRINT", CorpNum, UserID);

            return response.url;

        }
        public String GetMassPrintURL(String CorpNum, MgtKeyType KeyType, List<String> MgtKeyList, String UserID)
        {
            if (MgtKeyList == null || MgtKeyList.Count == 0) throw new PopbillException(-99999999, "문서번호 목록이 입력되지 않았습니다.");

            String PostData = toJsonString(MgtKeyList);

            URLResponse response = httppost<URLResponse>("/Taxinvoice/" + KeyType.ToString() + "?Print", CorpNum, UserID,PostData,null);

            return response.url;
        }

        public Response AttachFile(String CorpNum, MgtKeyType KeyType, String MgtKey, String FilePath, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(FilePath)) throw new PopbillException(-99999999, "파일경로가 입력되지 않았습니다.");

            List<UploadFile> files = new List<UploadFile>();

            UploadFile file = new UploadFile();

            file.FieldName = "Filedata";
            file.FileName = System.IO.Path.GetFileName(FilePath);
            file.FileData = new FileStream(FilePath, FileMode.Open, FileAccess.Read);

            files.Add(file);

            return httppostFile<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "/Files", CorpNum, UserID, null, files, null);
        }

        public List<AttachedFile> GetFiles(String CorpNum, MgtKeyType KeyType, String MgtKey)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");

            return httpget<List<AttachedFile>>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "/Files", CorpNum, null);
        }

        public Response DeleteFile(String CorpNum, MgtKeyType KeyType, String MgtKey,String FileID , String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "문서번호가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(FileID)) throw new PopbillException(-99999999, "파일 아이디가 입력되지 않았습니다.");

            return httppost<Response>("/Taxinvoice/" + KeyType.ToString() + "/" + MgtKey + "/Files/" + FileID, CorpNum, UserID, null, "DELETE");
        }

        public IssueResponse RegistIssue(String CorpNum, Taxinvoice taxinvoice, bool ForceIssue, String Memo)
        {
            return RegistIssue(CorpNum, taxinvoice, ForceIssue, Memo, false, null, null, null);
        }
        public IssueResponse RegistIssue(String CorpNum, Taxinvoice taxinvoice, bool ForceIssue, String Memo, bool WriteSpecification, String DealinvoiceMgtKey, String EmailSubject)
        {
            return RegistIssue(CorpNum, taxinvoice, ForceIssue, Memo, WriteSpecification, DealinvoiceMgtKey, EmailSubject, null);
        }

        public IssueResponse RegistIssue(String CorpNum, Taxinvoice taxinvoice, bool ForceIssue, String Memo, bool WriteSpecification, String DealinvoiceMgtKey, String EmailSubject, String UserID)
        {
            taxinvoice.writeSpecification = WriteSpecification;
            taxinvoice.forceIssue = ForceIssue;
            taxinvoice.dealInvoiceMgtKey = DealinvoiceMgtKey;
            taxinvoice.memo = Memo;
            taxinvoice.emailSubject = EmailSubject;

            String PostData = toJsonString(taxinvoice);

            return httppost<IssueResponse>("/Taxinvoice", CorpNum, UserID, PostData, "ISSUE");
        }
        
        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, bool? LateOnly, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, KeyType, DType, SDate, EDate, State, Type, TaxType, null, LateOnly, null, null, null, "", Order, Page, PerPage, "", null, null, null, "");
        }

        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, bool? LateOnly, String Order, int Page, int PerPage, String UserID)
        {
            return Search(CorpNum, KeyType, DType, SDate, EDate, State, Type, TaxType, null, LateOnly, null, null, null, "", Order, Page, PerPage, "", UserID, null, null, "");
        }

        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, bool? LateOnly, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String Order, int Page, int PerPage)
        {
            return Search(CorpNum, KeyType, DType, SDate, EDate, State, Type, TaxType, null, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, "", Order, Page, PerPage, "", null, null, null, "");
        }

            
        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, bool? LateOnly, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String Order, int Page, int PerPage, String UserID)
        {
            return Search(CorpNum, KeyType, DType, SDate, EDate, State, Type, TaxType, null, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, "", Order, Page, PerPage, "", null, null, null, "");
        }

        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, bool? LateOnly, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String QString, String Order, int Page, int PerPage, String UserID)
        {
            return Search(CorpNum, KeyType, DType, SDate, EDate, State, Type, TaxType, null, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, "", Order, Page, PerPage, "", null, null, null, "");
        }

        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, bool? LateOnly, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String QString, String Order, int Page, int PerPage, String InterOPYN, String UserID)
        {
            return Search(CorpNum, KeyType, DType, SDate, EDate, State, Type, TaxType, null, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page, PerPage, InterOPYN, UserID, null, null, "");
        }

        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, String[] IssueType, bool? LateOnly,
            String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String QString, String Order, int Page, int PerPage, String InterOPYN, String UserID)
        {
            return Search(CorpNum, KeyType, DType, SDate, EDate, State, Type, TaxType, IssueType, LateOnly, TaxRegIDYN, TaxRegIDType, TaxRegID, QString, Order, Page, PerPage, InterOPYN, UserID, null, null, "");
        }


        public TISearchResult Search(String CorpNum, MgtKeyType KeyType, String DType, String SDate, String EDate, String[] State, String[] Type, String[] TaxType, String[] IssueType, bool? LateOnly, 
            String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String QString, String Order, int Page, int PerPage, String InterOPYN, String UserID, String[] RegType, String[] CloseDownState, String MgtKey)
        {
            if (String.IsNullOrEmpty(DType)) throw new PopbillException(-99999999, "검색일자 유형이 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(SDate)) throw new PopbillException(-99999999, "시작일자가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(EDate)) throw new PopbillException(-99999999, "종료일자가 입력되지 않았습니다.");

            String uri = "/Taxinvoice/" + KeyType;
            uri += "?DType=" + DType;
            uri += "&SDate=" + SDate;
            uri += "&EDate=" + EDate;
            uri += "&State=" + String.Join(",", State);
            uri += "&Type=" + String.Join(",", Type);
            uri += "&TaxType=" + String.Join(",", TaxType);

            if (IssueType != null) uri += "&IssueType=" + String.Join(",", IssueType);
            if (RegType != null) uri += "&RegType=" + String.Join(",", RegType);
            if (CloseDownState != null) uri += "&CloseDownState=" + String.Join(",", CloseDownState);
            
            if (LateOnly != null)
            {
                if ((bool)LateOnly)
                {
                    uri += "&LateOnly=1";
                }
                else
                {
                    uri += "&LateOnly=0";
                }
            }


            if (TaxRegIDYN != "") uri += "&TaxRegIDYN=" + TaxRegIDYN;
            if (MgtKey != "") uri += "&MgtKey=" + MgtKey;

            uri += "&InterOPYN=" + InterOPYN;
            uri += "&TaxRegIDType=" + TaxRegIDType;
            uri += "&TaxRegID=" + TaxRegID;
            uri += "&QString=" + QString;
            uri += "&Order=" + Order;
            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();

            return httpget<TISearchResult>(uri, CorpNum, UserID);
        }

        public Response AttachStatement(String CorpNum, MgtKeyType KeyType, String MgtKey, int DocItemCode, String DocMgtKey)
        {
            String uri = "/Taxinvoice/" + KeyType + "/" + MgtKey + "/AttachStmt";

            DocRequest request = new DocRequest();
            request.ItemCode = DocItemCode;
            request.MgtKey = DocMgtKey;

            String PostData = toJsonString(request);
            
            return httppost<Response>(uri, CorpNum, null, PostData, null);
        }

        public Response DetachStatement(String CorpNum, MgtKeyType KeyType, String MgtKey, int DocItemCode, String DocMgtKey)
        {
            String uri = "/Taxinvoice/" + KeyType + "/" + MgtKey + "/DetachStmt";

            DocRequest request = new DocRequest();
            request.ItemCode = DocItemCode;
            request.MgtKey = DocMgtKey;

            String PostData = toJsonString(request);

            return httppost<Response>(uri, CorpNum, null, PostData, null);
        }

        public Response AssignMgtKey(String CorpNum, MgtKeyType KeyType, String ItemKey, String MgtKey)
        {
            return AssignMgtKey(CorpNum, KeyType, ItemKey, MgtKey, null);
        }
        public Response AssignMgtKey(String CorpNum, MgtKeyType KeyType, String ItemKey, String MgtKey, String UserID)
        {
            if (String.IsNullOrEmpty(MgtKey)) throw new PopbillException(-99999999, "할당할 문서번호가 입력되지 않았습니다.");


            String PostData = "MgtKey="+MgtKey;

            return httppost<Response>("/Taxinvoice/" + ItemKey + "/" + KeyType, CorpNum, UserID, PostData, null, "application/x-www-form-urlencoded; charset=utf-8");
        }

        public List<EmailConfig> ListEmailConfig(String CorpNum)
        {
            return ListEmailConfig(CorpNum, null);
        }    

        public List<EmailConfig> ListEmailConfig(String CorpNum, String UserID)
        {
            return httpget<List<EmailConfig>>("/Taxinvoice/EmailSendConfig", CorpNum, UserID);
        }

        public Response UpdateEmailConfig(String CorpNum, String EmailType, bool SendYN)
        {
            return UpdateEmailConfig(CorpNum, EmailType, SendYN, null);
        }

        public Response UpdateEmailConfig(String CorpNum, String EmailType, bool SendYN, String UserID)
        {
            if (String.IsNullOrEmpty(EmailType)) throw new PopbillException(-99999999, "메일전송 타입이 입력되지 않았습니다.");

            String uri = "/Taxinvoice/EmailSendConfig?EmailType="+EmailType+"&SendYN="+SendYN;

            return httppost<Response>(uri, CorpNum, UserID, null, null);
        }

        public bool GetSendToNTSConfig(String CorpNum)
        {
            return GetSendToNTSConfig(CorpNum, null);
        }

        public bool GetSendToNTSConfig(String CorpNum, String UserID)
        {
            return httpget<SendToNTSConfig>("/Taxinvoice/SendToNTSConfig", CorpNum, UserID).sendToNTS;
        }

        public Response CheckCertValidation(String corpNum)
        {
            return CheckCertValidation(corpNum, null);
        }

        public Response CheckCertValidation(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httpget<Response>("/Taxinvoice/CertCheck", corpNum, userID);
        }

        public String GetSealURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/?TG=SEAL", CorpNum, UserID);

            return response.url;
        }

        public String GetTaxCertURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/?TG=CERT", CorpNum, UserID);

            return response.url;
        }

        [DataContract]
        public class CertResponse
        {
            [DataMember]
            public String certificateExpiration;
        }

        [DataContract]
        private class MemoRequest
        {
            [DataMember]
            public String memo;
            [DataMember]
            public String emailSubject;

        }

        [DataContract]
        private class IssueRequest
        {
            [DataMember]
            public String memo;
            [DataMember]
            public String emailSubject;
            [DataMember]
            public bool forceIssue = false;
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
        private class DocRequest
        {
            [DataMember]
            public int ItemCode;
            [DataMember]
            public String MgtKey;
        }

        
    }
}
