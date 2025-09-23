using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Popbill;

namespace Popbill.AccountCheck
{
    public class AccountCheckService : BaseService
    {
        public AccountCheckService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("182");
            this.AddScope("183");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID) 
        {
            return GetChargeInfo(CorpNum, UserID, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID, String ServiceType)
        {
            String url = "/EasyFin/AccountCheck/ChargeInfo?serviceType=" + ServiceType;

            ChargeInfo response = httpget<ChargeInfo>(url, CorpNum, UserID);

            return response;
        }

        public Single GetUnitCost(String CorpNum)
        {
            return GetUnitCost(CorpNum, null, null);
        }

        public Single GetUnitCost(String CorpNum, String ServiceType, String UserID)
        {
            String url = "/EasyFin/AccountCheck/UnitCost?serviceType=" + ServiceType;

            UnitCostResponse response = httpget<UnitCostResponse>(url, CorpNum, UserID);

            return response.unitCost;
        }


        public AccountCheckInfo CheckAccountInfo(String MemberCorpNum, String BankCode, String AccountNumber)
        {
            return CheckAccountInfo(MemberCorpNum, BankCode, AccountNumber, null);
        }

        public AccountCheckInfo CheckAccountInfo(String MemberCorpNum, String BankCode, String AccountNumber, String UserID)
        {
            String url = "/EasyFin/AccountCheck";
            url += "?c=" + BankCode;
            url += "&n=" + AccountNumber;

            return httppost<AccountCheckInfo>(url, MemberCorpNum, UserID, null, null);
        }

        public DepositorCheckInfo CheckDepositorInfo(String MemberCorpNum, String BankCode, String AccountNumber, String IdentityNumType, String IdentityNum)
        {
            return CheckDepositorInfo(MemberCorpNum, BankCode, AccountNumber, IdentityNumType, IdentityNum, null);
        }

        public DepositorCheckInfo CheckDepositorInfo(String MemberCorpNum, String BankCode, String AccountNumber, String IdentityNumType, String IdentityNum, String UserID)
        {
            String url = "/EasyFin/DepositorCheck";
            url += "?c=" + BankCode;
            url += "&n=" + AccountNumber;
            url += "&t=" + IdentityNumType;
            url += "&p=" + IdentityNum;

            return httppost<DepositorCheckInfo>(url, MemberCorpNum, UserID, null, null);
        }
    }
}
