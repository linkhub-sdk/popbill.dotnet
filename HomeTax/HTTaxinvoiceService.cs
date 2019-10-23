using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.HomeTax
{
    public enum KeyType { SELL, BUY, TRUSTEE };

    public class HTTaxinvoiceService :BaseService
    {

        public HTTaxinvoiceService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("111");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/HomeTax/Taxinvoice/ChargeInfo", CorpNum, UserID);

            return response;
        }

        public String RequestJob(String CorpNum, KeyType tiType, String DType, String SDate, String EDate)
        {
            return RequestJob(CorpNum, tiType, DType, SDate, EDate, null);
        }

        public String RequestJob(String CorpNum, KeyType tiType, String DType, String SDate, String EDate, String UserID)
        {
            String uri = "/HomeTax/Taxinvoice/" + tiType.ToString();
            uri += "?DType=" + DType;
            uri += "&SDate=" + SDate;
            uri += "&EDate=" + EDate;

            JobIDResponse response;

            response = httppost<JobIDResponse>(uri, CorpNum, UserID, null, null);

            return response.jobID;
        }

        public HTTaxinvoiceJobState GetJobState(String CorpNum, String JobID)
        {
            return GetJobState(CorpNum, JobID, null);
        }


        public HTTaxinvoiceJobState GetJobState(String CorpNum, String JobID, String UserID)
        {
            if (JobID.Length != 18) throw new PopbillException(-99999999, "작업아이디(jobID)가 올바르지 않습니다.");

            return httpget<HTTaxinvoiceJobState>("/HomeTax/Taxinvoice/" + JobID + "/State", CorpNum, UserID);
        }


        public List<HTTaxinvoiceJobState> ListActiveJob(String CorpNum)
        {
            return ListActiveJob(CorpNum, null);
        }

        public List<HTTaxinvoiceJobState> ListActiveJob(String CorpNum, String UserID) 
        {
            return httpget<List<HTTaxinvoiceJobState>>("/HomeTax/Taxinvoice/JobList", CorpNum, UserID);
        }

        public HTTaxinvoiceSearch Search(String CorpNum, String JobID, String[] Type, String[] TaxType, String[] PurposeType, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, int Page, int PerPage, String Order)
        {
            return Search(CorpNum, JobID, Type, TaxType, PurposeType, TaxRegIDYN, TaxRegIDType, TaxRegID, Page, PerPage, Order, null);
        }

        public HTTaxinvoiceSearch Search(String CorpNum, String JobID, String[] Type, String[] TaxType, String[] PurposeType, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, int Page, int PerPage, String Order, String UserID)
        {
            return Search(CorpNum, JobID, Type, TaxType, PurposeType, TaxRegIDYN, TaxRegIDType, TaxRegID, Page, PerPage, Order, UserID, null);
        }

        public HTTaxinvoiceSearch Search(String CorpNum, String JobID, String[] Type, String[] TaxType, String[] PurposeType, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, int Page, int PerPage, String Order, String UserID, String SearchString)
        {
            if (JobID.Length != 18) throw new PopbillException(-99999999, "작업아이디(jobID)가 올바르지 않습니다.");

            String uri = "/HomeTax/Taxinvoice/" + JobID;
            uri += "?Type=" + String.Join(",", Type);
            uri += "&TaxType=" + String.Join(",", TaxType);
            uri += "&PurposeType=" + String.Join(",", PurposeType);

            if (TaxRegIDYN != "") uri += "&TaxRegIDYN=" + TaxRegIDYN;
            if (SearchString != null) uri += "&SearchString=" + SearchString;

            uri += "&TaxRegIDType=" + TaxRegIDType;
            uri += "&TaxRegID=" + TaxRegID;

            uri += "&Page=" + Page.ToString();
            uri += "&PerPage=" + PerPage.ToString();
            uri += "&Order=" + Order;

            return httpget<HTTaxinvoiceSearch>(uri, CorpNum, UserID);
        }

        public HTTaxinvoiceSummary Summary(String CorpNum, String JobID, String[] Type, String[] TaxType, String[] PurposeType, String TaxRegIDYN, String TaxRegIDType, String TaxRegID)
        {
            return Summary(CorpNum, JobID, Type, TaxType, PurposeType, TaxRegIDYN, TaxRegIDType, TaxRegID, null);
        }

        public HTTaxinvoiceSummary Summary(String CorpNum, String JobID, String[] Type, String[] TaxType, String[] PurposeType, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String UserID)
        {
            return Summary(CorpNum, JobID, Type, TaxType, PurposeType, TaxRegIDYN, TaxRegIDType, TaxRegID, UserID, null);
        }

        public HTTaxinvoiceSummary Summary(String CorpNum, String JobID, String[] Type, String[] TaxType, String[] PurposeType, String TaxRegIDYN, String TaxRegIDType, String TaxRegID, String UserID, String SearchString)
        {
            if (JobID.Length != 18) throw new PopbillException(-99999999, "작업아이디(jobID)가 올바르지 않습니다.");

            String uri = "/HomeTax/Taxinvoice/" + JobID + "/Summary";
            uri += "?Type=" + String.Join(",", Type);
            uri += "&TaxType=" + String.Join(",", TaxType);
            uri += "&PurposeType=" + String.Join(",", PurposeType);

            if (TaxRegIDYN != "") uri += "&TaxRegIDYN=" + TaxRegIDYN;
            if (SearchString != null) uri += "&SearchString=" + SearchString;

            uri += "&TaxRegIDType=" + TaxRegIDType;
            uri += "&TaxRegID=" + TaxRegID;

            return httpget<HTTaxinvoiceSummary>(uri, CorpNum, UserID);
        }

        public HTTaxinvoice GetTaxinvoice(String CorpNum, String ntsconfirmNum)
        {
            return GetTaxinvoice(CorpNum, ntsconfirmNum, null);
        }
        
        public HTTaxinvoice GetTaxinvoice(String CorpNum, String ntsconfirmNum, String UserID)
        {
            if (ntsconfirmNum.Length != 24 ) throw new PopbillException (-99999999, "국세청승인번호가 올바르지 않습니다.");
            
            return httpget<HTTaxinvoice>("/HomeTax/Taxinvoice/"+ntsconfirmNum, CorpNum, UserID);
        }

        public HTTaxinvoiceXML GetXML(String CorpNum, String ntsconfirmNum)
        {
            return GetXML(CorpNum, ntsconfirmNum, null);
        }


        public HTTaxinvoiceXML GetXML(String CorpNum, String ntsconfirmNum, String UserID)
        {
            if (ntsconfirmNum.Length != 24) throw new PopbillException(-99999999, "국세청승인번호가 올바르지 않습니다.");

            return httpget<HTTaxinvoiceXML>("/HomeTax/Taxinvoice/" + ntsconfirmNum + "?T=xml", CorpNum, UserID);
        }

        public String GetFlatRatePopUpURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/HomeTax/Taxinvoice?TG=CHRG", CorpNum, UserID);

            return response.url;
        }

        public String GetCertificatePopUpURL(String CorpNum, String UserID)
        {
            URLResponse response = httpget<URLResponse>("/HomeTax/Taxinvoice?TG=CERT", CorpNum, UserID);

            return response.url;
        }

        public HTFlatRate GetFlatRateState(String CorpNum)
        {
            return GetFlatRateState(CorpNum, null);
        }

        public HTFlatRate GetFlatRateState(String CorpNum, String UserID) 
        {
            return httpget<HTFlatRate>("/HomeTax/Taxinvoice/Contract", CorpNum, UserID);
        }

        public DateTime GetCertificateExpireDate(String CorpNum)
        {
            return GetCertificateExpireDate(CorpNum, null);
        }
        
        public DateTime GetCertificateExpireDate(String CorpNum, String UserID)
        {
            CertResponse response = httpget<CertResponse>("/HomeTax/Taxinvoice/CertInfo", CorpNum, UserID);

            return DateTime.ParseExact(response.certificateExpiration, "yyyyMMddHHmmss", null);
        }

        public String GetPopUpURL(String corpNum, String ntsconfirmNum)
        {
            return GetPopUpURL(corpNum, ntsconfirmNum, null);
        }

        public String GetPopUpURL(String corpNum, String ntsconfirmNum, String userID)
        {
            if (ntsconfirmNum.Length != 24) throw new PopbillException(-99999999, "국세청승인번호가 올바르지 않습니다.");

            URLResponse response = httpget<URLResponse>("/HomeTax/Taxinvoice/" + ntsconfirmNum + "/PopUp", corpNum, userID);

            return response.url;
        }

        public String GetPrintURL(String corpNum, String ntsconfirmNum)
        {
            return GetPrintURL(corpNum, ntsconfirmNum, null);
        }

        public String GetPrintURL(String corpNum, String ntsconfirmNum, String userID)
        {
            if (ntsconfirmNum.Length != 24) throw new PopbillException(-99999999, "국세청승인번호가 올바르지 않습니다.");

            URLResponse response = httpget<URLResponse>("/HomeTax/Taxinvoice/" + ntsconfirmNum + "/Print", corpNum, userID);

            return response.url;
        }


        public Response CheckCertValidation(String corpNum)
        {
            return CheckCertValidation(corpNum, null);
        }

        public Response CheckCertValidation(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httpget<Response>("/HomeTax/Taxinvoice/CertCheck", corpNum, userID);
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


            return httppost<Response>("/HomeTax/Taxinvoice/DeptUser", corpNum, userID, PostData, null);
        }

        public Response CheckDeptUser(String corpNum)
        {
            return CheckDeptUser(corpNum, null);
        }

        public Response CheckDeptUser(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httpget<Response>("/HomeTax/Taxinvoice/DeptUser", corpNum, userID);
        }

        public Response CheckLoginDeptUser(String corpNum)
        {
            return CheckLoginDeptUser(corpNum, null);
        }

        public Response CheckLoginDeptUser(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httpget<Response>("/HomeTax/Taxinvoice/DeptUser/Check", corpNum, userID);
        }

        public Response DeleteDeptUser(String corpNum)
        {
            return DeleteDeptUser(corpNum, null);
        }

        public Response DeleteDeptUser(String corpNum, String userID)
        {
            if (String.IsNullOrEmpty(corpNum)) throw new PopbillException(-99999999, "연동회원 사업자번호가 입력되지 않았습니다.");

            return httppost<Response>("/HomeTax/Taxinvoice/DeptUser", corpNum, userID, null, "DELETE");
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
