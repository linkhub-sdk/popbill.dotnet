using System;

namespace Popbill
{
    public class PopbillException : Exception
    {
        public PopbillException()
            : base()
        {
        }
        public PopbillException(long code, String Message)
            : base(Message)
        {
            this._code = code;
        }

        private long _code;

        public long code
        {
            get { return _code; }
        }
        public PopbillException(Linkhub.LinkhubException le) : base(le.Message,le)
        {
            this._code = le.code;
        }
        
    }
}
