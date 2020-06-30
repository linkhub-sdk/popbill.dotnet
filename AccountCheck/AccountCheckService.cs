using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Popbill;

namespace Popbill.AccountCheck
{
    public class AccountCheckService : BaseService
    {
        public AccountCheckService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("182");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/EasyFin/AccountCheck/ChargeInfo", CorpNum, UserID);

            return response;
        }

        public Single GetUnitCost(String CorpNum)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/EasyFin/AccountCheck/UnitCost", CorpNum, null);

            return response.unitCost;
        }

        public AccountCheckInfo CheckAccountInfo(String MemberCorpNum, String BankCode, String AccountNumber)
        {
            if (BankCode == null || BankCode == "")
            {
                throw new PopbillException(-99999999, "계좌의 기관코드가 입력되지 않았습니다");
            }

            if (AccountNumber == null || AccountNumber == "")
            {
                throw new PopbillException(-99999999, "조회할 계좌번호가 입력되지 않았습니다");
            }

            String url = "/EasyFin/AccountCheck";
            url += "?c=" + BankCode;
            url += "&n=" + AccountNumber;

            return httppost<AccountCheckInfo>(url, MemberCorpNum, null, null, null);
        }
    }
}
