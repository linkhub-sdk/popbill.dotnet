using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Popbill.HomeTax
{
    public class HTTaxinvoiceXML
    {
        [DataMember]
        public long? ResultCode;
        [DataMember]
        public string Message;
        [DataMember]
        public string retObject;
    }
}