using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class BulkTaxinvoiceSubmit
    {
        [DataMember]
        public bool forceIssue;
        [DataMember]
        public List<Taxinvoice> invoices;
    }
}