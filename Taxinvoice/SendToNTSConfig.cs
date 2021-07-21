using System.Runtime.Serialization;

namespace Popbill.Taxinvoice
{
    [DataContract]
    public class SendToNTSConfig
    {
        [DataMember]
        public bool sendToNTS;
    }
}
