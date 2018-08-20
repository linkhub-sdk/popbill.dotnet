using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.HomeTax
{
    public class HTCashbillService : BaseService
    {

        public HTCashbillService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("141");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/HomeTax/Cashbill/ChargeInfo", CorpNum, UserID);

            return response;
        }

        public String RequestJob(String CorpNum, KeyType cbType, String SDate, String EDate)
        {
            return RequestJob(CorpNum, cbType, SDate, EDate, null);
        }

        public String RequestJob(String CorpNum, KeyType cbType, String SDate, String EDate, String UserID)
        {
            String uri = "/HomeTax/Cashbill/" + cbType.ToString();
            uri += "?SDate=" + SDate;
            uri += "&EDate=" + EDate;

            JobIDResponse response;

            response = httppost<JobIDResponse>(uri, CorpNum, UserID, null, null);

            return response.jobID;
        }

        public HTCashbillJobState GetJobState(String CorpNum, String JobID)
        {
            return GetJobState(CorpNum, JobID, null);
        }

        public HTCashbillJobState GetJobState(String CorpNum, String JobID, String UserID)
        {
            if (JobID.Length != 18) throw new PopbillException(-99999999, "작업아이디(jobID)가 올바르지 않습니다.");

            return httpget<HTCashbillJobState>("/HomeTax/Cashbill/" + JobID + "/State", CorpNum, UserID);
        }

        public List<HTCashbillJobState> ListActiveJob(String CorpNum)
        {
            return ListActiveJob(CorpNum, null);
        }

        public List<HTCashbillJobState> ListActiveJob(String CorpNum, String UserID)
        {
            return httpget<List<HTCashbillJobState>>("/HomeTax/Cashbill/JobList", CorpNum, UserID);
        }

        public HTCashbillSearch Search(String CorpNum, String JobID, String[] TradeType, String[] TradeUsage, int Page, int PerPage, String Order)
        {
            return Search(CorpNum, JobID, TradeType, TradeUsage, Page, PerPage, Order, null);
        }

        public HTCashbillSearch Search(String CorpNum, String JobID, String[] TradeType, String[] TradeUsage, int Page, int PerPage, String Order, String UserID)
        {
            if (JobID.Length != 18) throw new PopbillException(-99999999, "작업아이디(jobID)가 올바르지 않습니다.");

            String uri = "/HomeTax/Cashbill/" + JobID;
            uri += "?TradeType=" + String.Join(",", TradeType);
            uri += "&TradeUsage=" + String.Join(",", TradeUsage);

            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();
            uri += "&Order=" + Order;

            return httpget<HTCashbillSearch>(uri, CorpNum, UserID);
        }

        public HTCashbillSummary Summary(String CorpNum, String JobID, String[] TradeType, String[] TradeUsage)
        {
            return Summary(CorpNum, JobID, TradeType, TradeUsage, null);
        }

        public HTCashbillSummary Summary(String CorpNum, String JobID, String[] TradeType, String[] TradeUsage, String UserID)
        {
            if (JobID.Length != 18) throw new PopbillException(-99999999, "작업아이디(jobID)가 올바르지 않습니다.");

            String uri = "/HomeTax/Cashbill/" + JobID + "/Summary";
            uri += "?TradeType=" + String.Join(",", TradeType);
            uri += "&TradeUsage=" + String.Join(",", TradeUsage);

            return httpget<HTCashbillSummary>(uri, CorpNum, UserID);
        }

        public String GetFlatRatePopUpURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/HomeTax/Cashbill?TG=CHRG", CorpNum, UserID);

            return response.url;
        }

        public String GetCertificatePopUpURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/HomeTax/Cashbill?TG=CERT", CorpNum, UserID);

            return response.url;
        }

        public HTFlatRate GetFlatRateState(String CorpNum)
        {
            return GetFlatRateState(CorpNum, null);
        }

        public HTFlatRate GetFlatRateState(String CorpNum, String UserID)
        {
            return httpget<HTFlatRate>("/HomeTax/Cashbill/Contract", CorpNum, UserID);
        }

        public DateTime GetCertificateExpireDate(String CorpNum)
        {
            return GetCertificateExpireDate(CorpNum, null);
        }

        public DateTime GetCertificateExpireDate(String CorpNum, String UserID)
        {
            CertResponse response = httpget<CertResponse>("/HomeTax/Cashbill/CertInfo", CorpNum, UserID);

            return DateTime.ParseExact(response.certificateExpiration, "yyyyMMddHHmmss", null);
        }

        public Response CheckCertValidation(String corpNum)
        {
            return CheckCertValidation(corpNum, null);
        }

        public Response CheckCertValidation(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httpget<Response>("/HomeTax/Cashbill/CertCheck", corpNum, userID);
        }

        public Response RegistDeptUser(String corpNum, String deptUserID, String deptUserPWD)
        {
            return RegistDeptUser(corpNum, deptUserID, deptUserPWD, null);
        }

        public Response RegistDeptUser(String corpNum, String deptUserID, String deptUserPWD, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(deptUserID)) throw new PopbillException(-99999999, "홈택스 부서사용자 계정 아이디가 입력되지 않았습니다.");
            if (String.IsNullOrEmpty(deptUserPWD)) throw new PopbillException(-99999999, "홈택스 부서사용자 계정 비밀번호가 입력되지 않았습니다.");

            RegistDeptUserRequest request = new RegistDeptUserRequest();

            request.id = deptUserID;
            request.pwd = deptUserPWD;

            String PostData = toJsonString(request);


            return httppost<Response>("/HomeTax/Cashbill/DeptUser", corpNum, userID, PostData, null);
        }
        
        public Response CheckDeptUser(String corpNum)
        {
            return CheckDeptUser(corpNum, null);
        }

        public Response CheckDeptUser(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httpget<Response>("/HomeTax/Cashbill/DeptUser", corpNum, userID);
        }

        public Response CheckLoginDeptUser(String corpNum)
        {
            return CheckLoginDeptUser(corpNum, null);
        }

        public Response CheckLoginDeptUser(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httpget<Response>("/HomeTax/Cashbill/DeptUser/Check", corpNum, userID);
        }

        public Response DeleteDeptUser(String corpNum)
        {
            return DeleteDeptUser(corpNum, null);
        }

        public Response DeleteDeptUser(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httppost<Response>("/HomeTax/Cashbill/DeptUser", corpNum, userID, null, "DELETE");
        }

        [DataContract]
        public class JobIDResponse
        {
            [DataMember]
            public String jobID;
        }

        [DataContract]
        public class CertResponse
        {
            [DataMember]
            public String certificateExpiration;
        }

        [DataContract]
        public class RegistDeptUserRequest
        {
            [DataMember]
            public String id;
            [DataMember]
            public String pwd;
        }
    }

}
