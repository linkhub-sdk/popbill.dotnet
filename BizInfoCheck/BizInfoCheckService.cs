﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Popbill;

namespace Popbill.BizInfoCheck
{
    public class BizInfoCheckService : BaseService
    {
        public BizInfoCheckService(string LinkID, string SecretKey)
            : base(LinkID, SecretKey)
        {
            this.AddScope("171");
        }

        public ChargeInfo GetChargeInfo(string CorpNum)
        {
            return GetChargeInfo(CorpNum, null);
        }

        public ChargeInfo GetChargeInfo(string CorpNum, string UserID)
        {
            ChargeInfo response = httpget<ChargeInfo>("/BizInfo/ChargeInfo", CorpNum, UserID);

            return response;
        }

        public Single GetUnitCost(string CorpNum)
        {
            UnitCostResponse response = httpget<UnitCostResponse>("/BizInfo/UnitCost", CorpNum, null);

            return response.unitCost;
        }

        public BizCheckInfo checkBizInfo(string MemberCorpNum, string CheckCorpNum)
        {
            if (CheckCorpNum == null || CheckCorpNum == "")
            {
                throw new PopbillException(-99999999, "조회할 사업자번호가 입력되지 않았습니다");
            }

            return httpget<BizCheckInfo>("/BizInfo/Check?CN=" + CheckCorpNum, MemberCorpNum, null);
        }
    }
}
