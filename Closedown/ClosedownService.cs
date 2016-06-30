using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Popbill;

namespace Popbill.Closedown
{
    public class ClosedownService : BaseService
    {
        public ClosedownService(String LinkID, String SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("170");
        }

        public ChargeInfo GetChargeInfo(String CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }

        public ChargeInfo GetChargeInfo(String CorpNum, String UserID) 
        {
            ChargeInfo response = httpget<ChargeInfo>("/CloseDown/ChargeInfo", CorpNum, UserID);

            return response;
        }

        public Single GetUnitCost(String CorpNum)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/CloseDown/UnitCost", CorpNum, null);

            return response.unitCost;
        }

        public CorpState checkCorpNum(String MemberCorpNum, String CheckCorpNum)
        {
            if (CheckCorpNum == null || CheckCorpNum == "")
            {
                throw new PopbillException(-99999999, "조회할 사업자번호가 입력되지 않았습니다");
            }

            return httpget<CorpState>("/CloseDown?CN=" + CheckCorpNum, MemberCorpNum, null);
        }

        public List<CorpState> checkCorpNums(String MemberCorpNum, List<String> CorpNumList)
        {
            if (CorpNumList == null || CorpNumList.Count == 0) throw new PopbillException(-99999999, "조회할 사업자번호 목록이 입력되지 않았습니다.");

            String PostData = toJsonString(CorpNumList);

            return httppost<List<CorpState>>("/CloseDown", MemberCorpNum, null, PostData, null);
        }
    }
}
